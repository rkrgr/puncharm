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

    LayerMask obstacle;

    GameObject player;

    Rigidbody2D rb;
    Vector3 localPositionGroundLeft;
    Vector3 localPositionGroundRight;

    Vector2 moveDirection = Vector2.right;

    Animator animator;

    protected override void Awake () {
        base.Awake();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player");
        obstacle = LayerMask.GetMask("Obstacle");
	}

    void Start()
    {
        BoxCollider2D enemyCollider = GetComponent<BoxCollider2D>();
        float halfHorizontalLength = enemyCollider.size.x * transform.lossyScale.x / 2;
        float halfVerticalLength = enemyCollider.size.y * transform.lossyScale.y / 2;
        localPositionGroundLeft = new Vector2(-halfHorizontalLength, -halfVerticalLength);
        localPositionGroundRight = new Vector2(halfHorizontalLength, -halfVerticalLength);
    }
	
	void FixedUpdate ()
    {
        if (IsSeeingPlayer())
        {
            StartCharge();
        }

        if (IsCharging())
        {
            Charge();
        }
        else
        {
            Move();
        }

        Debug.DrawRay(transform.position + localPositionGroundRight, moveDirection);
        if (IsMovingLeft() && (!HitGroundLeft() && HitGroundRight() || HitWallLeft())
            || IsMovingRight() && (HitGroundLeft() && !HitGroundRight() || HitWallRight()))
        {
            FlipMoveDirection();
            FlipCharacter();
            StopCharging();
        }
    }

    private void StopCharging()
    {
        chargeTimeLeft = 0f;
    }

    private void Move()
    {
        rb.velocity = new Vector2(moveDirection.x * normalSpeed * Time.deltaTime, rb.velocity.y);
    }

    private void Charge()
    {
        rb.velocity = new Vector2(moveDirection.x * chargeSpeed * Time.deltaTime, rb.velocity.y);
        chargeTimeLeft -= Time.deltaTime;
    }

    private bool IsCharging()
    {
        return chargeTimeLeft > 0f;
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
        return Physics2D.Raycast(transform.position + localPositionGroundLeft, Vector2.down, 0.1f, obstacle).collider != null;
    }

    bool HitGroundRight()
    {
        return Physics2D.Raycast(transform.position + localPositionGroundRight, Vector2.down, 0.1f, obstacle).collider != null;
    }

    bool HitWallLeft()
    {
        return Physics2D.Raycast(transform.position + localPositionGroundLeft, moveDirection, 0.1f, obstacle).collider != null;
    }

    bool HitWallRight()
    {
        return Physics2D.Raycast(transform.position + localPositionGroundRight, moveDirection, 0.1f, obstacle).collider != null;
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
