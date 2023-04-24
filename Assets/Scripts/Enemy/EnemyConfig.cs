using System;
using UnityEngine;

public class EnemyConfig : MonoBehaviour
{
    public int health = 10;

    [Header("Attack")]
    public float attackRange = 5f;
    public float attackDuration = 1.5f;
    public int attackDamage = 1;
    
    [Header("Move")] 
    public Transform initialPointTransform;
    public Transform finalPointTransform;
    public float speed = 1f;

    [Header("Finite-State Machine")]
    public StateType initialState;
    public FSMData fsmData;

    public Vector3 initialPos, finalPos;

    protected void Start()
    {
        initialPos = initialPointTransform.position;
        finalPos = finalPointTransform.position;
    }

    protected void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, 0.3f);
        Gizmos.DrawSphere(transform.position, attackRange);
    }
}