// SceneField.cs
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[System.Serializable]
public class SceneField
{
    [SerializeField] private Object sceneAsset;
    [SerializeField] private string sceneName = "";

    public string SceneName
    {
        get
        {
            return sceneName;
        }
    }

    public static implicit operator string(SceneField _sceneField)
    {
        return _sceneField.SceneName;
    }

    public static implicit operator SceneField(string _sceneName)
    {
        return new SceneField { sceneName = _sceneName };
    }
}

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(SceneField))]
public class SceneFieldPropertyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect _position, SerializedProperty _property, GUIContent _label)
    {
        EditorGUI.BeginProperty(_position, GUIContent.none, _property);
        SerializedProperty sceneAsset = _property.FindPropertyRelative("sceneAsset");
        SerializedProperty sceneName = _property.FindPropertyRelative("sceneName");
        _position = EditorGUI.PrefixLabel(_position, GUIUtility.GetControlID(FocusType.Passive), _label);
        if (sceneAsset != null)
        {
            EditorGUI.BeginChangeCheck();
            Object value = EditorGUI.ObjectField(_position, sceneAsset.objectReferenceValue, typeof(SceneAsset), false);
            if (EditorGUI.EndChangeCheck())
            {
                sceneAsset.objectReferenceValue = value;
                if (sceneAsset.objectReferenceValue != null)
                {
                    string scenePath = AssetDatabase.GetAssetPath(sceneAsset.objectReferenceValue);
                    sceneName.stringValue = System.IO.Path.GetFileNameWithoutExtension(scenePath);
                }
            }
        }
        EditorGUI.EndProperty();
    }
}
#endif
