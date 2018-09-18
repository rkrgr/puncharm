using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    const int moveLeft = -1;
    const int moveRight = 1;

    public int health = 100;

    public float speed;

    public Rigidbody2D player;

    Rigidbody2D rb;

    int moveDirection = moveLeft;
    bool isFacingLeft = true;

    void Start () {
        rb = GetComponent<Rigidbody2D>();
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
        if (isFacingLeft && IsMovingRight() || IsFacingRight() && IsMovingLeft())
        {
            FlipCharacter();
        }
        rb.velocity = new Vector2(moveDirection * speed * Time.fixedDeltaTime, rb.velocity.y);
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
        health -= damage;
        if(health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
