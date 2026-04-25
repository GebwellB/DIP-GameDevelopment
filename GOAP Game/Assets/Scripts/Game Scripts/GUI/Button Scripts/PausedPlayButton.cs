using System;
using UnityEngine;

public class PausedPlayButton : MonoBehaviour
{
    public GameObject menuObject;
    public GameObject testingText;
    [SerializeField] public bool isTesting;
    void Awake()
    {
        if (isTesting)
        {
            Time.timeScale = 1;
            testingText.SetActive(true);
        }
        else
        {
            Time.timeScale = 0;
            testingText.SetActive(false);
        }
        
    }

    public void Play()
    {
        Time.timeScale = 1;
        menuObject.SetActive(false);
    }
}
