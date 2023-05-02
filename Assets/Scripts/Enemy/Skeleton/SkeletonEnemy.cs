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
        evt.OnDestroyAction += DestroySkeleton;

    }

    private void OnDestroy()
    {
        EnemyAnimationEvent evt = GetComponentInChildren<EnemyAnimationEvent>();
        evt.OnAttackAction -= Attack;
        evt.OnDestroyAction -= DestroySkeleton;
    }

    public void Attack()
    {
        Debug.Log("Skeleton Attack!");
    }

    private void DestroySkeleton()
    {
        Destroy(gameObject);
    }
}
