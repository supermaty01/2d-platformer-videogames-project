using UnityEngine;

public class SkeletonEnemy : EnemyConfig
{
    [Header("Score")]
    [SerializeField] private Coin coinPrefab;
    
    private new void Start()
    {
        base.Start();

        var evt = GetComponentInChildren<EnemyAnimationEvent>();
        evt.OnAttackAction += Attack;
        evt.OnDestroyAction += DestroySkeleton;
    }

    private void OnDestroy()
    {
        var evt = GetComponentInChildren<EnemyAnimationEvent>();
        evt.OnAttackAction -= Attack;
        evt.OnDestroyAction -= DestroySkeleton;
    }

    public void Attack()
    {
        Debug.Log("Skeleton Attack!");
    }

    private void DestroySkeleton()
    {
        Coin newCoin = Instantiate(coinPrefab, transform.position + new Vector3(0,1,0), transform.rotation);
        Destroy(gameObject);
    }
}