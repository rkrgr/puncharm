using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float speed;
    public float jumpForce;

    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    public float swingVelocityAddSpeed = 0.1f;

    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;

    public Animator animator;

    Rigidbody2D rb;

    bool jump;
    bool grappled;
    float moveInput;
    bool isFacingRight = true;
    bool isGrounded;

    void Start () {
        rb = GetComponent<Rigidbody2D>();
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

	void FixedUpdate ()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
        animator.SetBool("IsOnGround", isGrounded);

        if (jump && isGrounded)
        {
            Jump();
        }
        else if(grappled)
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

        if(!isGrounded)
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

    bool IsFacingLeft()
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

    void FlipCharacter()
    {
        isFacingRight = !isFacingRight;
        Vector3 scaler = transform.localScale;
        scaler.x = -scaler.x;
        transform.localScale = scaler;
    }
}
