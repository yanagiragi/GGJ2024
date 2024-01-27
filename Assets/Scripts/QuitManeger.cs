using UnityEngine;

namespace DefaultNamespace
{
    public class QuitManeger : MonoBehaviour
    {
        private void Start()
        {
            DontDestroyOnLoad(this);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                QuitGame();
        }

        public void QuitGame()
        {
            Debug.Log("quit");
            Application.Quit();
        }
    }
}