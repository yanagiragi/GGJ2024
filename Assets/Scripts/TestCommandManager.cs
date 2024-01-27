using UnityEngine;

namespace DefaultNamespace
{
    public class TestCommandManager : MonoBehaviour
    {
        private void Start()
        {
            var cm = FindObjectOfType<HomeworkCommandManager>();
            cm.SetOnWorkDone(WorkDone);
            cm.SetInputDictionary(KeyCode.J, KeyCode.K, KeyCode.L, KeyCode.I);
        }

        private void WorkDone()
        {
            Debug.Log("Work done!");
        }
    }
}