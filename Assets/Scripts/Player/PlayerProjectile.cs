using System;
using UnityEngine;

public class PlayerProjectile : Projectile
{
    protected override void Start()
    {
        direction = Vector3.right;
        PlayerAnimationEvent evt = GetComponentInChildren<PlayerAnimationEvent>();
        evt.OnDestroyAction += DestroyProjectile;
    }

    protected override void OnDestroy()
    {
        PlayerAnimationEvent evt = GetComponentInChildren<PlayerAnimationEvent>();
        evt.OnAttackAction -= DestroyProjectile;
    }
}