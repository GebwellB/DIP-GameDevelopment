using GOAP;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.Audio;
public class SpaceShipDropper : MonoBehaviour
{
    [SerializeField]
    private float dropSpeed = 2f;

    [SerializeField]
    private float targetYPosition = 0f;

    public AudioClip spaceshipLandingClip;

    public AudioSource audioMixer;

    private bool firstRun = true;

    private void Update()
    {
        if (NPCGOAPHandler.gameRunning)
        {
            if (firstRun)
            {
                PlayLandingSound();
                firstRun = false;
            }
            Vector3 position = transform.position;

            if (position.y > targetYPosition)
            {
                position.y -= dropSpeed * Time.deltaTime;

                if (position.y < targetYPosition)
                {
                    position.y = targetYPosition;
                }

                transform.position = position;
            }
        }
    }

    private void PlayLandingSound()
    {
        audioMixer.PlayOneShot(spaceshipLandingClip);
        if (audioMixer.isPlaying)
        {
            Debug.Log("Something Played");
        }
    }
}