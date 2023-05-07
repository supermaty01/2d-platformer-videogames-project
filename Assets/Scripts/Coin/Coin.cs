using System;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public int scoreValue = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        ScoreManager.instance.AddScore(scoreValue);
        Destroy(gameObject);
    }
}
