using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class UIHandInput : MonoBehaviour
{
    [SerializeField] private UIHand playerOneHand;
    [SerializeField] private KeyCode playerOneHandInput = KeyCode.Z;
    [SerializeField] private UIHand playerTwoHand;
    [SerializeField] private KeyCode playerTwoandInput = KeyCode.Backslash;
    public void Update()
    {
        playerOneHand.Resolve(Input.GetKeyDown(playerOneHandInput));
        playerTwoHand.Resolve(Input.GetKeyDown(playerTwoandInput));
    }
}
