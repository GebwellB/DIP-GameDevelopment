using UnityEngine;

public class PersistentMusic : MonoBehaviour
{
    public static GameObject audioSingleton;

    void Awake()
    {
        GameObject[] musicObjs = GameObject.FindGameObjectsWithTag("Music");
        if (audioSingleton == null)
        {
            audioSingleton = gameObject;
            DontDestroyOnLoad(gameObject);
        }
        else if (audioSingleton != null
            && audioSingleton != this.gameObject
            && musicObjs.Length > 1)
        {
            Destroy(gameObject);
        }
    }
}
