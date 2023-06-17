using UnityEngine;

public class ThwompEnemy : EnemyConfig
{
    new void Start()
    {
        base.Start();
        transform.position = initialPos;
        EnemyAnimationEvent evt = GetComponentInChildren<EnemyAnimationEvent>();
        evt.OnAttackAction += AttackSound;
    }
    
    private void OnDestroy()
    {
        EnemyAnimationEvent evt = GetComponentInChildren<EnemyAnimationEvent>();
        evt.OnAttackAction -= AttackSound;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.TryGetComponent(out IDamageable targetHit))
        {
            targetHit.TakeHit(attackDamage);
        }
    }
    
    public void AttackSound()
    {
        AudioManager.Instance.PlaySound2D("ThwompAttack");
    }
}
