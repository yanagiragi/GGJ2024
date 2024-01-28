using System;
using DefaultNamespace;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI score;

    [SerializeField] private GameObject winPanel, losePanel;

    [SerializeField] private AudioManager _audioManager;
    
    private void Start()
    {
        ShowResult();
    }

    private void ShowResult()
    {
        if (ScoreManager.Instance.IsWin)
        {
            winPanel.SetActive(true);
            _audioManager.PlaySE(SE.Win);
        }
        else
        {
            losePanel.SetActive(true);
            _audioManager.PlaySE(SE.Lose);
        }
        score.text = $"Score: {ScoreManager.Instance.Score}";
        
    }

    public void BackToTitle()
    {
        SceneManager.LoadScene("Title");
    }
}