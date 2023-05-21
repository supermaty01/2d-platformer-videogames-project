using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    [SerializeField]
    private float _timeToDamage = 5f; 
    
    [SerializeField]
    private int _damage = 0;
    
    [SerializeField]
    private float lastTimeDamageDealt = -Mathf.Infinity;
    
    public float slowSpeed = 2f;
    
    public GameObject player;
    
    private PlayerMovement _playerMovement;
    private Rigidbody2D _playerRb;
    
    private float _normalSpeed;
    
    private void Start()
    {
        _playerMovement = player.GetComponent<PlayerMovement>();
        _playerRb = player.GetComponent<Rigidbody2D>();
        _normalSpeed = _playerMovement.moveSpeed;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("Player touched the spike");
        if (other.gameObject.CompareTag("Player"))
        {
            _playerMovement.moveSpeed = slowSpeed;
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("Player"))
            return;
        
        if (Time.time - lastTimeDamageDealt >= _timeToDamage)
        {
            if(other.gameObject.TryGetComponent(out IDamageable targetHit))
            {
                targetHit.TakeHit(_damage);
                lastTimeDamageDealt = Time.time;
            }
        }
    }
    
    private void OnCollisionExit2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("Player"))
            return;
        
        _playerMovement.moveSpeed = _normalSpeed;
    }
}