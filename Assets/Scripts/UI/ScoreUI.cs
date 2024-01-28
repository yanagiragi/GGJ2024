using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] private Sprite[] numbers;

    [SerializeField] private Image[] numberImages;

    private void OnEnable()
    {
        ScoreManager.Instance.OnScoreChanged.AddListener(UpdateScoreUI);
    }

    private void OnDisable()
    {
        ScoreManager.Instance.OnScoreChanged.RemoveListener(UpdateScoreUI);
    }

    [Button("更新分數 UI")]
    public void UpdateScoreUI(int score)
    {
        foreach (var numberImage in numberImages)
        {
            numberImage.gameObject.SetActive(false);
        }
        
        int[] scores = GetDigitsArray(score);
        
        for (var index = 0; index < scores.Length; index++)
        {
            var number = scores[index];
            numberImages[index].sprite = numbers[number];
            numberImages[index].gameObject.SetActive(true);
        }

        if (score == 0)
        {
            numberImages[0].sprite = numbers[0];
            numberImages[0].gameObject.SetActive(true);
        }
    }
    
    public static int[] GetDigitsArray(int number)
    {
        List<int> digitsList = new List<int>();

        // 将数字的每个位数提取并添加到列表中
        while (number > 0)
        {
            int digit = number % 10;  // 获取最低位的数字
            digitsList.Add(digit);

            // 去掉最低位的数字
            number /= 10;
        }

        // 将列表转换为数组并反转顺序，以保持原始数字的位数顺序
        int[] result = digitsList.ToArray();
        Array.Reverse(result);

        return result;
    }
}
