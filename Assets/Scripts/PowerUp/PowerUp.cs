using System;
using UnityEngine;
using UnityEngine.Serialization;

public abstract class PowerUp : MonoBehaviour
{
    [SerializeField] public float floatingSpeed = 2f;

    [FormerlySerializedAs("_initPos")] [Space(20)] [SerializeField]
    private Transform _initPosTransform;

    [FormerlySerializedAs("_endPos")] [SerializeField]
    private Transform _endPosTransform;
    
    private Vector3 _initPos, _endPos;

    private Rigidbody2D _rb;

    private void Start()
    {
        _initPos = _initPosTransform.position;
        _endPos = _endPosTransform.position;
    }

    private void Update()
    {
        float d = (_initPos - _endPos).magnitude;
        float delta = Mathf.PingPong(Time.time * floatingSpeed, d);
        transform.position = Vector3.Lerp(_initPos, _endPos, delta / d);
    }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
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