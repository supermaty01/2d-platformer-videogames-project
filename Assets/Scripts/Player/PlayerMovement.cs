using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 3f; // velocidad horizontal del personaje
    public float jumpForce = 7f; // fuerza vertical del salto
    public float maxChargeTime = 0.8f; // tiempo máximo de carga del salto
    public LayerMask ground; // capas que se consideran suelo
    public Animator animator; // componente Animator del personaje
    private bool facingRight = true; // indica si el personaje mira hacia la derecha
    private Rigidbody2D rb; // componente Rigidbody2D del personaje
    public float maxDistance;
    public Vector3 boxSize;
    
    private float chargeTime;
    private bool isAired;
    private bool isGrounded;
    
    public enum PlayerState
    {
        Idle,
        Walk,
        Charge,
        Jump,
        JumpPeak,
        Fall,
        Land,
        Attack,
        Defend,
    }
    
    public PlayerState playerState;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");

        if ((moveInput > 0 && !facingRight) || (moveInput < 0 && facingRight))
        {
            Flip();
        }

        if (playerState != PlayerState.Charge && playerState != PlayerState.Attack && playerState != PlayerState.Defend)
        {
            rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }

    private void Update()
    {
        // Actualizar las animaciones según el estado del jugador cuando está en el suelo
        if (IsGrounded())
        {
            var jumping = CheckJump();
            
            if (!jumping)
            {
                var currentAnimation = animator.GetCurrentAnimatorStateInfo(0);
                
                if (isAired || (currentAnimation.IsName("Landing") && currentAnimation.normalizedTime < 1f))
                {
                    playerState = PlayerState.Land;
                    isAired = false;
                }
                else if (Input.GetMouseButtonDown(0) || (currentAnimation.IsName("Attack") && currentAnimation.normalizedTime < 1f))
                {
                    playerState = PlayerState.Attack;
                }
                else if (Input.GetMouseButton(1))
                {
                    playerState = PlayerState.Defend;
                }
                else if (rb.velocity.x != 0)
                {
                    playerState = PlayerState.Walk;
                }
                else
                {
                    playerState = PlayerState.Idle;
                }
            }
        }
        else
        {
            isAired = true;
            if (Mathf.Abs(rb.velocity.y) < 5)
            {
                playerState = PlayerState.JumpPeak;
            }  
            else if (rb.velocity.y > 0)
            {
                playerState = PlayerState.Jump;
            }
            else
            {
                playerState = PlayerState.Fall;
            }
        }
        PlayAnimation();
    }


    private void PlayAnimation()
    {
        if (playerState == PlayerState.Walk)
        {
            animator.Play("Walking");
        }
        else if (playerState == PlayerState.Idle)
        {
            animator.Play("Idle");
        }
        else if (playerState == PlayerState.Jump)
        {
            animator.Play("FlyingUp");
        }
        else if (playerState == PlayerState.JumpPeak)
        {
            animator.Play("JumpingReload");
        }
        else if (playerState == PlayerState.Fall)
        {
            animator.Play("Falling");
        }
        else if (playerState == PlayerState.Land)
        {
            animator.Play("Landing");
        }
        else if (playerState == PlayerState.Charge)
        {
            animator.Play("Preparation");
        }
        else if (playerState == PlayerState.Attack)
        {
            animator.Play("Attack");
        }
        else if (playerState == PlayerState.Defend)
        {
            animator.Play("Defend");
        }
    }

    private bool CheckJump()
    {
        // Verificar si el jugador está cargando el salto
        if (Input.GetKey(KeyCode.Space))
        {
            playerState = PlayerState.Charge;
            chargeTime += Time.deltaTime;
            return true;
        }
        if (playerState == PlayerState.Charge)
        {
            Jump();
            return true;
        }
        return false;
    }
    
    private void Jump()
    {
        playerState = PlayerState.Jump;
        // Calcular la fuerza del salto
        var jumpPower = jumpForce;
        if (chargeTime > 0)
        {
            if (chargeTime > maxChargeTime)
            {
                chargeTime = maxChargeTime;
            }
            jumpPower += (jumpForce * chargeTime);
        }

        // Aplicar la fuerza del salto al jugador
        rb.velocity = new Vector2(rb.velocity.x, jumpPower);
        chargeTime = 0f;
    }

    private void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(Vector3.up, 180f);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(transform.position - transform.up * maxDistance, boxSize);
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, maxDistance, ground);
    }
}
