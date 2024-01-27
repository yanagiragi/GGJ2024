using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace DefaultNamespace
{
    public class HomeworkArticle : MonoBehaviour
    {
        private readonly List<List<string>> articles = new();
        private TMP_Text _text;
        private bool enableInput;

        private List<string> nowArticle = new();

        private UnityAction onDone;
        private UnityAction onWordDone;
        private int wordColIndex;
        private int wordRowIndex;

        private void Awake()
        {
            _text = GetComponent<TMP_Text>();
            articles.Add(article1);
            articles.Add(article2);
            articles.Add(article3);
            articles.Add(article4);
            articles.Add(article5);
            articles.Add(article6);
            articles.Add(article7);
            articles.Add(article8);
            articles.Add(article9);
            articles.Add(article10);
            articles.Add(article11);
            articles.Add(article12);
            articles.Add(article13);
            articles.Add(article14);

            NewArticle();
            enableInput = true;
        }

        private void Start()
        {
        }

        private void Update()
        {
            if (!enableInput || GameManager.Instance.PlayerManager.IsAnyPlayerSlept()) return;

            var c = nowArticle[wordColIndex][wordRowIndex].ToString();
            var getkey = false;
            if (c == " ")
                getkey = Input.GetKeyDown(KeyCode.Space);
            else
                getkey = Input.GetKeyDown(c);
            if (getkey)
            {
                Debug.Log("getkey");
                wordRowIndex++;
                if (wordRowIndex >= nowArticle[wordColIndex].Length)
                {
                    wordColIndex++;
                    wordRowIndex = 0;
                    if (wordColIndex >= nowArticle.Count)
                    {
                        NewArticle();
                        onDone?.Invoke();
                    }

                    onWordDone?.Invoke();
                }

                UpdateText();
            }
        }

        public void SetEnableInput(bool enable)
        {
            enableInput = enable;
        }

        public void NewArticle()
        {
            nowArticle = articles[Random.Range(0, articles.Count)];
            wordRowIndex = 0;
            wordColIndex = 0;
            UpdateText();
        }

        private void UpdateText()
        {
            var result = "<color=red>";
            // _text.text = nowArticle.Aggregate("", (current, word) => current + word + "\n");
            for (var i = 0; i < nowArticle.Count; i++)
                if (i == wordColIndex)
                    for (var ii = 0; ii < nowArticle[i].Length; ii++)
                    {
                        if (wordColIndex == i && wordRowIndex == ii)
                            result += "</color>";
                        result += nowArticle[i][ii];
                    }
            // result += "\n";

            _text.text = result;
        }

        public void SetOnWordDone(UnityAction action)
        {
            onWordDone += action;
        }

        public void SetOnWorkDone(UnityAction action)
        {
            onDone += action;
        }

        #region data

        private readonly List<string> article1 = new()
        {
            "cold", "ice", "snow", "wind", "a land", "wide", "deep", "walk"
        };

        private readonly List<string> article4 = new()
        {
            "sleep", "awake", "look", "in cold", "in ice", "alone"
        };

        private readonly List<string> article5 = new()
        {
            "on and on", "in silence", "in cake", "cold", "cold", "cold"
        };

        private readonly List<string> article6 = new()
        {
            "wind", "sock", "jam", "zip", "mix"
        };

        private readonly List<string> article7 = new()
        {
            "wax", "cake", "limp", "dock"
        };

        private readonly List<string> article8 = new()
        {
            "pond", "link", "mask"
        };

        private readonly List<string> article9 = new()
        {
            "joke", "coal", "swan", "dim"
        };

        private readonly List<string> article10 = new()
        {
            "pack", "mole", "lap", "kind"
        };

        private readonly List<string> article11 = new()
        {
            "scan", "pick", "wand"
        };

        private readonly List<string> article3 = new()
        {
            "milk", "walk", "land", "lake"
        };

        private readonly List<string> article2 = new()
        {
            "soap", "scan", "wind", "swim"
        };

        private readonly List<string> article14 = new()
        {
            "mind", "pick", "kind", "joke"
        };

        private readonly List<string> article13 = new()
        {
            "desk", "pack", "lamp", "loan"
        };

        private readonly List<string> article12 = new()
        {
            "mask", "link", "lion", "cold"
        };

        #endregion
    }
}