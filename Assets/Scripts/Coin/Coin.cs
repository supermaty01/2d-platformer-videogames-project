using UnityEngine;

public class Coin : MonoBehaviour
{
    public int scoreValue = 1;

    private Rigidbody2D _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ScoreManager.instance.AddScore(scoreValue);
            Destroy(gameObject);
        }
        else
        {
            _rb.gravityScale = 0;
            _rb.velocity = Vector2.zero;
        }
    }
}
