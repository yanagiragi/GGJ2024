using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour, ILogger
{
    #region Variable
    public string Prefix => "遊戲流程：";

    [SerializeField] private Timer timer;

    private Coroutine gameCoroutine;

    [Title("遊戲中")]
    public bool IsGaming;

    [Title("分數")]
    public int winNeedScore;

    #endregion

    #region Game Coroutine

    private void Start()
    {
        StartGame();
    }

    [Button("開始遊戲")]
    public void StartGame()
    {
        Logger.Log(this, "Start Game");
        gameCoroutine = StartCoroutine(GameCoroutine());
    }

    private IEnumerator GameCoroutine()
    {
        InitialGame();

        // 等待時間結束
        yield return new WaitUntil(() => timer.TimeOver);

        yield return GameOver();
    }

    private void InitialGame()
    {
        IsGaming = true;
        timer.StartTimer();
        ScoreManager.Instance.InitScore();
    }

    private IEnumerator GameOver()
    {
        Logger.Log(this, "GameOver");
        timer.StopTimer();
        IsGaming = false;
        JudgeWinCondition();

        yield return new WaitForSeconds(0.5f);

        StopCoroutine(gameCoroutine);
        EnterResultScene();
    }

    private void JudgeWinCondition()
    {
        ScoreManager.Instance.SetIsWin(winNeedScore);
    }

    private void EnterResultScene()
    {
        SceneManager.LoadScene("_Result");
    }

    #endregion

    #region Player Setting
    #endregion


    #region Test

    [Button("測試：增加分數")]
    public void AddScore(int addScore)
    {
        ScoreManager.Instance.AddScore(addScore);
    }

    #endregion
}
