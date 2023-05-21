using UnityEngine;

public class ThwompEnemy : EnemyConfig
{
    new void Start()
    {
        base.Start();
        transform.position = initialPos;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.TryGetComponent(out IDamageable targetHit))
        {
            targetHit.TakeHit(attackDamage);
        }
    }
}
