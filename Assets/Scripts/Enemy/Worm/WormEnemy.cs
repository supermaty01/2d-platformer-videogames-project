using UnityEngine;

public class WormEnemy : EnemyConfig
{
    [Header("Projectile")]
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private Transform shootPoint;
    
    new void Start()
    {
        EnemyAnimationEvent evt = GetComponentInChildren<EnemyAnimationEvent>();
        evt.OnAttackAction += SpawnProjectile;
        evt.OnDestroyAction += DestroyWorm;
    }

    private void OnDestroy()
    {
        EnemyAnimationEvent evt = GetComponentInChildren<EnemyAnimationEvent>();
        evt.OnAttackAction -= SpawnProjectile;
        evt.OnDestroyAction += DestroyWorm;
    }

    private void SpawnProjectile()
    {
        Projectile newProjectile = Instantiate(projectilePrefab, shootPoint.position, shootPoint.rotation);
        newProjectile.SetDamage(attackDamage);
    }
    
    private void DestroyWorm()
    {
        Destroy(gameObject);
    }
}
