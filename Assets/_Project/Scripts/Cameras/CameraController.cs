using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform Target;
    public float Distance = 15f;
    public float Height = 10f;
    public float Damping = 5f;
    public float MouseRotationSpeed = 1f;

    public float Yaw = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (Target == null) return;

        Yaw += Input.GetAxis("Mouse X") * MouseRotationSpeed * Time.deltaTime;

        var targetPosition =
            Target.position +
            ((Quaternion.Euler(0f, Yaw, 0f) * Vector3.forward) * Distance) +
            (Vector3.up * Height);

        transform.position = Vector3.Lerp(
            transform.position,
            targetPosition,
            Damping * Time.deltaTime
        );

        transform.LookAt(Target);
    }
}