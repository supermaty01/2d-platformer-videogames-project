using System;
using UnityEngine;

public class PlayerProjectile : Projectile
{
    
    protected override void Start()
    {
        PlayerAnimationEvent evt = GetComponentInChildren<PlayerAnimationEvent>();
        evt.OnDestroyAction += DestroyProjectile;
    }

    protected override void OnDestroy()
    {
        PlayerAnimationEvent evt = GetComponentInChildren<PlayerAnimationEvent>();
        evt.OnAttackAction -= DestroyProjectile;
    }
    
}