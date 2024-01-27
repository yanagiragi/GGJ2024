using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkHandler : MonoBehaviour, ILogger
{
    #region Defines
    //Mark as Serializable to make Unity's JsonUtility works.
    [System.Serializable]
    public class ReturnResult
    {
        public string msg;
        public int code;
    }
    #endregion

    #region Properties
    public string Prefix => "<Network>";
    #endregion

    #region Variables
    public const string playerId1 = @"playerOne";
    public const string playerId2 = @"playerTwo";

    public const string ok = "OK";
    #endregion

    #region APIs
    public ReturnResult HeartBeat()
    {
        this.Log($"Receive request: /HeartBeat");

        return new ReturnResult { msg = "I'm Alive!", code = 200 };
    }

    public ReturnResult Slap(string playerId)
    {
        this.Log($"Receive request: /Slap?playerId={playerId}");

        var message = playerId switch
        {
            playerId1 => "receive player one slap!",
            playerId2 => "receive player two slap!",
            _ => "receive player unkown slap!",
        };

        return new ReturnResult { msg = message, code = 200 };
    }
    #endregion
}
