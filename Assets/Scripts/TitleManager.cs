using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    public void GotoMainScene()
    {
        SceneManager.LoadScene("_Scenes/_MainScene");
    }
}