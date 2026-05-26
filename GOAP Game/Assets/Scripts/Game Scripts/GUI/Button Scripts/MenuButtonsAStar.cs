using GOAP;
using System;
using UnityEngine;
using UnityEngine.UIElements;

public class MenuButtonsAStar : MonoBehaviour
{
    public GameObject menuObject;

    void Awake()
    {
        menuObject.SetActive(true);
        NPCGOAPHandler.RunGame(false);
    }

    public void Play()
    {
        menuObject.SetActive(false);
        NPCGOAPHandler.RunGame(true);
        if (PauseScriptAStar.escPressed)
        {
            Time.timeScale = 1f;
            PauseScriptAStar.escPressed = false;
            NPCGOAPHandler.gameRunning = true;
            EscapeKeyPause_AStar.stopInput = false;
        }
    }
}
