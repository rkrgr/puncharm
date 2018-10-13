using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderBoss : Enemy {

    public float speed = 500f;
    public float gravitySpeed = 500f;
    public LayerMask obstacle;

    public float jumpCooldown = 2f;
    float waitForNextJump;

    Vector2 moveDirection;
    float colliderHalfWidth;
    float colliderHalfHeight;

    bool jump = false;

    GameObject player;

    protected override void Awake()
    {
        base.Awake();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    protected override void Start()
    {
        base.Start();
        BoxCollider2D col = GetComponent<BoxCollider2D>();
        colliderHalfWidth = col.size.x * transform.lossyScale.x / 2;
        colliderHalfHeight = col.size.y * transform.lossyScale.y / 2;
        SetMoveLeft();
    }

    void Update()
    {
        if (!jump && waitForNextJump <= 0f && (transform.position.y < player.transform.position.y + .5f && transform.position.y > player.transform.position.y - .1f
            || transform.position.x < player.transform.position.x + .5f && transform.position.x > player.transform.position.x - .5f))
        {
            jump = true;
            transform.Rotate(Vector3.forward, 180f);
            waitForNextJump = jumpCooldown;
        }

        if(waitForNextJump > 0f)
        {
            waitForNextJump -= Time.deltaTime;
        }
    }

	void FixedUpdate () {

        if(!jump)
        {
            if (IsMovingRight() && CollisonRight())
            {
                RotateRight();
                SetMoveRight();
            }
            else if(IsMovingLeft() && CollisonLeft())
            {
                RotateLeft();
                SetMoveLeft();
            }

            Move();
        }
        else // jump
        {
            rb.velocity = -transform.up * speed * 2 * Time.deltaTime;

            if (CollisonDown())
            {
                jump = false;

                if (transform.rotation.eulerAngles.z == 0f)
                {
                    if(transform.position.x < player.transform.position.x)
                    {
                        SetMoveLeft();
                    }
                    else
                    {
                        SetMoveRight();
                    }
                }
                else if(transform.rotation.eulerAngles.z == 180f)
                {
                    if (transform.position.x < player.transform.position.x)
                    {
                        SetMoveRight();
                    }
                    else
                    {
                        SetMoveLeft();
                    }
                }
                else if(transform.rotation.eulerAngles.z == 90f)
                {
                    if (transform.position.y < player.transform.position.y)
                    {
                        SetMoveLeft();
                    }
                    else
                    {
                        SetMoveRight();
                    }
                }
                else
                {
                    if (transform.position.y < player.transform.position.y)
                    {
                        SetMoveRight();
                    }
                    else
                    {
                        SetMoveLeft();
                    }
                }
            }
        }
	}

    void RotateRight()
    {
        transform.Rotate(Vector3.forward, 90f);
    }

    void RotateLeft()
    {
        transform.Rotate(Vector3.forward, -90f);
    }

    void Move()
    {
        rb.velocity = moveDirection * speed * Time.deltaTime;
    }

    void SetMoveLeft()
    {
        moveDirection = -transform.right;
    }

    void SetMoveRight()
    {
        moveDirection = transform.right;
    }

    bool IsMovingLeft()
    {
        return (Vector3) moveDirection == -transform.right;
    }

    bool IsMovingRight()
    {
        return (Vector3)moveDirection == transform.right;
    }

    bool CollisonLeft()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position - transform.right * colliderHalfWidth, -transform.right, 0.05f, obstacle);
        return hit.collider != null;
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
