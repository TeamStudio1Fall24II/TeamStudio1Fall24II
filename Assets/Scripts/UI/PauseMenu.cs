using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{   
    public void ResumeGame()
    {
        Time.timeScale = 1;
    }

    public void QuitGame()
    {
        Debug.Log("Quits Game.");
        Application.Quit();
    }
}
