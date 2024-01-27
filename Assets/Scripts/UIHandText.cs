using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIHandText : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private UIHand _hand;


    void Update()
    {
        _text.text = $"CHARGE: {_hand.ChargeCount}";
    }
}
