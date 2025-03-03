using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System.Collections;

public class ButtonEffectManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private Vector3 originalScale;
    public float hoverScale = 1.25f; // Hover時のスケール倍率
    public float animationSpeed = 0.15f; // 拡大アニメーション速度
    public float fadeDuration = 0.7f; // フェードアウトの時間
    public string nextSceneName = "GameSelect"; // 遷移するシーン名

    private CanvasGroup canvasGroup; // フェードアウト用
    private bool isFading = false;

    void Start()
    {
        originalScale = transform.localScale;
        canvasGroup = gameObject.AddComponent<CanvasGroup>();
        canvasGroup.alpha = 1;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isFading) // フェードアウト中でなければ
        {
            StopAllCoroutines();
            StartCoroutine(ScaleButton(originalScale * hoverScale));
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!isFading) // フェードアウト中でなければ
        {
            StopAllCoroutines();
            StartCoroutine(ScaleButton(originalScale));
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!isFading) // 連続クリックを防ぐ
        {
            isFading = true;
            StartCoroutine(FadeOutAndLoadScene());
        }
    }

    private IEnumerator ScaleButton(Vector3 targetScale)
    {
        float elapsedTime = 0f;
        Vector3 startScale = transform.localScale;

        while (elapsedTime < animationSpeed)
        {
            transform.localScale = Vector3.Lerp(startScale, targetScale, elapsedTime / animationSpeed);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.localScale = targetScale;
    }

    private IEnumerator FadeOutAndLoadScene()
    {
        float elapsedTime = 0f;
        float startAlpha = canvasGroup.alpha;

        while (elapsedTime < fadeDuration)
        {
            canvasGroup.alpha = Mathf.Lerp(startAlpha, 0, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = 0;
        SceneManager.LoadScene(nextSceneName);
    }
}
