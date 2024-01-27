using System;
using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spirit : MonoBehaviour
{
    [Title("每次施展睡眠魔法次數")]
    [SerializeField] private int spellCount = 5;

    private float size;
    
    [Title("移動設定")]
    // 設定移動速度
    public float speed = 2f;

    // 定義移動方向，1 表示向右，-1 表示向左
    [SerializeField] private int direction = 1;

    public float idleDuration = 5f;

    [SerializeField] private Transform leftBoundary, rightBoundary;
    
    [Serializable]
    private enum GhostState
    {
        Idle,
        Moving,
        CastingSpell
    }

    [SerializeField] private GhostState currentState = GhostState.Idle;
    [SerializeField] private Player targetPlayer;


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
                case GhostState.Moving:
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
        
        float startTime = Time.time;
        Flip();
        
        while (Time.time - startTime < idleDuration)
        {
            // 獲取當前UI的位置
            Vector3 currentPosition = transform.position;

            // 更新UI的X軸位置，根據速度和方向
            currentPosition.x += speed * direction * Time.deltaTime;
            
            // 確保UI在設定的區間內移動
            currentPosition.x = Mathf.Clamp(currentPosition.x, leftBoundary.position.x, 
                rightBoundary.position.x);
            
            // 將更新後的位置應用到UI
            transform.position = currentPosition;

            // 檢查是否到達邊界，如果是則改變移動方向
            if (currentPosition.x >=  rightBoundary.position.x || currentPosition.x <= leftBoundary.position.x)
            {
                Flip();
            }
            yield return null;
        }
        
        currentState = GhostState.CastingSpell;
    }

    private IEnumerator MovingState()
    {
        // targetPlayer = PlayerManager.Instance.GetRandomPlayer();
        // Debug.Log("Moving State");
        //
        // Vector3 randomDestination = transform.position + Random.onUnitSphere * idleRange;
        // randomDestination.y = 0f; // 將 y 座標固定為0，保持在地面上
        //
        // while (Vector3.Distance(transform.position, randomDestination) > 0.1f)
        // {
        //     transform.position = Vector3.MoveTowards(transform.position, randomDestination, moveSpeed * Time.deltaTime);
        //     yield return null;
        // }
        //
        // currentState = GhostState.CastingSpell;
        yield return null;
    }

    private void Flip()
    {
        direction *= -1;
    }

    private IEnumerator CastingSpellState()
    {
        targetPlayer = PlayerManager.Instance.GetRandomPlayer();
        Debug.Log("Casting Spell State");

        float magicCount = 0f;

        while (magicCount < spellCount)
        {
            // 每秒觸發 targetPlayer.SetSleepAmount(0.1f)
            targetPlayer.AddSleepAmount(0.1f);

            magicCount += 1;
            yield return new WaitForSeconds(1f);
        }

        // 持續5秒後進入閒置階段
        
        currentState = GhostState.Idle;
    }
}
