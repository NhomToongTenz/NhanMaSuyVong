using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_MainMenu : MonoBehaviour
{
    [SerializeField] private string sceneName = "MainScene";
    [SerializeField] private GameObject continueButton;
    [SerializeField] UI_FadeScreen fadeScreen;


    [Header("Scene to Load")]
    [SerializeField] private SceneField persistentGamePlay;
    [SerializeField] private SceneField levelScene;

    private List<AsyncOperation> scenesLoading = new List<AsyncOperation>();

    private void Start()
    {
        if (SaveManager.instance.HasSavedData() == false)
            continueButton.SetActive(false);
        else
            continueButton.SetActive(true);

    }

    public void ContinueGame()
    {
        StartCoroutine(LoadSceneWithFadeEffect(1.5f));
    }

    public void NewGame()
    {
        SaveManager.instance.DeleteSavedData();
        StartCoroutine(LoadSceneWithFadeEffect(1.5f));
    }

    public void ExitGame()
    {
        Debug.Log("Exit game");

        StartCoroutine(QuitEff());
    }

    IEnumerator QuitEff()
    {
        fadeScreen.FadeOut();

        yield return new WaitForSeconds(2f);

        Application.Quit();
    }

    IEnumerator LoadSceneWithFadeEffect(float _delay)
    {
        fadeScreen.FadeOut();

        yield return new WaitForSeconds(_delay);

        scenesLoading.Add(SceneManager.LoadSceneAsync(persistentGamePlay));
        scenesLoading.Add(SceneManager.LoadSceneAsync(levelScene, LoadSceneMode.Additive));
    }
}
