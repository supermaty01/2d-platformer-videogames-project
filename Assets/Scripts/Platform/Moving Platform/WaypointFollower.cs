using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointFollower : MonoBehaviour
{

    [Header("Move")] public Transform initialPointTransform;
    public Transform finalPointTransform;
    
    private Vector3 initialPos, finalPos;
    [SerializeField] private float _speed = 2f;

    void Start()
    {
        initialPos = initialPointTransform.position;
        finalPos = finalPointTransform.position;
    }
    void Update()
    {
        float d = (initialPos - finalPos).magnitude;
        float delta = Mathf.PingPong(Time.time * _speed, d);
        transform.position = Vector3.Lerp(initialPos, finalPos, delta / d);
    }
}
