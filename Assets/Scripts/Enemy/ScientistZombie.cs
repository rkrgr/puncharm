using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScientistZombie : Enemy {

    public float aggroRange = 100f;

    public float normalSpeed;
    public float chargeSpeed;

    public float chargeTime;
    float chargeTimeLeft;

    public float playerDistance = 1f;

    public LayerMask ground;

    GameObject player;

    Rigidbody2D rb;
    BoxCollider2D enemyCollider;
    Vector3 localPositionGroundLeft;
    Vector3 localPositionGroundRight;

    Vector2 moveDirection = Vector2.right;

    Animator animator;

    protected override void Awake () {
        base.Awake();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player");
        enemyCollider = GetComponent<BoxCollider2D>();
	}

    void Start()
    {
        float halfHorizontalLength = enemyCollider.size.x * transform.lossyScale.x / 2;
        float halfVerticalLength = enemyCollider.size.y * transform.lossyScale.y / 2;
        localPositionGroundLeft = new Vector2(-halfHorizontalLength, -halfVerticalLength);
        localPositionGroundRight = new Vector2(halfHorizontalLength, -halfVerticalLength);
    }
	
	void FixedUpdate () {
        if (IsSeeingPlayer())
        {
            StartCharge();
        }

        if (chargeTimeLeft > 0f) {
            rb.velocity = new Vector2(moveDirection.x * chargeSpeed * Time.deltaTime, rb.velocity.y);
            chargeTimeLeft -= Time.deltaTime;
        }
        else
        {
            rb.velocity = new Vector2(moveDirection.x * normalSpeed * Time.deltaTime, rb.velocity.y);
        }

        if (IsMovingLeft() && (!HitGroundLeft() && HitGroundRight() || HitWallLeft())
            || IsMovingRight() && (HitGroundLeft() && !HitGroundRight() || HitWallRight()))
        {
            FlipMoveDirection();
            FlipCharacter();
            chargeTimeLeft = 0f;
        }
	}

    private void StartCharge()
    {
        chargeTimeLeft = chargeTime;
        if (rb.position.x > player.transform.position.x)
        {
            moveDirection = Vector2.left;
        }
        else
        {
            moveDirection = Vector2.right;
        }
    }

    bool HitGroundLeft()
    {
        return Physics2D.Raycast(transform.position + localPositionGroundLeft, Vector2.down, 0.1f, ground).collider != null;
    }

    bool HitGroundRight()
    {
        return Physics2D.Raycast(transform.position + localPositionGroundRight, Vector2.down, 0.1f, ground).collider != null;
    }

    bool HitWallLeft()
    {
        return Physics2D.Raycast(transform.position + localPositionGroundLeft, moveDirection, 0.1f, ground).collider != null;
    }

    bool HitWallRight()
    {
        return Physics2D.Raycast(transform.position + localPositionGroundRight, moveDirection, 0.1f, ground).collider != null;
    }

    bool IsMovingLeft()
    {
        return moveDirection == Vector2.left;
    }

    bool IsMovingRight()
    {
        return moveDirection == Vector2.right;
    }

    void FlipMoveDirection()
    {
        moveDirection = -moveDirection;
    }

    void FlipCharacter()
    {
        Vector3 scale = transform.localScale;
        scale.x = -scale.x;
        transform.localScale = scale;
    }

    bool IsSeeingPlayer()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, moveDirection, aggroRange, LayerMask.GetMask("Player"));
        return hit.collider != null;
    }

}
