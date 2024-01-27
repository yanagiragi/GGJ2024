using System;
using System.Collections;
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
    // 設定移動速度
    public float speed = 2f;

    // 定義移動方向，1 表示向右，-1 表示向左
    [SerializeField] private int direction = 1;
    
    [SerializeField] private Transform leftBoundary, rightBoundary;
    private Transform targetBoundary;
    private IHand targetHand;
    
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
            // TODO Have Some Problem
            
            yield return null;
        }
        
        currentState = GhostState.MoveToTarget;
    }

    private IEnumerator MovingState()
    {
        _animator.SetBool(CastingSpell, false);

        // 如果有任何玩家睡著，回到 Idle 狀態
        if (PlayerManager.Instance.HaveAnySleepPlayer())
        {
            currentState = GhostState.Idle;
        }
        else
        {
            GetRandomTarget();
        
            yield return MoveToTarget(targetBoundary.position);
            yield return new WaitForSeconds(1f);
        
            currentState = GhostState.CastingSpell;
        }
    }

    private void GetRandomTarget()
    {
        bool isPlayer1 = PlayerManager.Instance.GetRandomNotSleepPlayer();
        Debug.Log($"isPlayer1 {isPlayer1}");
        
        targetHand = isPlayer1 ? PlayerManager.Instance.GetPlayer1() : PlayerManager.Instance.GetPlayer2();
        targetBoundary = isPlayer1 ? leftBoundary : rightBoundary;
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
        
        targetHand.EnableInput();
        yield return new WaitForSeconds(castingSpellDuration);
        
        currentState = GhostState.Idle;
    }
}
