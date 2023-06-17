using UnityEngine;

public class Player : LivingEntity
{
    [Header("Projectile")] [SerializeField]
    private Projectile projectilePrefab;

    [SerializeField] private Transform shootPoint;


    [SerializeField] private LayerMask _targetLayerMask;

    [SerializeField] private float attackRange;

    [SerializeField] private int windAttackDamage = 2;

    private PlayerMovement playerMovement;

    private bool _hasWindSword = false;

    public enum PowerUp
    {
        WindSword,
    }

    public void ActivatePowerUp(PowerUp powerUp)
    {
        if (powerUp == PowerUp.WindSword)
        {
            _hasWindSword = true;
        }
    }

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        InitHealth();

        var evt = GetComponentInChildren<PlayerAnimationEvent>();
        evt.OnAttackAction += Attack;
        evt.OnDestroyAction += Destroy;
    }

    private void OnDestroy()
    {
        var evt = GetComponentInChildren<PlayerAnimationEvent>();
        evt.OnAttackAction -= Attack;
        evt.OnDestroyAction -= Destroy;
    }

    protected override bool IsProtected()
    {
        // TODO Verificar que solo bloquea por el lado que recibe el golpe
        return playerMovement.GetPlayerState() == PlayerMovement.PlayerState.Defend;
    }

    protected override void OnTakeDamage()
    {
        base.OnTakeDamage();
        playerMovement.SetPlayerState(PlayerMovement.PlayerState.Hurt);
    }

    private void Attack()
    {
        if (_hasWindSword)
        {
            SpawnProjectile();
        }
        else
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, attackRange, _targetLayerMask);
            if (hit.collider != null)
            {
                if (hit.collider.TryGetComponent(out IDamageable targetHit))
                {
                    targetHit.TakeHit(1);
                }
            }
        }
    }

    public void SpawnProjectile()
    {
        Projectile newProjectile = Instantiate(projectilePrefab, shootPoint.position, shootPoint.rotation);
        newProjectile.SetDamage(windAttackDamage);
    }

    protected override void OnDeath()
    {
        base.OnDeath();
        playerMovement.SetPlayerState(PlayerMovement.PlayerState.Dead);
    }

    private void Destroy()
    {
        UIManager.instance.ShowGameOverScreen();
    }
}