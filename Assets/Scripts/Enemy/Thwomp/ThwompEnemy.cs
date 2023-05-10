using UnityEngine;

public class ThwompEnemy : EnemyConfig
{
    private FiniteStateMachine _fms;
    private Collider2D _collider2D;
    
    new void Start()
    {
        base.Start();
        EnemyAnimationEvent evt = GetComponentInChildren<EnemyAnimationEvent>();
        evt.OnAttackAction += Attack;

        transform.position = initialPos;

        _fms = GetComponent<FiniteStateMachine>();
        _collider2D = GetComponent<Collider2D>();
    }

    private void OnDestroy()
    {
        EnemyAnimationEvent evt = GetComponentInChildren<EnemyAnimationEvent>();
        evt.OnAttackAction -= Attack;
    }

    private void Attack()
    {
        var distance = Mathf.Abs(_fms.Target.position.x - transform.position.x);

        if (!(distance <= 1)) return;

        if(_fms.Target.TryGetComponent(out IDamageable targetHit))
        {
            targetHit.TakeHit(attackDamage);
        }
    }
    
    new void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, 0.3f);
        Gizmos.DrawCube(new Vector3(transform.position.x, 0, 0), new Vector3(attackRange*2, 10, 0));
    }
}
