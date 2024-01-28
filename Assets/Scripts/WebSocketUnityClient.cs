using UnityEngine;
using WebSocketSharp;

public class WebSocketUnityClient : MonoSingleton<WebSocketUnityClient>, ILogger
{
    public const string playerOne = @"Player 1";
    public const string playerTwo = @"Player 2";
    public string Prefix => "<WebSocket>";

    #region Variables
    [SerializeField] private string _ip = @"127.0.0.1";
    [SerializeField] private string _port = @"8765";
    [SerializeField, Header("ReadOnly")] private string _currentMessage;

    private WebSocket ws;
    #endregion

    #region Mono
    public void Start()
    {
        ws = new WebSocket($"ws://{_ip}:{_port}");
        ws.Connect();
        ws.OnMessage += _HandleReceivedMessage;
        ws.Send("login?id=unity");
    }

    public void Update()
    {
        if (_currentMessage.StartsWith("slap?id="))
        {
            if (GameManager.Instance == null || GameManager.Instance.HandManager == null)
            {
                this.Log($"HandManager is not ready. Abort.");
                _currentMessage = string.Empty;
                return;
            }

            var id = _currentMessage.Substring("slap?id=".Length);
            var hand = id switch
            {
                playerOne => GameManager.Instance.HandManager.GetHand(0),
                playerTwo => GameManager.Instance.HandManager.GetHand(1),
                _ => null
            };
            var slapPlayerIndex = id switch
            {
                playerOne => 0,
                playerTwo => 1,
                _ => -1
            };

            if (hand == null)
            {
                this.LogError($"Unable to perfrom slap for [{id}]! Abort.");
                _currentMessage = string.Empty;
                return;
            }

            hand.Slap(slapPlayerIndex);
            _currentMessage = string.Empty;
        }
    }

    public void OnDestroy()
    {
        if (ws != null)
        {
            ws.OnMessage -= _HandleReceivedMessage;
            ws.Close();
        }
    }
    #endregion

    #region Private Methods
    private void _HandleReceivedMessage(object sender, MessageEventArgs eventArgs)
    {
        var message = eventArgs.Data;
        this.Log($"Message Received from {((WebSocket)sender).Url}, Data : {eventArgs.Data}");
        _currentMessage = message; // deal main logic in Unity main thread
    }
    #endregion
}