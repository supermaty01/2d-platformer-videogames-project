using UnityEngine;

public class SkeletonEnemy : EnemyConfig
{
    public int scoreValue = 10;
    
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
        ScoreManager.instance.AddScore(scoreValue);
        Destroy(gameObject);
    }
}