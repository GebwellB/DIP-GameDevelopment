using GOAP;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScreenButtons : MonoBehaviour
{
    public GameObject winScreen;
    public GameObject mainMenu;

    public void ReturnToMenu()
    {
        NPCGOAPHandler.gameRunning = false;
        Time.timeScale = 1f;
        winScreen.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        mainMenu.SetActive(true);
    }
}
