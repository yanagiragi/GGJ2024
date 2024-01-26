using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public sealed class HandController : Singleton<HandController>, ILogger
{
    #region Properties
    public string Prefix => @"<HandController>";
    public int HitCount => _hitCount;
    #endregion

    #region Variables
    private int _hitCount;
    #endregion

    #region Public Methods
    public void IncreaseHitCount() => _hitCount += 1;
    #endregion
}
