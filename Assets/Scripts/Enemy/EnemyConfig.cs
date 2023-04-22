using UnityEngine;

public class EnemyConfig : MonoBehaviour
{
    public int health = 1;

    [Header("Attack")]
    public float attackRange = 5f;
    public float attackDelay = 0.18f;
    public float attackDuration = 1.5f;
    public int attackDamage = 1;

    [Header("Finite-State Machine")]
    public StateType initialState;
    public FSMData fsmData;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}