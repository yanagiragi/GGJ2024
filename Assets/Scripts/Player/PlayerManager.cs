using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerManager : MonoBehaviour
{
    #region Singleton

    private static PlayerManager instance;
    public static PlayerManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<PlayerManager>();
            }
            return instance;
        }
    }

    private void Awake()
    {
        instance = this;
    }

    #endregion

    public List<UIHand> players = new List<UIHand>();
    
    public IHand GetRandomPlayer()
    {
        var i = Random.Range(0, players.Count);
        return players[i];
    }

    public bool HaveAnySleepPlayer()
    {
        return players[0].IsEnableInput || players[1].IsEnableInput;
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <returns> Is Player 1</returns>
    public bool GetRandomNotSleepPlayer()
    {
        if (players[0].IsEnableInput)
        {
            return false;
        }

        if (players[1].IsEnableInput)
        {
            return true;
        }

        return Convert.ToBoolean(Random.Range(0, 2));
    }
    

    public UIHand GetPlayer1()
    {
        return players[0];
    }

    public UIHand GetPlayer2()
    {
        return players[1];
    }
}
