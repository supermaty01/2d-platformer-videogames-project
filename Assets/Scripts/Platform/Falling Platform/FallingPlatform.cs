using System.Collections;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    [SerializeField] private float fallDelay = 1f;
    [SerializeField] private float fallSpeed = 1f;
    [SerializeField] private float destroyDelay = 2f;

    private Collider2D _collider;
    private Rigidbody2D _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")) StartCoroutine(Fall());
    }

    private IEnumerator Fall()
    {
        yield return new WaitForSeconds(fallDelay);
        _collider.enabled = false;
        _rb.bodyType = RigidbodyType2D.Dynamic;
        _rb.gravityScale = fallSpeed;

        Destroy(gameObject, destroyDelay);
    }
}