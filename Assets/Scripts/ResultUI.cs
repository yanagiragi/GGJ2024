using System;
using DefaultNamespace;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI score;

    [SerializeField] private GameObject winPanel, losePanel;
    
    private void Start()
    {
        ShowResult();
    }

    private void ShowResult()
    {
        if (ScoreManager.Instance.IsWin)
        {
            winPanel.SetActive(true);
        }
        else
        {
            losePanel.SetActive(true);
        }
        score.text = $"Score: {ScoreManager.Instance.Score}";
        
    }

    public void BackToTitle()
    {
        SceneManager.LoadScene("Title");
    }
}