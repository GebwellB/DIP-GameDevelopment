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
    }
}
