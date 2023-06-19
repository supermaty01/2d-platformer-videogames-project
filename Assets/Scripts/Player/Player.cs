using UnityEngine;

public class Player : LivingEntity
{
    [SerializeField]
    private LayerMask _targetLayerMask;
    
    [SerializeField]
    private float attackRange;
    
    private PlayerMovement playerMovement;

    void Start()
    {
        GameManager.Instance.target = transform;
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
        GameEvents.OnPlayerHealthChangeEvent?.Invoke(HealthPoints);
        playerMovement.SetPlayerState(PlayerMovement.PlayerState.Hurt);
        AudioManager.Instance.PlaySound2D("PlayerTakeDamage");
    }
    
    private void Attack()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right , attackRange, _targetLayerMask);
        if (hit.collider != null)
        {
            if(hit.collider.TryGetComponent(out IDamageable targetHit))
            {
                targetHit.TakeHit(1);
            }
        }
        AudioManager.Instance.PlaySound2D("PlayerAttack");
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
