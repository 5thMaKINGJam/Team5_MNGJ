using UnityEngine;

public class CameraController: MonoBehaviour
{
    public Transform player;
    public Vector3 offset;

    [SerializeField]
    private BoxCollider2D bound;

    private Vector3 minBound;
    private Vector3 maxBound;

    private void Start()
    {
        minBound = bound.bounds.min;
        maxBound = bound.bounds.max;
    }

    void LateUpdate()
    {
        transform.position = player.position + offset;
        ClampCameraToBounds();
    }

    void ClampCameraToBounds()
    {
        Camera cam = Camera.main;

        float distanceFromTarget = Mathf.Abs(transform.position.z);
        float frustumHeight = 2.0f * distanceFromTarget * Mathf.Tan(cam.fieldOfView * 0.5f * Mathf.Deg2Rad);
        float frustumWidth = frustumHeight * cam.aspect;

        float clampedX = Mathf.Clamp(transform.position.x, minBound.x + frustumWidth / 2, maxBound.x - frustumWidth / 2);
        float clampedY = Mathf.Clamp(transform.position.y, minBound.y + frustumHeight / 2, maxBound.y - frustumHeight / 2);

        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }
}
