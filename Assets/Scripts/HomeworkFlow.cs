using UnityEngine;

namespace DefaultNamespace
{
    public class HomeworkFlow : MonoBehaviour
    {
        [SerializeField] private GameObject homeworkPrefab;

        private HomeworkCommandManager homeworkLeft;
        private HomeworkCommandManager homeworkRight;

        private void Start()
        {
            homeworkLeft = Instantiate(homeworkPrefab, transform).GetComponent<HomeworkCommandManager>();
            homeworkLeft.transform.position += new Vector3(-700, 0, 0);
            homeworkRight = Instantiate(homeworkPrefab, transform).GetComponent<HomeworkCommandManager>();
            homeworkRight.transform.position += new Vector3(700, 0, 0);
            homeworkRight.SetInputDictionary(KeyCode.J, KeyCode.K, KeyCode.L, KeyCode.I);
            homeworkLeft.SetOnWorkDone(LeftDone);
            homeworkRight.SetOnWorkDone(RightDone);
        }

        private void LeftDone()
        {
            homeworkRight.AddCommands(4);
        }

        private void RightDone()
        {
            homeworkLeft.AddCommands(4);
        }
    }
}