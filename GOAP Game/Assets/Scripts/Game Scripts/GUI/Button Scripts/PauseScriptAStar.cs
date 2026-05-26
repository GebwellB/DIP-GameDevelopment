using GOAP;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScriptAStar : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject mainMenu;
    public static bool escPressed = false;

    public void ReturnToMenu()
    {
        NPCGOAPHandler.gameRunning = false;
        EscapeKeyPause_NavMesh.stopInput = false;
        Time.timeScale = 0f;
        pauseMenu.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        mainMenu.SetActive(true);
    }
}
