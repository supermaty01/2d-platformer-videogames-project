using System;
using UnityEngine;

public class EnemyProjectile : Projectile
{
    protected override void Start()
    {
        direction = Vector3.left;
        EnemyAnimationEvent evt = GetComponentInChildren<EnemyAnimationEvent>();
        evt.OnDestroyAction += DestroyProjectile;
    }

    protected override void OnDestroy()
    {
        EnemyAnimationEvent evt = GetComponentInChildren<EnemyAnimationEvent>();
        evt.OnAttackAction -= DestroyProjectile;
    }
}