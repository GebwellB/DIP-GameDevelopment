using GOAP;
using UnityEngine;
using UnityEngine.Audio;

public class ShowNPCItems : MonoBehaviour
{
    private Inventory inv;

    [Header("Audio")]
    [SerializeField] private AudioClip eatingSound;
    [SerializeField] private AudioMixerGroup sfxGroup;
    private AudioSource audioSource;

    [Header("Items")]
    [SerializeField] private Item bucketItem;
    [SerializeField] private Item stolenEggsItem;
    [SerializeField] private Item chickenFeed;

    [Header("Visuals")]
    [SerializeField] private GameObject bucketObject;
    [SerializeField] private GameObject stolenEggsObject;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        audioSource.outputAudioMixerGroup = sfxGroup;

        inv = GetComponent<Inventory>();
    }

    void Update()
    {
        if (inv == null)
            return;

        if(inv.FindInInventory(bucketItem) != null)
        {
            bucketObject.SetActive(true);
            stolenEggsObject.SetActive(false);
        }
        if(inv.FindInInventory(stolenEggsItem) != null)
        {
            bucketObject.SetActive(true);
            stolenEggsObject.SetActive(true);
        }
        else
        {
            if(inv.FindInInventory(bucketItem) != null)
            {
                bucketObject.SetActive(true);
            }
            else
            {
                bucketObject.SetActive(false);
            }
            stolenEggsObject.SetActive(false);
        }

        if(inv.FindInInventory(chickenFeed) != null)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(eatingSound);
            }
        }
    }
}