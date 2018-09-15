using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    const int moveLeft = -1;
    const int moveRight = 1;

    public float speed;

    public Rigidbody2D player;

    Rigidbody2D rb;

    int moveDirection = moveLeft;

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
        rb.velocity = new Vector2(moveDirection * speed * Time.fixedDeltaTime, rb.velocity.y);
    }
}
