using UnityEngine;

public class WormEnemy : EnemyConfig
{
    [Header("Projectile")] 
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private Transform shootPoint;

    [Header("Score")] 
    [SerializeField] private Coin coinPrefab;

    private new void Start()
    {
        InitHealth();
        var evt = GetComponentInChildren<EnemyAnimationEvent>();
        evt.OnAttackAction += SpawnProjectile;
        evt.OnDestroyAction += DestroyWorm;
        deathSoundName = "WormDeath";
    }

    private void OnDestroy()
    {
        var evt = GetComponentInChildren<EnemyAnimationEvent>();
        evt.OnAttackAction -= SpawnProjectile;
        evt.OnDestroyAction -= DestroyWorm;
    }

    private void SpawnProjectile()
    {
        var newProjectile = Instantiate(projectilePrefab, shootPoint.position, shootPoint.rotation);
        newProjectile.SetDamage(attackDamage);
        AudioManager.Instance.PlaySound2D("WormAttack");
    }

    private void DestroyWorm()
    {
        var newCoin = Instantiate(coinPrefab, transform.position + new Vector3(0, 1, 0), transform.rotation);
        newCoin.DropCoin();
        Destroy(gameObject);
    }
}