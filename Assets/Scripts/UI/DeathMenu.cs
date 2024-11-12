using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Debug.Log("Quits Game.");
        Application.Quit();
    }
}
