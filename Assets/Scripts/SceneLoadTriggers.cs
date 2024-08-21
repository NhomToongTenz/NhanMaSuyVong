using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadTriggers : MonoBehaviour
{
    [SerializeField] private SceneField[] _sceneToLoad;
    [SerializeField] private SceneField[] _sceneToUnload;

    private GameObject _player;

    private void Awake() {
        _player = GameObject.FindGameObjectWithTag("Player");
        if (_player == null) {
            Debug.LogError("Player object not found!");
        }
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        if (collider.gameObject == _player) {
            LoadScenes();
            UnLoadScenes();
        }
    }

    private void LoadScenes() {
        for (int i = 0; i < _sceneToLoad.Length; i++) {
            bool sceneLoaded = false;
            for (int j = 0; j < SceneManager.sceneCount; j++) {
                Scene scene = SceneManager.GetSceneAt(j);
                if (scene.name == _sceneToLoad[i].SceneName) {
                    sceneLoaded = true;
                    break;
                }
            }

            if (!sceneLoaded) {
                SceneManager.LoadScene(_sceneToLoad[i].SceneName, LoadSceneMode.Additive);
            }
        }
    }

    private void UnLoadScenes() {
        for (int i = 0; i < _sceneToUnload.Length; i++) {
            bool sceneLoaded = false;
            for (int j = 0; j < SceneManager.sceneCount; j++) {
                Scene scene = SceneManager.GetSceneAt(j);
                if (scene.name == _sceneToUnload[i].SceneName) {
                    sceneLoaded = true;
                    break;
                }
            }

            if (sceneLoaded) {
                SceneManager.UnloadSceneAsync(_sceneToUnload[i].SceneName).completed += (AsyncOperation op) => {
                    Debug.Log("Unloaded scene: " + _sceneToUnload[i].SceneName);
                    Resources.UnloadUnusedAssets();
                };
            } else {
                Debug.LogWarning("Scene to unload not found: " + _sceneToUnload[i].SceneName);
            }
        }
    }

}