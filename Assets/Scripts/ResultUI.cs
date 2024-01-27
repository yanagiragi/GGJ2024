using System;
using DefaultNamespace;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI result, score;

    private void Start()
    {
        ShowResult();
    }

    private void ShowResult()
    {
        result.text = ScoreManager.Instance.IsWin ? "Win" : "Lose";
        score.text = $"Score: {ScoreManager.Instance.Score}";
        
    }

    public void BackToTitle()
    {
        SceneManager.LoadScene("Title");
    }
}