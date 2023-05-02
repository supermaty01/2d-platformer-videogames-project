using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormEnemy : EnemyConfig
{
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private Transform shootPoint;
    
    new void Start()
    {
        EnemyAnimationEvent evt = GetComponentInChildren<EnemyAnimationEvent>();
        evt.OnAttackAction += SpawnProjectile;
    }

    private void OnDestroy()
    {
        EnemyAnimationEvent evt = GetComponentInChildren<EnemyAnimationEvent>();
        evt.OnAttackAction -= SpawnProjectile;
    }

    public void SpawnProjectile()
    {
        Projectile newProjectile = Instantiate(projectilePrefab, shootPoint.position, shootPoint.rotation);
        newProjectile.SetDamage(attackDamage);
    }
}
