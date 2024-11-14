using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

public class VictoryMenu : MonoBehaviour
{
    public bool isPaused = false;
    public Player player;
    public GameObject MenuUI;


    public void OpenVictoryMenu()
    {
        MenuUI.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        player.m_Controller.disableControls = true;
        isPaused = true;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
          #if UNITY_EDITOR
          EditorApplication.ExitPlaymode();
          #endif
        Application.Quit();
    }
}
