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
        InitHealth();
        EnemyAnimationEvent evt = GetComponentInChildren<EnemyAnimationEvent>();
        evt.OnAttackAction += SpawnProjectile;
        evt.OnDestroyAction += DestroyWorm;
        deathSoundName = "WormDeath";
    }

    private void OnDestroy()
    {
        EnemyAnimationEvent evt = GetComponentInChildren<EnemyAnimationEvent>();
        evt.OnAttackAction -= SpawnProjectile;
        evt.OnDestroyAction -= DestroyWorm;
    }

    private void SpawnProjectile()
    {
        Projectile newProjectile = Instantiate(projectilePrefab, shootPoint.position, shootPoint.rotation);
        newProjectile.SetDamage(attackDamage);
        AudioManager.Instance.PlaySound2D("WormAttack");
    }
    
    private void DestroyWorm()
    {
        Coin newCoin = Instantiate(coinPrefab, transform.position + new Vector3(0,1,0), transform.rotation);
        newCoin.DropCoin();
        Destroy(gameObject);
    }
}
