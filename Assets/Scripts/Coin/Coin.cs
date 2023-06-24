using UnityEngine;

public class Coin : MonoBehaviour
{
    public int scoreValue = 1;
    private bool _canCollected = true;

    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!_canCollected) return;

            AudioManager.Instance.PlaySound2D("EarnCoin");
            GameEvents.OnPlayerScoreChangeEvent?.Invoke(scoreValue);
            Destroy(gameObject);
        }
        else
        {
            _canCollected = true;
            _rb.gravityScale = 0;
            _rb.velocity = Vector2.zero;
        }
    }

    public void DropCoin()
    {
        _canCollected = false;
        _rb.velocity = new Vector2(Random.Range(-1f, 1f), 1f) * 3f;
    }
}