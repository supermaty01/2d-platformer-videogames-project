using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 3f; // velocidad horizontal del personaje
    public float jumpForce = 5f; // fuerza vertical del salto
    public float maxChargeTime = 1.5f; // tiempo máximo de carga del salto
    public LayerMask ground; // capas que se consideran suelo
    public Animator animator; // componente Animator del personaje
    private bool facingRight = true; // indica si el personaje mira hacia la derecha
    private Rigidbody2D rb; // componente Rigidbody2D del personaje
    public float maxDistance;
    public Vector3 boxSize;
    
    private float chargeTime;
    private bool isCharging;
    private bool hasJumped;

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

        if (!isCharging)
        {
            rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
        }
        
        animator.SetFloat("SpeedY", rb.velocity.y);
        animator.SetFloat("SpeedX", Mathf.Abs(rb.velocity.x));

        var currentAnimation = animator.GetCurrentAnimatorStateInfo(0);
        
        // Actualizar las animaciones según el estado del jugador cuando está en el suelo
        if (IsGrounded())
        {
            animator.SetBool("IsGrounded", true);
            
            // Verificar si el jugador está cargando el salto
            if (Input.GetKey(KeyCode.Space))
            {
                isCharging = true;
            }
            else if (!Input.GetKey(KeyCode.Space) && isCharging)
            {
                isCharging = false;
                Jump();
                return;
            }
        
            // Actualizar el tiempo de carga del salto
            if (isCharging)
            {
                chargeTime += Time.deltaTime;
            }
            
            
            if (isCharging)
            {
                animator.Play("Preparation");
            }
            else if (hasJumped)
            {
                animator.Play("Landing");
                hasJumped = false;
            }
            else if (moveInput != 0)
            {
                
                animator.SetFloat("SpeedY", 0);
                animator.SetFloat("SpeedX", Mathf.Abs(rb.velocity.x));
                if (currentAnimation.IsName("Landing") && currentAnimation.normalizedTime < 1.0f)
                {
                    Debug.Log("Animation not finished");
                    return;
                }
                animator.Play("Walking");
            }
            else
            {
                if (currentAnimation.IsName("Landing") && currentAnimation.normalizedTime < 1.0f)
                {
                    Debug.Log("Animation not finished");
                    return;
                }
                animator.Play("Idle");
            }
        }
        else
        {
            animator.SetBool("IsGrounded", false);
        }
    }
    
    private void Jump()
    {
        if (IsGrounded())
        {
            // Calcular la fuerza del salto
            float jumpPower = jumpForce;
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
            hasJumped = true;
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(Vector3.up, 180f);
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, maxDistance, ground);
    }
}
