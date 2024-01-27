using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;

    private void OnEnable()
    {
        ScoreManager.Instance.OnScoreChanged.AddListener(UpdateScoreUI);
    }

    private void OnDisable()
    {
        ScoreManager.Instance.OnScoreChanged.RemoveListener(UpdateScoreUI);
    }


    private void UpdateScoreUI(int score)
    {
        scoreText.text = $"Score {score}";
    }
}
