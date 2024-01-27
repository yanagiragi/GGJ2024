using System;
using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spirit : MonoBehaviour
{
    [SerializeField] private float idleRange = 5f;
    [SerializeField] private float moveSpeed = 2f;
    [Title("每次施展睡眠魔法次數")]
    [SerializeField] private int spellCount = 5;

    [Serializable]
    private enum GhostState
    {
        Idle,
        Moving,
        CastingSpell
    }

    [SerializeField] private GhostState currentState = GhostState.Idle;
    [SerializeField] private IHand targetPlayer;


    private void Start()
    {
        StartCoroutine(GhostStateMachine());
    }

    private IEnumerator GhostStateMachine()
    {
        while (true)
        {
            switch (currentState)
            {
                case GhostState.Idle:
                    yield return StartCoroutine(IdleState());
                    break;
                case GhostState.Moving:
                    yield return StartCoroutine(MovingState());
                    break;
                case GhostState.CastingSpell:
                    yield return StartCoroutine(CastingSpellState());
                    break;
            }
        }
    }

    private IEnumerator IdleState()
    {
        Debug.Log("Idle State");
        yield return new WaitForSeconds(5f); // 每隔5秒進入移動狀態
        currentState = GhostState.Moving;
    }

    private IEnumerator MovingState()
    {
        targetPlayer = PlayerManager.Instance.GetRandomPlayer();
        Debug.Log("Moving State");

        Vector3 randomDestination = transform.position + Random.onUnitSphere * idleRange;
        randomDestination.y = 0f; // 將 y 座標固定為0，保持在地面上

        while (Vector3.Distance(transform.position, randomDestination) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, randomDestination, moveSpeed * Time.deltaTime);
            yield return null;
        }

        currentState = GhostState.CastingSpell;
    }

    private IEnumerator CastingSpellState()
    {
        Debug.Log("Casting Spell State");

        float magicCount = 0f;

        while (magicCount < spellCount)
        {
            // 每秒觸發 targetPlayer.SetSleepAmount(0.1f)
            targetPlayer.EnableInput();

            magicCount += Time.deltaTime;
            yield return new WaitForSeconds(5f);
        }

        // 持續5秒後進入閒置階段

        currentState = GhostState.Idle;
    }
}
