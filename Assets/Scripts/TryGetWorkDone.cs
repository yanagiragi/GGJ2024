using UnityEngine;

namespace DefaultNamespace
{
    public class TryGetWorkDone : MonoBehaviour
    {
        private void Start()
        {
            FindObjectOfType<CommandManager>().SetOnWorkDone(WorkDone);
        }

        private void WorkDone()
        {
            Debug.Log("Work done!");
        }
    }
}