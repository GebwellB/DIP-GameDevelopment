using GOAP;
using System;
using UnityEngine;

public class PausedPlayButton : MonoBehaviour
{
    public GameObject menuObject;
    public GameObject testingText;
    public ValueTracker valueTracker;
    [SerializeField] public bool isTesting;
    void Awake()
    {
        menuObject.SetActive(true);
        valueTracker.SetAllEntries(false);
        NPCGOAPHandler.RunGame(false);
    }

    public void Play()
    {
        menuObject.SetActive(false);
        valueTracker.SetAllEntries(true);
        NPCGOAPHandler.RunGame(true);
    }
}
