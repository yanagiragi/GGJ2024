using UnityEngine;

namespace DefaultNamespace
{
    public class QuitManager : MonoSingleton<QuitManager>
    {
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