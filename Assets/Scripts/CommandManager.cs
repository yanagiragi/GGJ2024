using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CommandManager : MonoBehaviour, ILogger
{
    [SerializeField] private bool isShowImage;

    [SerializeField] private bool isShowText;

    private readonly Queue<int> commands = new();

    private readonly Dictionary<int, KeyCode> inputDictionary = new()
    {
        { 1, KeyCode.A },
        { 2, KeyCode.S },
        { 3, KeyCode.D },
        { 4, KeyCode.W }
    };

    private Image _image;
    private TMP_Text _text;
    private bool isInputEnabled = true;


    private void Awake()
    {
        _image = GetComponent<Image>();
        _text = GetComponentInChildren<TMP_Text>();
        isShowImage = true;
        isShowText = true;
    }

    private void Start()
    {
        for (var i = 0; i < 10; i++) commands.Enqueue(Random.Range(1, 5));
        SetInputEnabled(true);
    }

    // Update is called once per frame
    private void Update()
    {
        if (isInputEnabled)
        {
            UpdateAppear();
            GetInput();
        }

        if (Input.GetKeyDown(KeyCode.Space)) SetInputEnabled(!isInputEnabled);
    }

    public string Prefix => "<CommandManager> ";

    public void SetInputEnabled(bool enabled)
    {
        _image.enabled = enabled && isShowImage;
        _text.enabled = enabled && isShowText;
        isInputEnabled = enabled;
    }

    private void UpdateAppear()
    {
        if (commands.Count > 0)
            switch (commands.Peek())
            {
                case 1:
                    _image.color = Color.red;
                    break;
                case 2:
                    _image.color = Color.green;
                    break;
                case 3:
                    _image.color = Color.blue;
                    break;
                case 4:
                    _image.color = Color.yellow;
                    break;
            }
        else
            _image.color = Color.white;

        _text.text = ToString(translate(commands));
    }

    private void GetInput()
    {
        if (commands.Count <= 0)
            return;
        foreach (var pair in inputDictionary)
            if (Input.GetKeyDown(pair.Value) &&
                commands.Peek() == pair.Key)
            {
                commands.Dequeue();
                this.Log(ToString(commands));
            }
    }

    private List<string> translate(Queue<int> queue)
    {
        var result = new List<string>();
        foreach (var com in queue)
            if (com == 1)
                result.Add("A");
            else if (com == 2)
                result.Add("S");
            else if (com == 3)
                result.Add("D");
            else if (com == 4) result.Add("W");
        return result;
    }

    public static string ToString<T>(List<T> list)
    {
        var output = "";
        foreach (var item in list) output += item + " ";

        return output;
    }

    public static string ToString<T>(Queue<T> list)
    {
        var output = "";
        foreach (var item in list) output += item + " ";

        return output;
    }
}