using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Player : LivingEntity
{
    PlayerMovement playerMovement;
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
        // gameObject.SetActive(false);
    }
}
