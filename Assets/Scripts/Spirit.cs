using System;
using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spirit : MonoBehaviour
{
    [Title("每次施展睡眠魔法次數")]
    [SerializeField] private int spellCount = 5;

    [SerializeField] private Animator _animator;

    private float size;
    private string CastingSpell = "CastingSpell";
    
    [Title("移動設定")]
    // 設定移動速度
    public float speed = 2f;

    // 定義移動方向，1 表示向右，-1 表示向左
    [SerializeField] private int direction = 1;

    public float idleDuration = 5f;

    [SerializeField] private Transform leftBoundary, rightBoundary;
    [SerializeField] private Transform targetBoundary;
    
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
    [SerializeField] private IHand targetPlayer;


    private void Start()
    {
        size = transform.localScale.x;
        StartCoroutine(GhostStateMachine());
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
        GetRandomTarget();
        
        yield return MoveToTarget(targetBoundary.position);

        yield return new WaitForSeconds(1f);
        
        currentState = GhostState.CastingSpell;
    }

    private void GetRandomTarget()
    {
        bool isPlayer1 = Convert.ToBoolean(Random.Range(0, 2));
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
        
        // targetPlayer = PlayerManager.Instance.GetRandomPlayer();

        float magicCount = 0f;

        while (magicCount < spellCount)
        {
            // 每秒觸發 targetPlayer.SetSleepAmount(0.1f)
            // targetPlayer.EnableInput();

            magicCount += 1;
            yield return new WaitForSeconds(1f);
        }

        // 持續5秒後進入閒置階段

        currentState = GhostState.Idle;
    }
}
