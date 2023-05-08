using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardEnemy : EnemyConfig
{
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private Transform shootPoint;
    
    [Header("Score")]
    [SerializeField] private Coin coinPrefab;
    
    new void Start()
    {
        InitHealth();
        EnemyAnimationEvent evt = GetComponentInChildren<EnemyAnimationEvent>();
        evt.OnAttackAction += SpawnBullet;
        evt.OnDestroyAction += DestroyWizard;
    }

    private void OnDestroy()
    {
        EnemyAnimationEvent evt = GetComponentInChildren<EnemyAnimationEvent>();
        evt.OnAttackAction -= SpawnBullet;
        evt.OnDestroyAction -= DestroyWizard;
    }

    public void SpawnBullet()
    {
        Projectile newProjectile = Instantiate(projectilePrefab, shootPoint.position, shootPoint.rotation);
        newProjectile.SetDamage(attackDamage);
    }
    
    private void DestroyWizard()
    {
        Coin newCoin = Instantiate(coinPrefab, transform.position + new Vector3(0,1,0), transform.rotation);
        Destroy(gameObject);
    }
}
