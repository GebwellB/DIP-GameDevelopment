using UnityEngine;
using GOAP;

public class CloseIntro : MonoBehaviour
{
    public GameObject introText;
    public GameObject valueTracker;
    public GameObject spawnMoreAliensButton;
    public void ResumeGame()
    {
        valueTracker.SetActive(true);
        spawnMoreAliensButton.SetActive(true);
        Time.timeScale = 1f;
        NPCGOAPHandler.readyToSpawn = true;
        SFXManager.Instance.EnableSound();
        introText.SetActive(false);
    }
}
