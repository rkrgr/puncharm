using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health), typeof(Rigidbody2D))]
public abstract class Enemy : MonoBehaviour {

    public int damage;

    protected Health health;

    public Vector2 FacingDirection { get; set; }
    public float moveSpeed;

    protected Rigidbody2D rb;

    protected virtual void Awake()
    {
        health = GetComponent<Health>();
        rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void Start()
    {
        FacingDirection = Vector2.right;
    }

    public virtual void TakeDamage(int receiveDamage)
    {
        health.TakeDamage(receiveDamage);
        if (health.IsDead())
        {
            Destroy(gameObject);
        }
    }

    protected void Die()
    {
        Destroy(gameObject);
    }

    protected void NormalMove()
    {
        rb.velocity = new Vector2(FacingDirection.x * moveSpeed * Time.deltaTime, rb.velocity.y);
    }

    protected void FlipCharacter()
    {
        Vector3 scale = transform.localScale;
        scale.x = -scale.x;
        transform.localScale = scale;

        if (FacingDirection == Vector2.right)
        {
            FacingDirection = Vector2.left;
        }
        else
        {
            FacingDirection = Vector2.right;
        }
    }

}
