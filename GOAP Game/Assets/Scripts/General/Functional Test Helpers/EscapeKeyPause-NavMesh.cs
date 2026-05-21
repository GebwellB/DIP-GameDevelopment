using GOAP;
using UnityEngine;
using UnityEngine.AI;

public class EscapeKeyPause_NavMesh : MonoBehaviour
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
