using UnityEngine;

public class PlayerProjectile : Projectile
{
    protected override void Start()
    {
        direction = Vector3.right;
        var evt = GetComponentInChildren<PlayerAnimationEvent>();
        evt.OnDestroyAction += DestroyProjectile;
    }

    protected override void OnDestroy()
    {
        var evt = GetComponentInChildren<PlayerAnimationEvent>();
        evt.OnAttackAction -= DestroyProjectile;
    }
}