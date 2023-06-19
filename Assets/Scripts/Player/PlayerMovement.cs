using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 3f; // velocidad horizontal del personaje
    public float jumpForce = 6f; // fuerza vertical del salto
    public float maxChargeTime = 0.8f; // tiempo máximo de carga del salto
    public LayerMask ground; // capas que se consideran suelo
    public Animator animator; // componente Animator del personaje
    public float maxDistance; // distancia máxima a la que se detecta el suelo
    public Vector3 boxSize; // tamaño del boxcast que detecta el suelo
    public float bootsSpeedBooster = 2f;
    public float bootsJumpBooster = 1f;

    public Rigidbody2D rb; // componente Rigidbody2D del personaje
    private bool _facingRight = true; // indica si el personaje está mirando a la derecha
    private float _chargeTime; // tiempo de carga del salto
    private bool _isAired; // indica si el personaje está en el aire
    private bool _hasBoots = false;
    private float _baseSpeed;
    private float _baseJump;

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
        Dead,
        Hurt,
        HurtFalling,
        PowerUp,
        Run
    }

    private PlayerState _playerState;

    // Create setter for playerState
    public void SetPlayerState(PlayerState newState)
    {
        _playerState = newState;
    }

    public void SetBoots(bool hasBoots)
    {
        _hasBoots = hasBoots;
        if (hasBoots)
        {
            moveSpeed = _baseSpeed + bootsSpeedBooster;
            jumpForce = _baseJump + bootsJumpBooster;
        }
        else
        {
            moveSpeed = _baseSpeed;
            jumpForce = _baseJump;
        }
    }

    public PlayerState GetPlayerState()
    {
        return _playerState;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        _baseJump = jumpForce;
        _baseSpeed = moveSpeed;
    }

    private void FixedUpdate()
    {
        var currentAnimation = animator.GetCurrentAnimatorStateInfo(0);
        if ((currentAnimation.IsName("FallingDamage") || currentAnimation.IsName("PowerUp")) &&
            currentAnimation.normalizedTime < 1f)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            return;
        }

        float moveInput = Input.GetAxisRaw("Horizontal");

        if ((moveInput > 0 && !_facingRight) || (moveInput < 0 && _facingRight))
        {
            Flip();
        }

        if (_playerState != PlayerState.Charge && _playerState != PlayerState.Attack &&
            _playerState != PlayerState.Defend)
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
        var currentAnimation = animator.GetCurrentAnimatorStateInfo(0);
        if (_playerState == PlayerState.Dead)
        {
            PlayAnimation();
            return;
        }

        if (currentAnimation.IsName("FallingDamage") && currentAnimation.normalizedTime < 1f)
        {
            return;
        }

        if (_playerState == PlayerState.PowerUp && currentAnimation.IsName("PowerUp") && currentAnimation.normalizedTime > 1f)
        {
            _playerState = PlayerState.Idle;
        }
        if (_playerState == PlayerState.PowerUp)
        {
            PlayAnimation();
            return;
        }
        
        // Actualizar las animaciones según el estado del jugador cuando está en el suelo
        if (IsGrounded())
        {
            if (_playerState == PlayerState.Hurt && !currentAnimation.IsName("TakeHit"))
            {
                PlayAnimation();
                return;
            }

            var jumping = CheckJump();

            if (!jumping)
            {
                if (_isAired || (currentAnimation.IsName("Landing") && currentAnimation.normalizedTime < 1f))
                {
                    _playerState = PlayerState.Land;
                    _isAired = false;
                }
                else if (_playerState == PlayerState.Hurt && currentAnimation.IsName("TakeHit") &&
                         currentAnimation.normalizedTime < 1f)
                {
                    _playerState = PlayerState.Hurt;
                }
                else if (Input.GetMouseButtonDown(0) ||
                         (currentAnimation.IsName("Attack") && currentAnimation.normalizedTime < 1f))
                {
                    _playerState = PlayerState.Attack;
                }
                else if (Input.GetMouseButton(1))
                {
                    _playerState = PlayerState.Defend;
                }
                else if (rb.velocity.x != 0)
                {
                    _playerState = _hasBoots ? PlayerState.Run : PlayerState.Walk;
                }
                else
                {
                    _playerState = PlayerState.Idle;
                }
            }
        }
        else
        {
            _isAired = true;

            if (_playerState == PlayerState.Hurt || _playerState == PlayerState.HurtFalling)
            {
                _playerState = PlayerState.HurtFalling;
            }
            else if (Mathf.Abs(rb.velocity.y) < 5)
            {
                _playerState = PlayerState.JumpPeak;
            }
            else if (rb.velocity.y > 0)
            {
                _playerState = PlayerState.Jump;
            }
            else
            {
                _playerState = PlayerState.Fall;
            }
        }

        PlayAnimation();
    }


    private void PlayAnimation()
    {
        if (_playerState == PlayerState.Walk)
        {
            animator.Play("Walking");
        }
        else if (_playerState == PlayerState.Run)
        {
            animator.Play("Running");
        }
        else if (_playerState == PlayerState.Idle)
        {
            animator.Play("Idle");
        }
        else if (_playerState == PlayerState.Jump)
        {
            animator.Play("FlyingUp");
        }
        else if (_playerState == PlayerState.JumpPeak)
        {
            animator.Play("JumpingReload");
        }
        else if (_playerState == PlayerState.Fall)
        {
            animator.Play("Falling");
        }
        else if (_playerState == PlayerState.Land)
        {
            animator.Play("Landing");
        }
        else if (_playerState == PlayerState.Charge)
        {
            animator.Play("Preparation");
        }
        else if (_playerState == PlayerState.Attack)
        {
            animator.Play("Attack");
        }
        else if (_playerState == PlayerState.Defend)
        {
            animator.Play("Defend");
        }
        else if (_playerState == PlayerState.Dead)
        {
            animator.Play("Death");
            Death();
        }
        else if (_playerState == PlayerState.Hurt)
        {
            animator.Play("TakeHit");
        }
        else if (_playerState == PlayerState.HurtFalling)
        {
            animator.Play("FallingDamage");
        }
        else if (_playerState == PlayerState.PowerUp)
        {
            animator.Play("PowerUp");
        }
    }

    private bool CheckJump()
    {
        if (_playerState == PlayerState.Dead || _playerState == PlayerState.Hurt)
        {
            return false;
        }

        // Verificar si el jugador está cargando el salto
        if (Input.GetKey(KeyCode.Space))
        {
            _playerState = PlayerState.Charge;
            _chargeTime += Time.deltaTime;
            return true;
        }

        if (_playerState == PlayerState.Charge)
        {
            Jump();
            return true;
        }

        return false;
    }

    private void Jump()
    {
        _playerState = PlayerState.Jump;
        // Calcular la fuerza del salto
        var jumpPower = jumpForce;
        if (_chargeTime > 0)
        {
            if (_chargeTime > maxChargeTime)
            {
                _chargeTime = maxChargeTime;
            }

            jumpPower += (jumpForce * _chargeTime);
        }

        // Aplicar la fuerza del salto al jugador
        rb.velocity = new Vector2(rb.velocity.x, jumpPower);
        _chargeTime = 0f;
        AudioManager.Instance.PlaySound2D("PlayerJump");
    }

    private void Flip()
    {
        _facingRight = !_facingRight;
        transform.Rotate(Vector3.up, 180f);
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, maxDistance, ground);
    }

    private void Death()
    {
        enabled = false;
    }
}