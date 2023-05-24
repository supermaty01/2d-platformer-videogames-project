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
        if (transform.position.x >= finalPos.x ||
            transform.position.x <= initialPos.x)
        {
            _speed *= -1;
        }
        transform.Translate(_speed * Time.deltaTime * Vector3.right);
    }
}
