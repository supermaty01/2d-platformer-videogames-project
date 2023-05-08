using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Player : LivingEntity
{
    [SerializeField]
    private LayerMask _targetLayerMask;
    
    [SerializeField]
    private float attackRange;
    
    private PlayerMovement playerMovement;

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        InitHealth();
    }

    protected override void OnDeath()
    {
        base.OnDeath();
        playerMovement.SetPlayerState(PlayerMovement.PlayerState.Dead);
        // gameObject.SetActive(false);
    }
    
    protected override void OnTakeDamage()
    {
        base.OnTakeDamage();
        playerMovement.SetPlayerState(PlayerMovement.PlayerState.Hurt);
    }
    
    protected override bool IsProtected()
    {
        // TODO Verificar que solo bloquea por el lado que recibe el golpe
        return playerMovement.GetPlayerState() == PlayerMovement.PlayerState.Defend;
    }
    
    public void Attack()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right , attackRange, _targetLayerMask);
        if (hit.collider != null)
        {
            if(hit.collider.TryGetComponent(out IDamageable targetHit))
            {
                targetHit.TakeHit(1);
            }
        }
    }
}
