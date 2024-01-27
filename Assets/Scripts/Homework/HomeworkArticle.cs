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
            List<string> article = new();
            article.Add("cold");
            article.Add("ice");
            article.Add("snow");
            article.Add("wind");
            article.Add("a land");
            article.Add("wide");
            article.Add("deep");
            article.Add("walk");
            article.Add("sleep");
            article.Add("awake");
            article.Add("look");
            article.Add("in cold");
            article.Add("in ice");
            article.Add("alone");
            article.Add("on and on");
            article.Add("in silence");
            article.Add("in cake");
            article.Add("cold");
            article.Add("cold");
            article.Add("cold");
            articles.Add(article);
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
    }
}