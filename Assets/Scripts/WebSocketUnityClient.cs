using UnityEngine;
using WebSocketSharp;

public class WebSocketUnityClient : MonoBehaviour
{
    [SerializeField] private string _ip = @"127.0.0.1";
    [SerializeField] private string _port = @"8765";

    private WebSocket ws;

    #region Mono
    public void Start()
    {
        ws = new WebSocket($"ws://{_ip}:{_port}");
        ws.Connect();
        ws.OnMessage += (sender, e) =>
        {
            Debug.Log("Message Received from " + ((WebSocket)sender).Url + ", Data : " + e.Data);
        };
        ws.Send("login?id=unity");
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (ws == null)
            {
                Debug.LogError("ws is null"); ;
                return;
            }

            Debug.Log("Send");
            ws.Send("Hello");
        }
    }

    public void OnDestroy()
    {
        ws.Close();
    }
    #endregion
}