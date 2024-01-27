using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour
{

    [SerializeField] private UIHand playerOneHand;
    [SerializeField] private KeyCode playerOneHandInput = KeyCode.Z;
    [SerializeField] private UIHand playerTwoHand;
    [SerializeField] private KeyCode playerTwoandInput = KeyCode.Backslash;

    private List<IHand> players = new List<IHand>();

    public void Start()
    {
        GameManager.Instance.HandController = this;

        players.Add(playerTwoHand);
        players.Add(playerOneHand);
    }

    public IHand GetRandomHand()
    {
        var i = Random.Range(0, players.Count);
        return players[i];
    }

    public IHand GetHand(int index)
    {
        return players[index];
    }

    public void Update()
    {
        playerOneHand.Resolve(Input.GetKeyDown(playerOneHandInput));
        playerTwoHand.Resolve(Input.GetKeyDown(playerTwoandInput));
    }
}
