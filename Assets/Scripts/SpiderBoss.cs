using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderBoss : MonoBehaviour {

    public float speed = 500f;
    public float gravitySpeed = 500f;

    Vector2 moveDirection;
    Vector2 gravityDirection = Vector2.down;
    float colliderHalfWidth;

    Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        BoxCollider2D col = GetComponent<BoxCollider2D>();
        colliderHalfWidth = col.size.x / 4;
    }

	void Update () {
        SetMoveRight();

        if (CollisonRight())
        {
            transform.Rotate(Vector3.forward, 90f);
            gravityDirection = -transform.up;
        }

        Move();
	}

    void Move()
    {
        rb.velocity = (moveDirection + gravityDirection) * speed * Time.deltaTime;
    }

    void SetMoveRight()
    {
        moveDirection = transform.right;
    }

    bool CollisonRight()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position + transform.right * colliderHalfWidth, transform.right, 0.1f, LayerMask.GetMask("Obstacle"));
        return hit.collider != null;
    }
}
