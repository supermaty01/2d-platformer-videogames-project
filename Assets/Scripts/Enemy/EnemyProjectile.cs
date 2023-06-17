using System;
using UnityEngine;

public class EnemyProjectile : Projectile
{
    
    protected override void Start()
    {
        EnemyAnimationEvent evt = GetComponentInChildren<EnemyAnimationEvent>();
        evt.OnDestroyAction += DestroyProjectile;
    }

    protected override void OnDestroy()
    {
        EnemyAnimationEvent evt = GetComponentInChildren<EnemyAnimationEvent>();
        evt.OnAttackAction -= DestroyProjectile;
    }
    
}