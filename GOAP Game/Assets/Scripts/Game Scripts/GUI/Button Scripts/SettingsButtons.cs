using GOAP;
using System;
using UnityEngine;

public class SettingsButtons : MonoBehaviour
{
    public GameObject menuObject;
    public GameObject settingsmenuObject;

    public void CloseSettings()
    {
        settingsmenuObject.SetActive(false);
        menuObject.SetActive(true);
    }
}
