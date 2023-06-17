using UnityEngine;

public class WizardEnemy : EnemyConfig
{
    [Header("Projectile")]
    [SerializeField] private EnemyProjectile projectilePrefab;
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
        EnemyProjectile newProjectile = Instantiate(projectilePrefab, shootPoint.position, shootPoint.rotation);
        newProjectile.SetDamage(attackDamage);
    }
    
    private void DestroyWizard()
    {
        Coin newCoin = Instantiate(coinPrefab, transform.position + new Vector3(0,1,0), transform.rotation);
        newCoin.DropCoin();
        Destroy(gameObject);
    }
}
