using GOAP;
using UnityEngine;

public class EscapeKeyPause : MonoBehaviour
{
    public static bool stopInput = false;

    public GameObject pauseMenu;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!stopInput && NPCGOAPHandler.gameRunning && NPCGOAPHandler.readyToSpawn)
            {
                NPCGOAPHandler.gameRunning = false;
                stopInput = true;
                pauseMenu.SetActive(true);
                Time.timeScale = 0f;
            }
        }
    }
}
