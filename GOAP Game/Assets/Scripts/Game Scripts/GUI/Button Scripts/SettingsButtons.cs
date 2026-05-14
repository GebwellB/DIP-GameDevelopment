using GOAP;
using System;
using UnityEngine;

public class SettingsButtons : MonoBehaviour
{
    public GameObject menuObject;
    public GameObject pauseObject;
    public GameObject settingsmenuObject;

    public static bool openedFromPauseMenu;

    public void CloseSettings()
    {
        if (openedFromPauseMenu)
        {
            settingsmenuObject.SetActive(false);
            pauseObject.SetActive(true);
        }
        else
        {
            settingsmenuObject.SetActive(false);
            menuObject.SetActive(true);
        } 
    }
}
