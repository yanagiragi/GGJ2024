using System;
using DefaultNamespace;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultUI : MonoBehaviour
{

    [SerializeField] private GameObject winPanel, losePanel;

    [SerializeField] private ScoreUI _scoreUI;
    
    private void Start()
    {
        ShowResult();
    }

    private void ShowResult()
    {
        if (ScoreManager.Instance.IsWin)
        {
            winPanel.SetActive(true);
            losePanel.SetActive(false);
            AudioManager.Instance.PlaySE(SE.Win);
        }
        else
        {
            winPanel.SetActive(false);
            losePanel.SetActive(true);
            AudioManager.Instance.PlaySE(SE.Lose);
        }
        
        _scoreUI.UpdateScoreUI(ScoreManager.Instance.Score);
    }

    public void BackToTitle()
    {
        SceneManager.LoadScene("Title");
    }
}