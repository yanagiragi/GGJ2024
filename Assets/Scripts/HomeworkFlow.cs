﻿using System;
using UnityEngine;

namespace DefaultNamespace
{
    internal enum WorkType
    {
        Article,
        Command
    }

    public class HomeworkFlow : MonoBehaviour
    {
        [SerializeField] private GameObject homeworkPrefab;
        [SerializeField] private GameObject articlePrefab;
        private HomeworkArticle article;

        private HomeworkCommandManager homeworkLeft;
        private HomeworkCommandManager homeworkRight;

        private bool leftDone;

        private WorkType nowWorkType;
        private bool rightDone;

        private void Start()
        {
            article = Instantiate(articlePrefab, transform).GetComponent<HomeworkArticle>();
            article.SetOnWorkDone(ArticleDone);
            homeworkLeft = Instantiate(homeworkPrefab, transform).GetComponent<HomeworkCommandManager>();
            homeworkLeft.transform.position += new Vector3(-700, 0, 0);
            homeworkRight = Instantiate(homeworkPrefab, transform).GetComponent<HomeworkCommandManager>();
            homeworkRight.transform.position += new Vector3(700, 0, 0);
            homeworkRight.SetInputDictionary(KeyCode.J, KeyCode.K, KeyCode.L, KeyCode.I);
            homeworkLeft.SetOnWorkDone(LeftDone);
            homeworkRight.SetOnWorkDone(RightDone);
            SwitchWorkType(WorkType.Command);
        }

        private void Update()
        {
            if (leftDone && rightDone)
            {
                leftDone = false;
                rightDone = false;
                NextWork();
            }
        }

        private void NextWork()
        {
            switch (nowWorkType)
            {
                case WorkType.Article:
                    SwitchWorkType(WorkType.Command);
                    break;
                case WorkType.Command:
                    SwitchWorkType(WorkType.Article);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void SwitchWorkType(WorkType type)
        {
            nowWorkType = type;
            switch (type)
            {
                case WorkType.Article:
                    article.gameObject.SetActive(true);
                    homeworkLeft.gameObject.SetActive(false);
                    homeworkRight.gameObject.SetActive(false);
                    article.SetEnableInput(true);
                    article.NewArticle();
                    homeworkLeft.SetInputEnabled(false);
                    homeworkRight.SetInputEnabled(false);
                    break;
                case WorkType.Command:
                    article.gameObject.SetActive(false);
                    homeworkLeft.gameObject.SetActive(true);
                    homeworkRight.gameObject.SetActive(true);
                    article.SetEnableInput(false);
                    homeworkLeft.SetInputEnabled(true);
                    homeworkRight.SetInputEnabled(true);
                    homeworkLeft.AddCommands(4);
                    homeworkRight.AddCommands(4);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void LeftDone()
        {
            //homeworkRight.AddCommands(4);
            leftDone = true;
        }

        private void RightDone()
        {
            //homeworkLeft.AddCommands(4);
            rightDone = true;
        }

        private void ArticleDone()
        {
            SwitchWorkType(WorkType.Command);
        }
    }
}