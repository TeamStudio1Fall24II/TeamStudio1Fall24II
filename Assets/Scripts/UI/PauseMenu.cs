using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
     public bool isPaused = false;
     public Player player;
     public GameObject MenuUI;

     public void TogglePause()
     {
          if(!isPaused)
          {
               PauseGame();
          }
          else
          {
               ResumeGame();
          }
     }

     public void PauseGame()
     {
          MenuUI.SetActive(true);
          Cursor.lockState = CursorLockMode.None;
          Cursor.visible = true;
          player.m_Controller.disableControls = true;
          Time.timeScale = 0.0f;
          isPaused = true;
     }

    public void ResumeGame()
    {
          MenuUI.SetActive(false);
          Cursor.lockState = CursorLockMode.Locked;
          Cursor.visible = false;
          player.m_Controller.disableControls = false;
          Time.timeScale = 1.0f;
          isPaused = false;
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
