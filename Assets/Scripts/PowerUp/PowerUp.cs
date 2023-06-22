using System;
using UnityEngine;
using UnityEngine.Serialization;

public abstract class PowerUp : MonoBehaviour
{
    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            AudioManager.Instance.PlaySound2D("PlayerPowerUp");
            if (other.TryGetComponent(out PlayerMovement playerMovement))
            {
                playerMovement.SetPlayerState(PlayerMovement.PlayerState.PowerUp);
                ActivatePowerUp(other.GetComponent<Player>());
            }

            Destroy(gameObject);
        }
        else
        {
            _rb.gravityScale = 0;
            _rb.velocity = Vector2.zero;
        }
    }

    protected abstract void ActivatePowerUp(Player player);
}