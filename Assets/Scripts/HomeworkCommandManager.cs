using System.Collections.Generic;
using DefaultNamespace.Homework;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class HomeworkCommandManager : MonoBehaviour, ILogger
{
    [SerializeField] private bool isShowImage;

    [SerializeField] private bool isShowText;
    [SerializeField] private GameObject ArrowPrefab;
    [SerializeField] private float gap = 20;

    private readonly Queue<int> commands = new();

    private Image _image;
    private TMP_Text _text;

    private Dictionary<int, KeyCode> inputDictionary = new()
    {
        { 1, KeyCode.A },
        { 2, KeyCode.S },
        { 3, KeyCode.D },
        { 4, KeyCode.W }
    };

    private bool isInputEnabled = true;
    public string Prefix => "<CommandManager> ";

    private void UpdateAppear()
    {
        // if (commands.Count > 0)
        //     switch (commands.Peek())
        //     {
        //         case 1:
        //             _image.color = Color.red;
        //             break;
        //         case 2:
        //             _image.color = Color.green;
        //             break;
        //         case 3:
        //             _image.color = Color.blue;
        //             break;
        //         case 4:
        //             _image.color = Color.yellow;
        //             break;
        //     }
        // else
        //     _image.color = Color.white;
        //
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
                if (commands.Count == 0)
                {
                    onWorkDone?.Invoke();
                    AudioManager.Instance.PlaySE(SE.GetPoint);
                }
                else
                {
                    AudioManager.Instance.PlaySE(SE.Arrow);
                }
                
                // AddCommands(4);
                UpdateArrow();
                
                return;
            }
        
        
    }


    #region mono

    private void Awake()
    {
        _image = GetComponent<Image>();
        _text = GetComponentInChildren<TMP_Text>();
        isShowImage = true;
        isShowText = false;
    }

    private void Start()
    {
        var count = 4;
        AddCommands(count);
        SetInputEnabled(true);
        InitArrow(count);
        SetArrowPosition();
        UpdateArrow();
    }

    #region arrow

    private readonly List<Arrow> arrows = new();

    public void InitArrow(int count)
    {
        for (var i = 0; i < count; i++)
        {
            var arrow = Instantiate(ArrowPrefab, transform);

            arrows.Add(arrow.GetComponent<Arrow>());
        }
    }

    private void SetArrowPosition()
    {
        var arrowWidth = 100;
        for (var i = 0; i < arrows.Count; i++)
            arrows[i].transform.position = transform.position + new Vector3(0, -(arrowWidth * i + gap * i), 0);
    }

    private void UpdateArrow()
    {
        for (var i = 0; i < arrows.Count; i++)
            if (i >= commands.Count)
            {
                arrows[i].gameObject.SetActive(false);
            }
            else
            {
                arrows[i].gameObject.SetActive(true);
                arrows[i].SetDirection((Direction)commands.ToArray()[i]);
            }
    }

    #endregion

    // Update is called once per frame
    private void Update()
    {
        if (!isInputEnabled) return;
        UpdateAppear();
        GetInput();

        // if (Input.GetKeyDown(KeyCode.Space)) SetInputEnabled(!isInputEnabled);
    }

    #endregion

    #region api

    public void AddCommands(int count)
    {
        for (var i = 0; i < count; i++) commands.Enqueue(Random.Range(1, 5));
        UpdateArrow();
    }

    public void SetInputDictionary(KeyCode a, KeyCode s, KeyCode d, KeyCode w)
    {
        inputDictionary = new Dictionary<int, KeyCode>
        {
            { 1, a },
            { 2, s },
            { 3, d },
            { 4, w }
        };
    }

    public void SetInputEnabled(bool enabled)
    {
        _image.enabled = enabled && isShowImage;
        _text.enabled = enabled && isShowText;
        isInputEnabled = enabled;
    }

    #endregion

    #region translateType

    private List<string> translate(Queue<int> queue)
    {
        var result = new List<string>();
        foreach (var com in queue)
            result.Add(inputDictionary[com].ToString());
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

    #endregion

    #region workdone

    private UnityAction onWorkDone;

    public void SetOnWorkDone(UnityAction action)
    {
        onWorkDone += action;
    }

    #endregion
}