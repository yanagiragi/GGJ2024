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
    
    public Player player1, player2;

    public Player GetRandomPlayer()
    {
        var i = Random.Range(0,1);

        return i == 0 ? player1 : player2;
    }
}
