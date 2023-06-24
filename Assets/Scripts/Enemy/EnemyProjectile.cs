using UnityEngine;

public class EnemyProjectile : Projectile
{
    protected override void Start()
    {
        direction = Vector3.left;
        var evt = GetComponentInChildren<EnemyAnimationEvent>();
        evt.OnDestroyAction += DestroyProjectile;
    }

    protected override void OnDestroy()
    {
        var evt = GetComponentInChildren<EnemyAnimationEvent>();
        evt.OnAttackAction -= DestroyProjectile;
    }
}