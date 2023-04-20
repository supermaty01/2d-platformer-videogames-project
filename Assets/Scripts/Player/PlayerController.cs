using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f; // velocidad horizontal del personaje
    public float jumpForce = 10f; // fuerza vertical del salto
    public float fallMultiplier = 2.5f; // multiplicador de caída para hacerla más rápida
    public float lowJumpMultiplier = 2f; // multiplicador de salto bajo para hacerlo más suave
    public Transform groundCheck; // objeto que indica si el personaje está en el suelo
    public float groundCheckRadius = 0.2f; // radio de detección del suelo
    public LayerMask ground; // capas que se consideran suelo
    public Animator animator; // componente Animator del personaje
    private bool facingRight = true; // indica si el personaje mira hacia la derecha
    private Rigidbody2D rb; // componente Rigidbody2D del personaje

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        if (moveInput > 0 && !facingRight)
        {
            Flip();
        }
        else if (moveInput < 0 && facingRight)
        {
            Flip();
        }

        animator.SetFloat("Speed", Mathf.Abs(moveInput));

        bool isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, ground);
        animator.SetBool("IsGrounded", isGrounded);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            animator.SetTrigger("Jump");
        }

        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
            animator.SetBool("IsFalling", true);
        }
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
        else
        {
            animator.SetBool("IsFalling", false);
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(Vector3.up, 180f);
    }
}
