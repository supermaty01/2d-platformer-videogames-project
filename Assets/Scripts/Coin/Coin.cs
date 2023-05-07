using UnityEngine;

public class Coin : MonoBehaviour
{
    public int scoreValue = 1;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ScoreManager.instance.AddScore(scoreValue);
            Destroy(gameObject);
        }
    }
}
