using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThwompEnemy : EnemyConfig
{
    private void Start()
    {
        base.Start();
        
        EnemyAnimationEvent evt = GetComponentInChildren<EnemyAnimationEvent>();
        evt.OnAttackAction += Attack;

        transform.position = initialPos;
    }

    private void OnDestroy()
    {
        EnemyAnimationEvent evt = GetComponentInChildren<EnemyAnimationEvent>();
        evt.OnAttackAction -= Attack;
    }

    public void Attack()
    {
        Debug.Log("Attack!");
    }
    
    protected void OnDrawGizmos()
    {
        Debug.Log(transform.position.x);
        Gizmos.color = new Color(1, 0, 0, 0.3f);
        Gizmos.DrawCube(new Vector3(transform.position.x, 0, 0), new Vector3(attackRange*2, 10, 0));
    }
}
