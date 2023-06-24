using UnityEngine;

public class SkeletonEnemy : EnemyConfig
{
    [Header("Score")] 
    [SerializeField] private Coin coinPrefab;

    private FiniteStateMachine _fms;

    private new void Start()
    {
        base.Start();

        var evt = GetComponentInChildren<EnemyAnimationEvent>();
        evt.OnAttackAction += Attack;
        evt.OnDestroyAction += DestroySkeleton;

        _fms = GetComponent<FiniteStateMachine>();
        deathSoundName = "SkeletonDeath";
    }

    private void OnDestroy()
    {
        var evt = GetComponentInChildren<EnemyAnimationEvent>();
        evt.OnAttackAction -= Attack;
        evt.OnDestroyAction -= DestroySkeleton;
    }

    private void Attack()
    {
        AudioManager.Instance.PlaySound2D("SkeletonAttack");
        var distance = (_fms.Target.position - transform.position).magnitude;
        var xDiff = _fms.Target.position.x - transform.position.x;

        var inRange = distance <= attackRange && Mathf.Sign(xDiff) == Mathf.Sign(transform.localScale.x);

        if (!inRange) return;

        if (_fms.Target.TryGetComponent(out IDamageable targetHit)) targetHit.TakeHit(attackDamage);
    }

    private void DestroySkeleton()
    {
        var newCoin = Instantiate(coinPrefab, transform.position + new Vector3(0, 1, 0), transform.rotation);
        newCoin.DropCoin();
        Destroy(gameObject);
    }
}