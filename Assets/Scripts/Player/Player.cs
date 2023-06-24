using UnityEngine;

public class Player : LivingEntity
{
    public enum PowerUp
    {
        WindSword,
        Boots
    }

    [Header("Projectile")] 
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private LayerMask _targetLayerMask;
    [SerializeField] private float attackRange;
    [SerializeField] private int windAttackDamage = 2;

    private bool _hasWindSword;
    private PlayerMovement playerMovement;

    private void Awake()
    {
        GameManager.Instance.target = transform;
    }

    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        InitHealth();

        var evt = GetComponentInChildren<PlayerAnimationEvent>();
        evt.OnAttackAction += Attack;
        evt.OnDestroyAction += Destroy;
        evt.OnFirstStepAction += () => AudioManager.Instance.PlaySound2D("FirstStep");
        evt.OnSecondStepAction += () => AudioManager.Instance.PlaySound2D("SecondStep");
    }

    private void OnDestroy()
    {
        var evt = GetComponentInChildren<PlayerAnimationEvent>();
        evt.OnAttackAction -= Attack;
        evt.OnDestroyAction -= Destroy;
    }

    public void ActivatePowerUp(PowerUp powerUp)
    {
        if (powerUp == PowerUp.WindSword) _hasWindSword = true;
        if (powerUp == PowerUp.Boots) playerMovement.SetBoots(true);
    }

    public void ClearPowerUps()
    {
        _hasWindSword = false;
        playerMovement.SetBoots(false);
    }

    protected override bool IsProtected()
    {
        return playerMovement.GetPlayerState() == PlayerMovement.PlayerState.Defend;
    }

    protected override void OnTakeDamage()
    {
        base.OnTakeDamage();
        GameEvents.OnPlayerHealthChangeEvent?.Invoke(HealthPoints);
        playerMovement.SetPlayerState(PlayerMovement.PlayerState.Hurt);
        ClearPowerUps();
        AudioManager.Instance.PlaySound2D("PlayerTakeDamage");
    }

    private void Attack()
    {
        if (_hasWindSword)
        {
            SpawnProjectile();
        }
        else
        {
            var hit = Physics2D.Raycast(transform.position, transform.right, attackRange, _targetLayerMask);
            if (hit.collider != null)
                if (hit.collider.TryGetComponent(out IDamageable targetHit))
                    targetHit.TakeHit();
        }

        AudioManager.Instance.PlaySound2D("PlayerAttack");
    }

    public void SpawnProjectile()
    {
        var newProjectile = Instantiate(projectilePrefab, shootPoint.position, shootPoint.rotation);
        newProjectile.SetDamage(windAttackDamage);
    }

    protected override void OnDeath()
    {
        base.OnDeath();
        playerMovement.SetPlayerState(PlayerMovement.PlayerState.Dead);
        AudioManager.Instance.PlaySound2D("PlayerDeath");
    }

    private void Destroy()
    {
        GameEvents.OnGameOverEvent?.Invoke();
    }
}