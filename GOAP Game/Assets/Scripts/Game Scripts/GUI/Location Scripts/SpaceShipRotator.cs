using UnityEngine;

public class SpaceShipRotator : MonoBehaviour
{
    [SerializeField]
    private float rotationSpeed = 20f;

    private void Update()
    {
        transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
    }
}
