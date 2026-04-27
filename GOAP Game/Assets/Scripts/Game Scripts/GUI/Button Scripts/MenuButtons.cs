using GOAP;
using System;
using UnityEngine;
using UnityEngine.UIElements;

public class MenuButtons : MonoBehaviour
{
    public GameObject menuObject;
    public GameObject settingsmenuObject;
    public GameObject pauseButton;
    public ValueTracker valueTracker;
    void Awake()
    {
        pauseButton.SetActive(false);
        menuObject.SetActive(true);
        valueTracker.SetAllEntries(false);
        NPCGOAPHandler.RunGame(false);
    }

    public void Play()
    {
        menuObject.SetActive(false);
        valueTracker.SetAllEntries(true);
        pauseButton.SetActive(true);
        NPCGOAPHandler.RunGame(true);
    }

    public void OpenSettings()
    {
        menuObject.SetActive(false);
        settingsmenuObject.SetActive(true);
    }
}
