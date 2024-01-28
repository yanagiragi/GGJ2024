using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class ImageFadeInOut : MonoBehaviour
{
    public Image myImage;
    public float fadeDuration = 1f;
    public float pauseDuration = 0.5f;

    void Start()
    {
        StartFade();
    }

    [Button("Start Fade")]
    private void StartFade()
    {
        StartCoroutine(FadeInOutRoutine());
    }

    IEnumerator FadeInOutRoutine()
    {
        while (true)
        {
            yield return StartCoroutine(FadeIn());
            yield return new WaitForSeconds(pauseDuration);
            yield return StartCoroutine(FadeOut());
            yield return new WaitForSeconds(pauseDuration);
        }
    }

    IEnumerator FadeIn()
    {
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            // 計算插值值，實現淡入效果
            float alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);

            // 設置CanvasGroup的alpha值
            myImage.canvasRenderer.SetAlpha(alpha);

            // 遞增時間
            elapsedTime += Time.deltaTime;

            yield return null;
        }
    }

    IEnumerator FadeOut()
    {
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            // 計算插值值，實現淡出效果
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);

            // 設置CanvasGroup的alpha值
            myImage.canvasRenderer.SetAlpha(alpha);

            // 遞增時間
            elapsedTime += Time.deltaTime;

            yield return null;
        }
    }
}