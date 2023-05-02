using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float lifeTime = 3;
    [SerializeField] private Animator anim;
    
    private float _speed = 7;
    private int _damage = 1;
    private float _destructionTime;
    
    public void SetSpeed(float speed) => _speed = speed;
    public void SetDamage(int damage) => _damage = damage;
    
    private void Start()
    {
        EnemyAnimationEvent evt = GetComponentInChildren<EnemyAnimationEvent>();
        evt.OnDestroyAction += DestroyProjectile;
    }

    private void OnDestroy()
    {
        EnemyAnimationEvent evt = GetComponentInChildren<EnemyAnimationEvent>();
        evt.OnAttackAction -= DestroyProjectile;
    }

    private void OnEnable()
    {
        _destructionTime = Time.time + lifeTime;
    }
    
    private void Update()
    {
        if (Time.time > _destructionTime )
        {
            DestroyProjectile();
        }
        
        float movementDistance = _speed * Time.deltaTime;
        Vector3 translation = Vector3.left * movementDistance;
        transform.Translate(translation);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.TryGetComponent(out IDamageable targetHit))
        {
            targetHit.TakeHit(_damage);
        }
        
        anim.SetTrigger("Impact");
    }

    private void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}