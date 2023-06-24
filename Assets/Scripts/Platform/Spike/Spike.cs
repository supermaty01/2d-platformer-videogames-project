using UnityEngine;

public class Spike : MonoBehaviour
{
    [SerializeField] private float timeToDamage = 5f;

    [SerializeField] private int damage;

    [SerializeField] private float lastTimeDamageDealt = -Mathf.Infinity;

    public float slowSpeed = 2f;

    public GameObject player;

    private float _normalSpeed;

    private PlayerMovement _playerMovement;
    private Rigidbody2D _playerRb;

    private void Start()
    {
        _playerMovement = player.GetComponent<PlayerMovement>();
        _playerRb = player.GetComponent<Rigidbody2D>();
        _normalSpeed = _playerMovement.moveSpeed;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player")) _playerMovement.moveSpeed = slowSpeed;
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("Player"))
            return;

        _playerMovement.moveSpeed = _normalSpeed;
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("Player"))
            return;

        if (Time.time - lastTimeDamageDealt >= timeToDamage)
            if (other.gameObject.TryGetComponent(out IDamageable targetHit))
            {
                targetHit.TakeHit(damage);
                lastTimeDamageDealt = Time.time;
            }
    }
}