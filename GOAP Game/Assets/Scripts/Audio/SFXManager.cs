using TMPro;
using UnityEngine;
using UnityEngine.Audio;

namespace GOAP
{
    public class SFXManager : MonoBehaviour
    {
        public static SFXManager Instance;

        [SerializeField] private AudioMixerSnapshot mutedSnapshot;
        [SerializeField] private AudioMixerSnapshot activeSnapshot;

        public static bool sfxMuted = true;

        private void Start()
        {
            Debug.Log("SFXManager alive");
            DisableSound();
        }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        public void EnableSound()
        {
            Debug.Log("SFXManager switched to active");
            activeSnapshot.TransitionTo(0.1f);
            sfxMuted = false;
        }

        public void DisableSound()
        {
            Debug.Log("SFXManager switched to muted");
            mutedSnapshot.TransitionTo(0.1f);
            sfxMuted = true;
        }
    }
}