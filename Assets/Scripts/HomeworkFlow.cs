using System;
using Homework;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DefaultNamespace
{
    internal enum WorkType
    {
        Article,
        Command,
        River
    }

    public class HomeworkFlow : MonoBehaviour
    {
        [SerializeField] private GameObject homeworkPrefab;
        [SerializeField] private GameObject articlePrefab;
        [SerializeField] private GameObject riverPrefab;
        [SerializeField] private Transform homeWorkSpawnPos;
        private HomeworkArticle article;

        private HomeworkCommandManager homeworkLeft;
        private HomeworkCommandManager homeworkRight;

        private bool leftDone;

        private WorkType nowWorkType;
        private bool rightDone;

        private HwRiver river;

        private void Start()
        {
            article = Instantiate(articlePrefab, homeWorkSpawnPos).GetComponent<HomeworkArticle>();
            article.SetOnWorkDone(ArticleDone);
            article.SetOnWordDone(WordDone);
            homeworkLeft = Instantiate(homeworkPrefab, transform).GetComponent<HomeworkCommandManager>();
            homeworkLeft.transform.position += new Vector3(-700, 0, 0);
            homeworkRight = Instantiate(homeworkPrefab, transform).GetComponent<HomeworkCommandManager>();
            homeworkRight.transform.position += new Vector3(700, 0, 0);
            homeworkRight.SetInputDictionary(KeyCode.J, KeyCode.K, KeyCode.L, KeyCode.I);
            homeworkLeft.SetOnWorkDone(LeftDone);
            homeworkRight.SetOnWorkDone(RightDone);

            river = Instantiate(riverPrefab, transform).GetComponent<HwRiver>();
            river.SetOnDoneAction(RiverDone);

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

            if (GameManager.Instance.PlayerManager.IsAnyPlayerSlept())
            {
                if (GameManager.Instance.PlayerManager.GetSleptPlayer().chair.sprite.name == "Chair_Left")
                    homeworkLeft.SetInputEnabled(false);
                else
                    homeworkRight.SetInputEnabled(false);
            }
            else
            {
                homeworkRight.SetInputEnabled(true);

                homeworkLeft.SetInputEnabled(true);
            }
        }

        private void NextWork()
        {
            var random = Random.Range(0, 3);
            switch (random)
            {
                case 0:
                    SwitchWorkType(WorkType.Article);
                    break;
                case 1:
                    SwitchWorkType(WorkType.Command);
                    break;
                case 2:
                    SwitchWorkType(WorkType.River);
                    break;
            }
        }

        private void SwitchWorkType(WorkType type)
        {
            nowWorkType = type;
            switch (type)
            {
                case WorkType.Article:
                    article.gameObject.SetActive(true);
                    river.gameObject.SetActive(false);
                    homeworkLeft.gameObject.SetActive(false);
                    homeworkRight.gameObject.SetActive(false);
                    // article.SetEnableInput(true);
                    article.NewArticle();
                    // homeworkLeft.SetInputEnabled(false);
                    // homeworkRight.SetInputEnabled(false);
                    break;
                case WorkType.Command:
                    article.gameObject.SetActive(false);
                    river.gameObject.SetActive(false);
                    homeworkLeft.gameObject.SetActive(true);
                    homeworkRight.gameObject.SetActive(true);
                    // article.SetEnableInput(false);
                    // homeworkLeft.SetInputEnabled(true);
                    // homeworkRight.SetInputEnabled(true);
                    homeworkLeft.AddCommands(4);
                    homeworkRight.AddCommands(4);
                    break;
                case WorkType.River:
                    river.gameObject.SetActive(true);
                    article.gameObject.SetActive(false);
                    homeworkLeft.gameObject.SetActive(false);
                    homeworkRight.gameObject.SetActive(false);
                    river.Init();
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void LeftDone()
        {
            //homeworkRight.AddCommands(4);
            AddScore(5);
            leftDone = true;
        }

        private void RightDone()
        {
            //homeworkLeft.AddCommands(4);
            AddScore(5);
            rightDone = true;
        }

        private void ArticleDone()
        {
            AddScore(8);
            SwitchWorkType(WorkType.Command);
        }

        private void WordDone()
        {
            AddScore(1);
        }

        private void RiverDone()
        {
            AddScore(5);
            NextWork();
        }

        private void AddScore(int score)
        {
            ScoreManager.Instance.AddScore(score);
        }
    }
}