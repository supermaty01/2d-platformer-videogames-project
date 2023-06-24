using UnityEngine;

public class WaypointFollower : MonoBehaviour
{
    [Header("Move")] 
    public Transform initialPointTransform;
    public Transform finalPointTransform;
    [SerializeField] private float _speed = 2f;

    private Vector3 initialPos, finalPos;

    private void Start()
    {
        initialPos = initialPointTransform.position;
        finalPos = finalPointTransform.position;
    }

    private void Update()
    {
        var d = (initialPos - finalPos).magnitude;
        var delta = Mathf.PingPong(Time.time * _speed, d);
        transform.position = Vector3.Lerp(initialPos, finalPos, delta / d);
    }
}