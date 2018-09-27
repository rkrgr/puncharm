using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FistAttack : MonoBehaviour {

    public GameObject fistPrefab;
    public float speed = 25f;
    public float maxDistance = 5f;

    PlayerMovement movement;
    GameObject fist;

    bool isExpanding = false;
    bool isRetracting = false;
    Vector3 punchDirection;

    Vector3 oldPlayerPos;

    void Awake()
    {
        movement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if (isExpanding)
        {
            Expand();
            if(IsFullyExpanded())
            {
                isExpanding = false;
                isRetracting = true;
            }
        }
        else if (isRetracting)
        {
            Retract();
            if (IsFullyRetracted())
            {
                Destroy(fist);
                isRetracting = false;
            }
        }

        if (!IsPunching())
        {
            if (Input.GetButtonDown("PunchUpLeft"))
            {
                punchDirection = Vector2.up + Vector2.left;
                Punch();
            }
            else if (Input.GetButtonDown("PunchUpRight"))
            {
                punchDirection = Vector2.up + Vector2.right;
                Punch();
            }
            else if(Input.GetButtonDown("Punch"))
            {
                float verticalInput = Input.GetAxisRaw("Vertical");
                float horizontalInput = Input.GetAxisRaw("Horizontal");

                if (verticalInput > 0.5f)
                {
                    punchDirection = Vector2.up;
                }
                else if (verticalInput < -0.5f)
                {
                    if (horizontalInput > 0.5f)
                    {
                        punchDirection = Vector2.down + Vector2.right;
                    }
                    else if (horizontalInput < -0.5f)
                    {
                        punchDirection = Vector2.down + Vector2.left;
                    }
                    else
                    {
                        punchDirection = Vector2.down;
                    }
                }
                else
                {
                    if (movement.IsFacingLeft())
                    {
                        punchDirection = Vector2.left;
                    }
                    else
                    {
                        punchDirection = Vector2.right;
                    }
                }
                Punch();
            }
        }
        oldPlayerPos = transform.position;
    }

    void Punch()
    {
        isExpanding = true;
        fist = Instantiate(fistPrefab, new Vector2(transform.position.x, transform.position.y), transform.rotation);
    }

    public void Expand()
    {
        fist.transform.position += transform.position - oldPlayerPos + punchDirection.normalized * speed * Time.deltaTime;
    }

    public void Retract()
    {
        fist.transform.position += transform.position - oldPlayerPos - punchDirection.normalized * speed * Time.deltaTime;
    }

    public bool IsFullyExpanded()
    {
        float dis = (fist.transform.position - transform.position).sqrMagnitude;
        return dis >= maxDistance * maxDistance;
    }

    public bool IsFullyRetracted()
    {
        if (punchDirection.y > 0)
        {
            return fist.transform.position.y <= transform.position.y;
        }
        if (punchDirection.y < 0)
        {
            return fist.transform.position.y >= transform.position.y;
        }
        if (punchDirection.x > 0)
        {
            return fist.transform.position.x <= transform.position.x;
        }
        return fist.transform.position.x <= transform.position.x;
    }

    public bool IsPunching()
    {
        return isExpanding || isRetracting;
    }

    public void CollideWithWall()
    {
        isExpanding = false;
        isRetracting = true;
    }

}
