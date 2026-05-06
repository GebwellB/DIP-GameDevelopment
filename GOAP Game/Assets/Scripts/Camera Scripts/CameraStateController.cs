using UnityEngine;
using GOAP;

public class CameraStateController : MonoBehaviour
{
    public MonoBehaviour orbiterScript;
    public MonoBehaviour movementScript;

    bool firstRun = true;

    public float delay = 3f;

    void Start()
    {
        orbiterScript.enabled = true;
        movementScript.enabled = false;
    }

    private void Update()
    {
        if (NPCGOAPHandler.gameRunning && firstRun)
        {
            Invoke("SwitchToMovement", delay);
            firstRun = false;
        }
    }

    void SwitchToMovement()
    {
        orbiterScript.enabled = false;
        movementScript.enabled = true;
    }
}