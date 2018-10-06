using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScientistZombie : Enemy {

    const int moveLeft = -1;
    const int moveRight = 1;

    public float aggroRange = 100f;

    public float normalSpeed;
    public float chargeSpeed;

    public float playerDistance = 1f;

    public LayerMask ground;

    GameObject player;
    GameObject groundCheckForward;

    Rigidbody2D rb;

    int moveDirection = moveLeft;
    bool isFacingLeft = true;

    Animator animator;

    protected override void Awake () {
        base.Awake();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player");

        groundCheckForward = GameObject.Find("GroundCheckForward");
	}
	
	void FixedUpdate () {
        if (IsSeeingPlayer())
        {
            if(rb.position.x > player.transform.position.x)
            {
                moveDirection = moveLeft;
            }
            else
            {
                moveDirection = moveRight;
            }

            if (isFacingLeft && IsMovingRight() || IsFacingRight() && IsMovingLeft())
            {
                FlipCharacter();
            }

            if(Mathf.Abs(player.transform.position.x - transform.position.x) > playerDistance)
            {
                rb.velocity = new Vector2(moveDirection * chargeSpeed * Time.deltaTime, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(0f, rb.velocity.y);
            }
        }
        else
        {
            RaycastHit2D hit = Physics2D.Raycast(groundCheckForward.transform.position, Vector2.down, 0.1f, ground);

            if(hit.collider == null)
            {
                FlipMoveDirection();
                FlipCharacter();
            }

            rb.velocity = new Vector2(moveDirection * normalSpeed * Time.deltaTime, rb.velocity.y);
        }
	}

    bool IsFacingRight()
    {
        return !isFacingLeft;
    }

    bool IsMovingLeft()
    {
        return moveDirection == moveLeft;
    }

    bool IsMovingRight()
    {
        return moveDirection == moveRight;
    }

    void FlipMoveDirection()
    {
        moveDirection = -moveDirection;
    }

    void FlipCharacter()
    {
        isFacingLeft = !isFacingLeft;
        Vector3 scale = transform.localScale;
        scale.x = -scale.x;
        transform.localScale = scale;
    }

    bool IsSeeingPlayer()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, aggroRange, LayerMask.GetMask("Player"));
        return hit.collider != null;
    }

}
