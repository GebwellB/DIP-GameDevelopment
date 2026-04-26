using UnityEngine;

public class MenuOrbitCamera : MonoBehaviour
{
    public Transform target;          // Point to rotate around
    public float rotationSpeed = 30f; // Degrees per second
    public GameObject menuObject;

    [Header("Default (Menu Off) View")]
    public Vector3 defaultPosition;
    public Quaternion defaultRotation;
    public float defaultFOV = 60f;

    [Header("Orbit (Menu On) View")]
    public Vector3 orbitStartingPosition;
    public Quaternion orbitStartingRotation;
    public float orbitFOV = 8.5f;

    [Header("Transition")]
    public float transitionSpeed = 1f;

    private Camera cam;

    private void Start()
    {
        cam = GetComponent<Camera>();

        cam.fieldOfView = orbitFOV;
        transform.position = orbitStartingPosition;
        transform.rotation = orbitStartingRotation;
    }

    void Update()
    {
        if (menuObject.activeInHierarchy && target != null)
        {
            transform.RotateAround(target.position, Vector3.up, rotationSpeed * Time.unscaledDeltaTime);

            transform.LookAt(target);
        }
        else
        {
            transform.position = Vector3.Lerp(
                transform.position,
                defaultPosition,
                Time.unscaledDeltaTime * transitionSpeed
            );

            transform.rotation = Quaternion.Lerp(
                transform.rotation,
                defaultRotation,
                Time.unscaledDeltaTime * transitionSpeed
            );

            cam.fieldOfView = Mathf.Lerp(
                cam.fieldOfView,
                defaultFOV,
                Time.unscaledDeltaTime * transitionSpeed
            );
        }
    }
}