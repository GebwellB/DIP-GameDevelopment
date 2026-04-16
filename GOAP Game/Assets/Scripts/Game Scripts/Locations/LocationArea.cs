using UnityEngine;

public class LocationArea : MonoBehaviour
{
    [SerializeField] LocationInstance parentInstance;

    public LocationInstance GetPaarentInstance()
    {
        return parentInstance;
    }
}
