using UnityEngine;

public abstract class PowerUp : MonoBehaviour
{
    [SerializeField]
    public float rotationSpeed = 2f;
    
    private Rigidbody2D _rb;

    private void Update()
    {
        transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
    }
    
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if(other.TryGetComponent(out PlayerMovement playerMovement))
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