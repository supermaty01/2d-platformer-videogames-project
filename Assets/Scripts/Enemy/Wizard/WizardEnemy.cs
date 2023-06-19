using UnityEngine;

public class WizardEnemy : EnemyConfig
{
    [Header("Projectile")] [SerializeField]
    private Projectile projectilePrefab;

    [SerializeField] private Transform shootPoint;

    [Header("Score")] [SerializeField] private Coin coinPrefab;

    new void Start()
    {
        InitHealth();
        EnemyAnimationEvent evt = GetComponentInChildren<EnemyAnimationEvent>();
        evt.OnAttackAction += SpawnBullet;
        evt.OnDestroyAction += DestroyWizard;
        deathSoundName = "WizardDeath";
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
        AudioManager.Instance.PlaySound2D("WizardAttack");
    }

    private void DestroyWizard()
    {
        Coin newCoin = Instantiate(coinPrefab, transform.position + new Vector3(0, 1, 0), transform.rotation);
        newCoin.DropCoin();
        Destroy(gameObject);
    }
}