using System.Collections.Generic;
using UnityEngine;

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

    public UIHand GetPlayer1()
    {
        return players[0];
    }

    public UIHand GetPlayer2()
    {
        return players[1];
    }
}
