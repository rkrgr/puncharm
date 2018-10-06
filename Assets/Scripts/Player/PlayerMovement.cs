using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float speed;
    public float jumpForce;

    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    public float swingVelocityAddSpeed = 0.1f;

    public float checkRadius;
    public LayerMask whatIsGround;

    public Animator animator;

    Rigidbody2D rb;
    FistAttack fistAttack;
    PlayerGroundCheck groundChecker;

    bool jump;
    bool grappled;
    float moveInput;
    internal bool isFacingRight = true;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        fistAttack = GetComponent<FistAttack>();
        groundChecker = GetComponentInChildren<PlayerGroundCheck>();
    }

    void Update()
    {
        moveInput = Input.GetAxisRaw("Horizontal");
        animator.SetFloat("Speed", Mathf.Abs(moveInput));

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }

        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }

    void FixedUpdate()
    {
        animator.SetBool("IsOnGround", groundChecker.IsGrounded);

        if (jump && groundChecker.IsGrounded)
        {
            Jump();
        }
        else if (grappled)
        {
            Swing();
        }
        else
        {
            Move();
        }

        if (isFacingRight && IsMovingLeft() || IsFacingLeft() && IsMovingRight())
        {
            FlipCharacter();
        }

        if (!groundChecker.IsGrounded)
        {
            animator.SetFloat("VerticalVelocity", rb.velocity.y);
        }
        else
        {
            animator.SetFloat("VerticalVelocity", 0);
        }
    }

    private void Swing()
    {
        if (IsMovingLeft())
        {
            SwingLeft();
        }
        else if (IsMovingRight())
        {
            SwingRight();
        }
    }

    private void Jump()
    {
        rb.velocity = Vector2.up * jumpForce;
        jump = false;
    }

    private void Move()
    {
        rb.velocity = new Vector2(moveInput * speed * Time.fixedDeltaTime, rb.velocity.y);
    }

    private void SwingRight()
    {
        rb.velocity = new Vector2(rb.velocity.x + swingVelocityAddSpeed * Time.fixedDeltaTime, rb.velocity.y);
    }

    private void SwingLeft()
    {
        rb.velocity = new Vector2(rb.velocity.x - swingVelocityAddSpeed * Time.fixedDeltaTime, rb.velocity.y);
    }

    public bool IsFacingLeft()
    {
        return !isFacingRight;
    }

    bool IsMovingRight()
    {
        return moveInput > 0;
    }

    bool IsMovingLeft()
    {
        return moveInput < 0;
    }

    public void Stop()
    {
        rb.velocity = Vector2.zero;
    }

    void FlipCharacter()
    {
        if (!fistAttack.IsPunching())
        {
            isFacingRight = !isFacingRight;
            Vector3 scaler = transform.localScale;
            scaler.x = -scaler.x;
            transform.localScale = scaler;
        }
    }

    public bool IsGrounded
    {
        get { return groundChecker.IsGrounded; }
    }
}
