using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoadSceneAnimationManager : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public float fadeDuration = 100f; // フェード時間

    void Start()
    {
        StartCoroutine(FadeIn());
    }

    public void FadeToScene(string sceneName)
    {
        StartCoroutine(FadeOut(sceneName));
    }

    IEnumerator FadeIn()
    {
        float t = fadeDuration;
        while (t > 0)
        {
            t -= Time.deltaTime;
            canvasGroup.alpha = t / fadeDuration;
            yield return null;
        }
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false; // UIのクリックを許可
    }

    IEnumerator FadeOut(string sceneName)
    {
        canvasGroup.blocksRaycasts = true; // UIのクリックを防ぐ
        float t = 0;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            canvasGroup.alpha = t / fadeDuration;
            yield return null;
        }
        canvasGroup.alpha = 1;
        SceneManager.LoadScene(sceneName);
    }
}
