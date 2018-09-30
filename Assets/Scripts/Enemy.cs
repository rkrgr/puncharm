using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    const int moveLeft = -1;
    const int moveRight = 1;

    public float aggroRange = 100f;

    public float speed;
    public int damage;

    public float pushBackForce = 100f;
    public float hitCooldown = 1f;

    public float playerDistance = 1f;

    public float[] patArea = new float[2];
    public float patSpeed;

    GameObject player;

    Rigidbody2D rb;

    int moveDirection = moveLeft;
    bool isFacingLeft = true;

    bool isHit = false;

    Animator animator;
    Health health;

    void Awake () {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player");
        health = GetComponent<Health>();
	}
	
	void Update () {
        if (!isHit)
        {
            float playerDis = (player.transform.position - transform.position).sqrMagnitude;
            if (playerDis < aggroRange * aggroRange)
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
                    rb.velocity = new Vector2(moveDirection * speed * Time.deltaTime, rb.velocity.y);
                }
                else
                {
                    rb.velocity = new Vector2(0f, rb.velocity.y);
                }
            }
            else
            {
                rb.velocity = new Vector2(moveDirection * patSpeed * Time.deltaTime, rb.velocity.y);
            }
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
            health.TakeDamage(damage);
            
            if(health.IsDead())
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
