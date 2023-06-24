using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Movement")] public Transform target;

    [Range(1, 10)] public float smoothSpeed = 1.5f;

    [Header("Limits")] public float minX;

    public float maxX;
    public float minY;
    public float maxY;


    private void LateUpdate()
    {
        FollowPlayer();
    }

    private void FollowPlayer()
    {
        var targetPosition = target.position + new Vector3(0, 0, -10);
        Vector3 clampedPosition = new(Mathf.Clamp(targetPosition.x, minX, maxX),
            Mathf.Clamp(targetPosition.y, minY, maxY), targetPosition.z);

        var smoothedPosition = Vector3.Lerp(transform.position, clampedPosition, smoothSpeed * Time.fixedDeltaTime);
        transform.position = smoothedPosition;
    }
}