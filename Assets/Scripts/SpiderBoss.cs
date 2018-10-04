using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderBoss : MonoBehaviour {

    public float speed = 500f;
    public float gravitySpeed = 500f;
    public LayerMask obstacle;

    Vector2 moveDirection;
    Vector2 gravityDirection = Vector2.down;
    float colliderHalfWidth;
    float colliderHalfHeight;

    bool jump = false;

    Rigidbody2D rb;

    GameObject player;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Start()
    {
        BoxCollider2D col = GetComponent<BoxCollider2D>();
        colliderHalfWidth = col.size.x * transform.lossyScale.x / 2;
        colliderHalfHeight = col.size.y * transform.lossyScale.y / 2;
        SetMoveRight();
    }

    void Update()
    {
        if (!jump && (transform.position.y < player.transform.position.y + .5f && transform.position.y > player.transform.position.y - .1f
            || transform.position.x < player.transform.position.x + .5f && transform.position.x > player.transform.position.x - .5f))
        {
            jump = true;
            transform.Rotate(Vector3.forward, 180f);
        }
        Debug.Log(jump);
    }

	void FixedUpdate () {

        if(!jump)
        {
            if (CollisonRight())
            {
                transform.Rotate(Vector3.forward, 90f);
                gravityDirection = -transform.up;
            }

            Move();
        }
        else // jump
        {
            gravityDirection = transform.up;
            rb.velocity = -transform.up * speed * 2 * Time.deltaTime;

            if (CollisonDown())
            {
                jump = false;
            }
        }
	}

    void Move()
    {
        rb.velocity = transform.right * speed * Time.deltaTime;
    }

    void SetMoveRight()
    {
        moveDirection = transform.right;
    }

    bool CollisonRight()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position + transform.right * colliderHalfWidth, transform.right, 0.05f, obstacle);
        return hit.collider != null;
    }
    bool CollisonDown()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position - transform.up * colliderHalfHeight, -transform.up, 0.5f, obstacle);
        return hit.collider != null;
    }
}
