using System;
using System.Collections;
using DefaultNamespace;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spirit : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private float size;
    
    [Title("持續時間")]
    public float idleDuration = 5f;

    public float castingSpellDuration = 3f;

    [Title("移動設定")] 
    public float idleSpeed = 30f;
    public float speed = 2f;

    // 定義移動方向，1 表示向右，-1 表示向左
    [SerializeField] private int direction = 1;
    
    [SerializeField] private Transform leftBoundary, rightBoundary;
    private Transform targetBoundary;
    private int targetIndex;

    // 判斷是否已抵達目標位置的距離容忍值
    public float arrivalThreshold = 10f;
    
    [Serializable]
    private enum GhostState
    {
        Idle,
        MoveToTarget,
        CastingSpell
    }

    [SerializeField] private GhostState currentState = GhostState.Idle;
    [SerializeField] private UIHand _uiHand;
    private static readonly int CastingSpell = Animator.StringToHash("CastingSpell");


    private void Start()
    {
        size = transform.localScale.x;
        StartCoroutine(GhostStateMachine());
    }

    private void Update()
    {
        Vector3 scale = transform.localScale;
        scale.x =  size * direction;
        transform.localScale = scale;
    }

    private IEnumerator GhostStateMachine()
    {
        while (true)
        {
            switch (currentState)
            {
                case GhostState.Idle:
                    yield return IdleState();
                    break;
                case GhostState.MoveToTarget:
                    yield return MovingState();
                    break;
                case GhostState.CastingSpell:
                    yield return CastingSpellState();
                    break;
            }
        }
    }

    private IEnumerator IdleState()
    {
        Debug.Log("Idle State");
        _animator.SetBool(CastingSpell, false);
        
        float startTime = Time.time;
        
        while (Time.time - startTime < idleDuration)
        {
            // 獲取當前UI的位置
            Vector3 currentPosition = transform.position;

            // 更新UI的X軸位置，根據速度和方向
            currentPosition.x += idleSpeed * direction * Time.deltaTime;

            // 確保UI在設定的區間內移動
            currentPosition.x = Mathf.Clamp(currentPosition.x, leftBoundary.position.x, 
                rightBoundary.position.x);

            // 將更新後的位置應用到UI
            transform.position = currentPosition;

            // 檢查是否到達邊界，如果是則改變移動方向
            if (currentPosition.x >= rightBoundary.position.x || currentPosition.x <= leftBoundary.position.x)
            {
                direction *= -1;
            }
            
            yield return null;
        }
        
        currentState = GhostState.MoveToTarget;
    }

    private IEnumerator MovingState()
    {
        AudioManager.Instance.PlaySE(SE.Ghost);
        
        _animator.SetBool(CastingSpell, false);

        // 如果有任何玩家睡著，回到 Idle 狀態
        if (GameManager.Instance.PlayerManager.IsAnyPlayerSlept())
        {
            currentState = GhostState.Idle;
        }
        else
        {
            GetRandomTarget();
        
            yield return MoveToTarget(targetBoundary.position);
            yield return new WaitForSeconds(2f);
        
            currentState = GhostState.CastingSpell;
        }
    }

    private void GetRandomTarget()
    {
        targetIndex = Random.Range(0, 2);
        targetBoundary = targetIndex == 0 ? leftBoundary : rightBoundary;
    }
    
    IEnumerator MoveToTarget(Vector3 targetPosition)
    {
        if (targetPosition.x > transform.position.x)
        {
            direction = 1;
        }
        else
        {
            direction = -1;
        }
        
        while (Vector3.Distance(transform.position, targetPosition) > arrivalThreshold)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

            yield return null;
        }

    }

    private IEnumerator CastingSpellState()
    {
        _animator.SetBool(CastingSpell, true);
        
        GameManager.Instance.PlayerManager.Sleep(targetIndex);
        AudioManager.Instance.PlaySE(SE.ScaryLaugh);
        var targetHand = GameManager.Instance.HandManager.GetHand(targetIndex);
        targetHand.EnableInput();
        yield return new WaitForSeconds(castingSpellDuration);
        
        currentState = GhostState.Idle;
    }
}
