using GOAP;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscapeKeyPause_AStar : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject mainMenu;

    public static bool stopInput = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!stopInput && NPCGOAPHandler.gameRunning && !PauseScriptAStar.escPressed)
            {
                stopInput = true;
                PauseScriptAStar.escPressed = true;
                NPCGOAPHandler.gameRunning = false;
                Time.timeScale = 0f;
                pauseMenu.SetActive(true);
            }
            else
            {
                stopInput = false;
                PauseScriptAStar.escPressed = false;
                NPCGOAPHandler.gameRunning = true;
                pauseMenu.SetActive(false);
                Time.timeScale = 1f;
            }
        }
    }
}
