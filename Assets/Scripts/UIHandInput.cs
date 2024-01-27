using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class UIHandInput : MonoBehaviour
{
    [SerializeField] private UIHand playerOneHand;
    [SerializeField] private string playerOneHandInput = "space";

    public void Update()
    {
        playerOneHand.Resolve(Input.GetKeyDown(playerOneHandInput));
    }
}
