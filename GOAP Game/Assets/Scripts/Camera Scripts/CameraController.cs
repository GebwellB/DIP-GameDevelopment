using UnityEngine;
using GOAP;

public class CameraController : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float rotateSpeed = 100f;
    public float zoomSpeed = 10f;

    public Transform cam;
    public Camera camZoom;

    public float minZoom = 5f;
    public float maxZoom = 20f;

    void Update()
    {
        HandleMovement();
        HandleRotation();
        HandleZoom();
    }

    void HandleMovement()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 forward = transform.forward;
        Vector3 right = transform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        Vector3 move = (forward * v + right * h);

        transform.position += move * moveSpeed * Time.deltaTime;
    }

    void HandleRotation()
    {
        float rotate = 0f;

        if (Input.GetKey(KeyCode.Q))
            rotate = -1f;

        if (Input.GetKey(KeyCode.E))
            rotate = 1f;

        transform.Rotate(Vector3.up, rotate * rotateSpeed * Time.deltaTime, Space.World);
    }

    void HandleZoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        camZoom.fieldOfView -= scroll * zoomSpeed;

        camZoom.fieldOfView = Mathf.Clamp(camZoom.fieldOfView, minZoom, maxZoom);
    }
}