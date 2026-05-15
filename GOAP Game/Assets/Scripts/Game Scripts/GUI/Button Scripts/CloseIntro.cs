using UnityEngine;
using GOAP;

public class CloseIntro : MonoBehaviour
{
    public GameObject introText;
    public GameObject valueTracker;
    public GameObject spawnMoreAliensButton;
    public void ResumeGame()
    {
        Time.timeScale = 1f;
        valueTracker.SetActive(true);
        spawnMoreAliensButton.SetActive(true);
        NPCGOAPHandler.readyToSpawn = true;
        SFXManager.Instance.EnableSound();
        introText.SetActive(false);
    }
}
