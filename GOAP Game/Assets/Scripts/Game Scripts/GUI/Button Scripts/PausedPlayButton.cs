using GOAP;
using System;
using UnityEngine;

public class PausedPlayButton : MonoBehaviour
{
    public GameObject menuObject;
    public GameObject testingText;
    [SerializeField] public bool isTesting;
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
