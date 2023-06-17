using System;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    [SerializeField] private float lifeTime = 3;
    [SerializeField] private Animator anim;

    private float _speed = 7;
    private int _damage = 1;
    private float _destructionTime;
    protected Vector3 direction;
    private bool _hasHit = false;

    public void SetSpeed(float speed) => _speed = speed;
    public void SetDamage(int damage) => _damage = damage;

    protected abstract void Start();

    protected abstract void OnDestroy();

    protected void OnEnable()
    {
        _destructionTime = Time.time + lifeTime;
    }

    protected void Update()
    {
        if (Time.time > _destructionTime)
        {
            DestroyProjectile();
        }

        float movementDistance = _speed * Time.deltaTime;
        Vector3 translation = direction * movementDistance;
        transform.Translate(translation);
    }

    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (_hasHit) return;
        _hasHit = true;
        anim.SetTrigger("Impact");
        if (other.TryGetComponent(out IDamageable targetHit))
        {
            targetHit.TakeHit(_damage);
        }
    }

    protected void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}