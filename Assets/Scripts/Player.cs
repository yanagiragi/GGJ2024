using System;
using Sirenix.OdinInspector;
using UI.Component;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Title("睡眠度")]
    [SerializeField] private float sleepAmount;

    [Title("進入睡覺的伐值")]
    [SerializeField] private float sleepThreshold = 0.7f;
    
    [Title("睡覺中")]
    [SerializeField] private bool isSleep;
    
    [Header("每秒新增的睡眠度")]
    [SerializeField] private float addPerSecond;
    
    [SerializeField] private ProgressBar _progressBar;
    
    private void Update()
    {
        // 檢查是否正在睡覺，如果是則不執行增加睡眠度的邏輯
        if (isSleep)
        {
            return;
        }

        // 每秒增加睡眠度
        AddSleepAmount(addPerSecond * Time.deltaTime);

        // 檢查是否達到進入睡覺的伐值
        if (sleepAmount >= sleepThreshold)
        {
            EnterSleepMode();
        }
    }
    
    
    public void SubSleepAmount(float addAmount)
    {
        var newAmount = Mathf.Max(sleepAmount - addAmount, 0f);
        SetSleepAmount(newAmount);
        _progressBar.Minus(addAmount);
    }

    [Button("增加睡眠度")]
    public void AddSleepAmount(float addAmount)
    {
        var newAmount = Mathf.Min(sleepAmount + addAmount, 1f);
        SetSleepAmount(newAmount);
        _progressBar.Plus(addAmount);
    }
    
    public void SetSleepAmount(float amount)
    {
        sleepAmount = amount;
        _progressBar.SetValue(amount);
    }
    
    private void EnterSleepMode()
    {
        // 在這裡實現進入睡覺模式的邏輯
        isSleep = true;
        Debug.Log("Entering Sleep Mode!");
    }
    
}