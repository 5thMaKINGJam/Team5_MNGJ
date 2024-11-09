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

    // Orthographic Size를 사용하여 화면 높이와 너비 계산
    float frustumHeight = cam.orthographicSize * 2;
    float frustumWidth = frustumHeight * cam.aspect;

    float clampedX = Mathf.Clamp(transform.position.x, minBound.x + frustumWidth / 2, maxBound.x - frustumWidth / 2);
    float clampedY = Mathf.Clamp(transform.position.y, minBound.y + frustumHeight / 2, maxBound.y - frustumHeight / 2);

    // 카메라의 Z 위치를 -10으로 고정
    transform.position = new Vector3(clampedX, clampedY, -10f);
}

}
