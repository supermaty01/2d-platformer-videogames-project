using UnityEngine;

public class WormEnemy : EnemyConfig
{
    [Header("Projectile")]
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private Transform shootPoint;

    [Header("Score")]
    [SerializeField] private Coin coinPrefab;
    
    new void Start()
    {
        EnemyAnimationEvent evt = GetComponentInChildren<EnemyAnimationEvent>();
        evt.OnAttackAction += SpawnProjectile;
        evt.OnDestroyAction += DestroyWorm;
        Debug.Log('c');
    }

    private void OnDestroy()
    {
        EnemyAnimationEvent evt = GetComponentInChildren<EnemyAnimationEvent>();
        evt.OnAttackAction -= SpawnProjectile;
        evt.OnDestroyAction -= DestroyWorm;
        Debug.Log('b');
    }

    private void SpawnProjectile()
    {
        Projectile newProjectile = Instantiate(projectilePrefab, shootPoint.position, shootPoint.rotation);
        newProjectile.SetDamage(attackDamage);
    }
    
    private void DestroyWorm()
    {
        Debug.Log('a');
        Coin newCoin = Instantiate(coinPrefab, transform.position + new Vector3(0,1,0), transform.rotation);
        Destroy(gameObject);
    }
}
