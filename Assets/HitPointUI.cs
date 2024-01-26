using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HitPointUI : MonoBehaviour
{
    #region Variables
    [SerializeField] TMP_Text _message;
    #endregion

    #region Mono
    void Update()
    {
        _message.text = $"HitCount: {HandController.Instance.HitCount}";
    }
    #endregion
}
