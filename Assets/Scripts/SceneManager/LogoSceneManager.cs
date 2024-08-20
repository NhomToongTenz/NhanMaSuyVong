using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LogoSceneManager : MonoBehaviour
{
    public CanvasGroup logoCanvasGroup; 
    public float fadeDuration = 1f;     
    public float delay = 3f;

    [SerializeField] private SceneField mainMenuScene;

    private void Start()
    {
        StartCoroutine(LoadMainSceneAfterDelay());
    }

    private IEnumerator LoadMainSceneAfterDelay()
    {
        yield return StartCoroutine(FadeCanvasGroup(logoCanvasGroup, 0, 1, fadeDuration)); 
        yield return new WaitForSeconds(delay); 
        yield return StartCoroutine(FadeCanvasGroup(logoCanvasGroup, 1, 0, fadeDuration)); 
        SceneManager.LoadScene(mainMenuScene);
    }

    private IEnumerator FadeCanvasGroup(CanvasGroup cg, float start, float end, float duration)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            cg.alpha = Mathf.Lerp(start, end, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        cg.alpha = end;
    }
}
