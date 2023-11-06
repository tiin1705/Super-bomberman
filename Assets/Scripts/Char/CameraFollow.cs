using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Tham chi?u ??n transform c?a "Camera Target"
    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    void LateUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        transform.LookAt(target); // ?? camera luôn nhìn vào "Camera Target"
    }
}
