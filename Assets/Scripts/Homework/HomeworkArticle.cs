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

        private List<string> nowArticle = new();

        private UnityAction onDone;
        private int wordColIndex;
        private int wordRowIndex;

        private void Awake()
        {
            _text = GetComponent<TMP_Text>();
            articles.Add(article1);
            articles.Add(article2);
            articles.Add(article3);
            NewArticle();
            UpdateText();
        }

        private void Start()
        {
        }

        private void Update()
        {
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
                        onDone.Invoke();
                    }
                }

                UpdateText();
            }
        }

        public void NewArticle()
        {
            nowArticle = articles[Random.Range(0, articles.Count)];
            wordRowIndex = 0;
            wordColIndex = 0;
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

        #region data

        private readonly List<string> article1 = new()
        {
            "cold", "ice", "snow", "wind", "a land", "wide", "deep", "walk",
            "sleep", "awake", "look", "in cold", "in ice", "alone",
            "on and on", "in silence", "in cake", "cold", "cold", "cold"
        };

        private readonly List<string> article2 = new()
        {
            "wind", "sock", "jam", "zip", "mix",
            "wax", "cake", "limp", "dock",
            "pond", "link", "mask",
            "joke", "coal", "swan", "dim",
            "pack", "mole", "lap", "kind",
            "scan", "pick", "wand"
        };

        private readonly List<string> article3 = new()
        {
            "milk", "walk", "land", "lake",
            "soap", "scan", "wind", "swim",
            "mind", "pick", "kind", "joke",
            "desk", "pack", "lamp", "loan",
            "mask", "link", "lion", "cold"
        };

        #endregion
    }
}