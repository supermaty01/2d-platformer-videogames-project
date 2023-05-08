using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardEnemy : EnemyConfig
{
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private Transform shootPoint;
    
    new void Start()
    {
        InitHealth();
        EnemyAnimationEvent evt = GetComponentInChildren<EnemyAnimationEvent>();
        evt.OnAttackAction += SpawnBullet;
    }

    private void OnDestroy()
    {
        EnemyAnimationEvent evt = GetComponentInChildren<EnemyAnimationEvent>();
        evt.OnAttackAction -= SpawnBullet;
    }

    public void SpawnBullet()
    {
        Projectile newProjectile = Instantiate(projectilePrefab, shootPoint.position, shootPoint.rotation);
        newProjectile.SetDamage(attackDamage);
    }
}
