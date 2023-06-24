using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    [SerializeField] private float lifeTime = 3;
    [SerializeField] private Animator anim;
    private int _damage = 1;
    private float _destructionTime;
    private bool _hasHit;

    private readonly float _speed = 7;
    protected Vector3 direction;

    protected abstract void Start();

    protected void Update()
    {
        if (Time.time > _destructionTime) DestroyProjectile();

        var movementDistance = _speed * Time.deltaTime;
        var translation = direction * movementDistance;
        transform.Translate(translation);
    }

    protected void OnEnable()
    {
        _destructionTime = Time.time + lifeTime;
    }

    protected abstract void OnDestroy();

    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (_hasHit) return;
        _hasHit = true;
        anim.SetTrigger("Impact");
        if (other.TryGetComponent(out IDamageable targetHit)) targetHit.TakeHit(_damage);
    }

    public void SetDamage(int damage)
    {
        _damage = damage;
    }

    protected void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}