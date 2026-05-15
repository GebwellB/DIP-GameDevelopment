using UnityEngine;
using GOAP;
using UnityEngine.SceneManagement;

public class PauseScript : MonoBehaviour
{
    public GameObject pauseButton;
    public GameObject pauseMenu;
    public GameObject settingsMenu;
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
            EscapeKeyPause.stopInput = false;
            NPCGOAPHandler.gameRunning = true;
            pauseMenu.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    public void ReturnToMenu()
    {
        NPCGOAPHandler.gameRunning = false;
        NPCGOAPHandler.readyToSpawn = false;
        EscapeKeyPause.stopInput = false;
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        mainMenu.SetActive(true);
    }

    public void OpenSettings()
    {
        SettingsButtons.openedFromPauseMenu = true;
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }
}
