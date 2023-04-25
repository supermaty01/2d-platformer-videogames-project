using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonEnemy : EnemyConfig
{
    new void Start()
    {
        base.Start();
        
        EnemyAnimationEvent evt = GetComponentInChildren<EnemyAnimationEvent>();
        evt.OnAttackAction += Attack;

    }

    private void OnDestroy()
    {
        EnemyAnimationEvent evt = GetComponentInChildren<EnemyAnimationEvent>();
        evt.OnAttackAction -= Attack;
    }

    public void Attack()
    {
        Debug.Log("Skeleton Attack!");
    }
}
