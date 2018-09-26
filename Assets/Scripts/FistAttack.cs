using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FistAttack : MonoBehaviour {

    enum PunchDirection { UP, DOWN, LEFT, RIGHT, UPLEFT, UPRIGHT, DOWNLEFT, DOWNRIGHT }

    public GameObject fistPrefab;
    public float speed = 25f;
    public float maxDistance = 5f;

    PlayerMovement movement;
    GameObject fist;

    bool isExpanding = false;
    bool isRetracting = false;
    PunchDirection punchDirection;

    Vector3 oldPlayerPos;

    void Awake()
    {
        movement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if (isExpanding)
        {
            if (punchDirection == PunchDirection.RIGHT)
            {
                fist.transform.position = new Vector2(fist.transform.position.x + speed * Time.deltaTime, transform.position.y);

                if (fist.transform.position.x >= transform.position.x + maxDistance)
                {
                    isExpanding = false;
                    isRetracting = true;
                }
            }
            else if (punchDirection == PunchDirection.LEFT)
            {
                fist.transform.position = new Vector2(fist.transform.position.x - speed * Time.deltaTime, transform.position.y);

                if (fist.transform.position.x <= transform.position.x - maxDistance)
                {
                    isExpanding = false;
                    isRetracting = true;
                }
            }
            else if (punchDirection == PunchDirection.UP)
            {
                fist.transform.position = new Vector2(transform.position.x, fist.transform.position.y + speed * Time.deltaTime);

                if (fist.transform.position.y >= transform.position.y + maxDistance)
                {
                    isExpanding = false;
                    isRetracting = true;
                }
            }
            else if (punchDirection == PunchDirection.DOWN)
            {
                fist.transform.position = new Vector2(transform.position.x, fist.transform.position.y - speed * Time.deltaTime);

                if (fist.transform.position.y <= transform.position.y - maxDistance)
                {
                    isExpanding = false;
                    isRetracting = true;
                }
            }
            else if (punchDirection == PunchDirection.UPLEFT)
            {
                fist.transform.position += transform.position - oldPlayerPos + (Vector3.up + Vector3.left).normalized * speed * Time.deltaTime;

                float dis = (fist.transform.position - transform.position).sqrMagnitude;
                if (dis >= maxDistance * maxDistance)
                {
                    isExpanding = false;
                    isRetracting = true;
                }
            }
            else if (punchDirection == PunchDirection.UPRIGHT)
            {
                fist.transform.position += transform.position - oldPlayerPos + (Vector3.up + Vector3.right).normalized * speed * Time.deltaTime;

                float dis = (fist.transform.position - transform.position).sqrMagnitude;
                if (dis >= maxDistance * maxDistance)
                {
                    isExpanding = false;
                    isRetracting = true;
                }
            }
            else if (punchDirection == PunchDirection.DOWNLEFT)
            {
                fist.transform.position += transform.position - oldPlayerPos + (Vector3.down + Vector3.left).normalized * speed * Time.deltaTime;

                float dis = (fist.transform.position - transform.position).sqrMagnitude;
                if (dis >= maxDistance * maxDistance)
                {
                    isExpanding = false;
                    isRetracting = true;
                }
            }
            else if (punchDirection == PunchDirection.DOWNRIGHT)
            {
                fist.transform.position += transform.position - oldPlayerPos + (Vector3.down + Vector3.right).normalized * speed * Time.deltaTime;

                float dis = (fist.transform.position - transform.position).sqrMagnitude;
                if (dis >= maxDistance * maxDistance)
                {
                    isExpanding = false;
                    isRetracting = true;
                }
            }
        }
        else if (isRetracting)
        {
            if (punchDirection == PunchDirection.RIGHT)
            {
                fist.transform.position = new Vector2(fist.transform.position.x - speed * Time.deltaTime, transform.position.y);

                if (fist.transform.position.x <= transform.position.x)
                {
                    Destroy(fist);
                    isRetracting = false;
                }
            }
            else if (punchDirection == PunchDirection.LEFT)
            {
                fist.transform.position = new Vector2(fist.transform.position.x + speed * Time.deltaTime, transform.position.y);

                if (fist.transform.position.x >= transform.position.x)
                {
                    Destroy(fist);
                    isRetracting = false;
                }
            }
            else if (punchDirection == PunchDirection.UP)
            {
                fist.transform.position = new Vector2(transform.position.x, fist.transform.position.y - speed * Time.deltaTime);

                if (fist.transform.position.y <= transform.position.y)
                {
                    Destroy(fist);
                    isRetracting = false;
                }
            }
            else if (punchDirection == PunchDirection.DOWN)
            {
                fist.transform.position = new Vector2(transform.position.x, fist.transform.position.y + speed * Time.deltaTime);

                if (fist.transform.position.y >= transform.position.y)
                {
                    Destroy(fist);
                    isRetracting = false;
                }
            }
            else if (punchDirection == PunchDirection.UPLEFT)
            {
               fist.transform.position += transform.position - oldPlayerPos + (Vector3.down + Vector3.right).normalized * speed * Time.deltaTime;

               if (fist.transform.position.y <= transform.position.y)
                {
                    Destroy(fist);
                    isRetracting = false;
                }
            }
            else if (punchDirection == PunchDirection.UPRIGHT)
            {
                fist.transform.position += transform.position - oldPlayerPos + (Vector3.down + Vector3.left).normalized * speed * Time.deltaTime;

                if (fist.transform.position.y <= transform.position.y)
                {
                    Destroy(fist);
                    isRetracting = false;
                }
            }
            else if (punchDirection == PunchDirection.DOWNLEFT)
            {
                fist.transform.position += transform.position - oldPlayerPos + (Vector3.up + Vector3.right).normalized * speed * Time.deltaTime;

                if (fist.transform.position.y >= transform.position.y)
                {
                    Destroy(fist);
                    isRetracting = false;
                }
            }
            else if (punchDirection == PunchDirection.DOWNRIGHT)
            {
                fist.transform.position += transform.position - oldPlayerPos + (Vector3.up + Vector3.left).normalized * speed * Time.deltaTime;

                if (fist.transform.position.y >= transform.position.y)
                {
                    Destroy(fist);
                    isRetracting = false;
                }
            }
        }

        if (!IsPunching() && Input.GetButtonDown("Fire1"))
        {
            float verticalInput = Input.GetAxisRaw("Vertical");
            float horizontalInput = Input.GetAxisRaw("Horizontal");
            if (verticalInput > 0.5f)
            {
                if(horizontalInput > 0.5f)
                {
                    punchDirection = PunchDirection.UPRIGHT;
                }
                else if(horizontalInput < -0.5f)
                {
                    punchDirection = PunchDirection.UPLEFT;
                }
                else
                {
                    punchDirection = PunchDirection.UP;
                }
            }
            else if (verticalInput < -0.5f)
            {
                if (horizontalInput > 0.5f)
                {
                    punchDirection = PunchDirection.DOWNRIGHT;
                }
                else if (horizontalInput < -0.5f)
                {
                    punchDirection = PunchDirection.DOWNLEFT;
                }
                else
                {
                    punchDirection = PunchDirection.DOWN;
                }
            }
            else
            {
                if (movement.IsFacingLeft())
                {
                    punchDirection = PunchDirection.LEFT;
                }
                else
                {
                    punchDirection = PunchDirection.RIGHT;
                }
            }
            Punch();
        }

        oldPlayerPos = transform.position;
    }

    void Punch()
    {
        isExpanding = true;
        fist = Instantiate(fistPrefab, new Vector2(transform.position.x, transform.position.y), transform.rotation);
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
