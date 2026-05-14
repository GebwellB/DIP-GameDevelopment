using GOAP;
using System;
using UnityEngine;
using UnityEngine.UIElements;

public class MenuButtons : MonoBehaviour
{
    public GameObject menuObject;
    public GameObject settingsmenuObject;
    public GameObject pauseButton;
    public GameObject pauseMenu;
    public ValueTracker valueTracker;

    public static bool openedFromGamePlay = false;

    void Awake()
    {
        pauseButton.SetActive(false);
        menuObject.SetActive(true);
        valueTracker.SetAllEntries(false);
        NPCGOAPHandler.RunGame(false);
    }

    public void Play()
    {
        NPCGOAPHandler.RunGame(true);
        SFXManager.Instance.EnableSound();
        menuObject.SetActive(false);
        valueTracker.SetAllEntries(true);
        pauseButton.SetActive(true);
    }

    public void OpenSettings()
    {
        SettingsButtons.openedFromPauseMenu = false;
        menuObject.SetActive(false);
        settingsmenuObject.SetActive(true);
    }
}
