using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float speed;
    public float jumpForce;

    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;

    public Animator animator;

    Rigidbody2D rb;

    bool jump;
    bool grappled = true;
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
    }

	void FixedUpdate ()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
        animator.SetBool("IsOnGround", isGrounded);

        if (jump && isGrounded)
        {
            rb.velocity = Vector2.up * jumpForce;
            jump = false;
        }
        else if(grappled)
        {
            if(moveInput != 0)
            {
                // nicht jedes mal adden sondern steady
                Vector2 move = new Vector2(moveInput * speed * Time.fixedDeltaTime, moveInput * speed * Time.fixedDeltaTime) * 0.2f;
                rb.velocity = rb.velocity * move;
            }
        }
        else
        {
            rb.velocity = new Vector2(moveInput * speed * Time.fixedDeltaTime, rb.velocity.y);
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
