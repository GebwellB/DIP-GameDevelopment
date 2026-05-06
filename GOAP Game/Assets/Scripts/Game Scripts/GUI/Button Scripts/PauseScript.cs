using UnityEngine;
using GOAP;
using UnityEngine.SceneManagement;

public class PauseScript : MonoBehaviour
{
    public GameObject pauseButton;
    public GameObject pauseMenu;
    public GameObject mainMenu;

    public void PlayPauseGame()
    {
        if (NPCGOAPHandler.gameRunning)
        {
            NPCGOAPHandler.gameRunning = false;
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            NPCGOAPHandler.gameRunning = true;
            pauseMenu.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    public void ReturnToMenu()
    {
        NPCGOAPHandler.gameRunning = false;
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        mainMenu.SetActive(true);
    }
}
