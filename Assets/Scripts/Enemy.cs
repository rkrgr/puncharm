using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    const int moveLeft = -1;
    const int moveRight = 1;
    public int health = 100;

    public float speed;
    public int damage;

    public float pushBackForce = 100f;
    public float hitCooldown = 1f;

    public Rigidbody2D player;

    Rigidbody2D rb;

    int moveDirection = moveLeft;
    bool isFacingLeft = true;

    bool isHit = false;

    Animator animator;

    void Awake () {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
	}
	
	void Update () {
        if(rb.position.x > player.position.x)
        {
            moveDirection = moveLeft;
        }
        else
        {
            moveDirection = moveRight;
        }
	}

    void FixedUpdate()
    {
        if (!isHit)
        {
            if (isFacingLeft && IsMovingRight() || IsFacingRight() && IsMovingLeft())
            {
                FlipCharacter();
            }
            rb.velocity = new Vector2(moveDirection * speed * Time.deltaTime, rb.velocity.y);
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

    void FlipCharacter()
    {
        isFacingLeft = !isFacingLeft;
        Vector3 scale = transform.localScale;
        scale.x = -scale.x;
        transform.localScale = scale;
    }

    public void TakeDamage(int damage)
    {
        if(!isHit)
        {
            isHit = true;
            health -= damage;
            if(health <= 0)
            {
                Die();
                return;
            }
            rb.velocity = Vector2.zero;
            rb.AddForce(new Vector2(-moveDirection * pushBackForce, 0f));
            animator.SetLayerWeight(1, 1f);
            Invoke("ResetHit", hitCooldown);
        }
    }

    void ResetHit()
    {
        isHit = false;
        animator.SetLayerWeight(1, 0f);
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
