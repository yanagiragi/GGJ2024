using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    #region Variable

    public bool TimeOver => timerOver;
    public float CurrentTime => currentTime;
    public float TotalTime => totalTime;

    [SerializeField] private float totalTime = 60f;
    [SerializeField] private float currentTime = 0f;
    [SerializeField] private bool timerOver;

    private Coroutine countdownCoroutine;

    #endregion

    #region Event

    public static UnityEvent OnTimerOver = new UnityEvent();
    

    #endregion
    
    #region Singleton

    private static Timer instance;
    public static Timer Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<Timer>();
            }
            return instance;
        }
    }

    private void Awake()
    {
        instance = this;
    }

    #endregion


    public float GetTimeRadio()
    {
        return currentTime / totalTime;
    }
    
    [ContextMenu("Start Timer")]
    public void StartTimer()
    {
        if (countdownCoroutine != null)
        {
            StopCoroutine(countdownCoroutine);
        }
        countdownCoroutine = StartCoroutine(Countdown());
    }

    [ContextMenu("Pause Timer")]
    public void PauseTimer()
    {
        if (countdownCoroutine != null)
        {
            StopCoroutine(countdownCoroutine);
        }
    }

    [ContextMenu("Resume Timer")]
    public void ResumeTimer()
    {
        if (countdownCoroutine != null)
        {
            StopCoroutine(countdownCoroutine);
        }
        countdownCoroutine = StartCoroutine(Countdown());
    }

    [ContextMenu("Stop Timer")]
    public void StopTimer()
    {
        if (countdownCoroutine != null)
        {
            StopCoroutine(countdownCoroutine);
        }
        InvokeTimeOver();
    }

    private IEnumerator Countdown()
    {
        timerOver = false;
        currentTime = totalTime;

        while (currentTime > 0)
        {
            yield return null;

            currentTime -= Time.deltaTime;
        }

        InvokeTimeOver();
        Debug.Log("Timer Finished!");
    }

    private void InvokeTimeOver()
    {
        timerOver = true;
        OnTimerOver.Invoke();
    }
}
