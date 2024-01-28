using System.Collections;
using UnityEngine;

public class ImageVibration : MonoBehaviour
{
    public float vibrationRange = 10f; // 震动幅度
    public float vibrationSpeed = 1f;  // 震动速度

    private RectTransform imageRectTransform;
    private float startY;

    void Start()
    {
        imageRectTransform = GetComponent<RectTransform>();
        startY = imageRectTransform.anchoredPosition.y;

        // 启动协程，用于实现震动效果
        StartCoroutine(VibrateImage());
    }

    IEnumerator VibrateImage()
    {
        while (true)
        {
            // 计算新的y坐标位置
            float newY = startY + Mathf.Sin(Time.time * vibrationSpeed) * vibrationRange;

            // 更新Image的位置
            imageRectTransform.anchoredPosition = new Vector2(imageRectTransform.anchoredPosition.x, newY);

            yield return null; // 等待一帧
        }
    }
}