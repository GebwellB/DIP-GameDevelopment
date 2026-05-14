using UnityEngine;
using GOAP;

public class CameraStateController : MonoBehaviour
{
    public MonoBehaviour orbiterScript;
    public MonoBehaviour movementScript;
    public MonoBehaviour playerChickenController;
    public GameObject introText;
    public GameObject valueTracker;

    public static bool cameraLocked = true;

    bool firstRun = true;

    public float delay = 3f;

    void Start()
    {
        orbiterScript.enabled = true;
        movementScript.enabled = false;
        playerChickenController.enabled = false;
    }

    private void Update()
    {
        if (NPCGOAPHandler.gameRunning && firstRun)
        {
            valueTracker.SetActive(false);
            Invoke("SwitchToMovement", delay);
            firstRun = false;
        }
    }

    void SwitchToMovement()
    {
        orbiterScript.enabled = false;
        movementScript.enabled = true;
        playerChickenController.enabled = true;
        introText.SetActive(true);
        Time.timeScale = 0f;
        cameraLocked = false;
    }
}