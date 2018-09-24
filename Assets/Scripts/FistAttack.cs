using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FistAttack : MonoBehaviour {

    public GameObject fistPrefab;
    public float offset = 1f;
    public float speed = 25f;
    public float maxDistance = 5f;
    
    PlayerMovement movement;
    GameObject fist;

    bool isExpanding = false;
    bool isRetracting = false;

    void Awake()
    {
        movement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if(isExpanding)
        {
            if (movement.isFacingRight)
            {
                fist.transform.position = new Vector2(fist.transform.position.x + speed * Time.deltaTime, transform.position.y);

                if(fist.transform.position.x >= transform.position.x + maxDistance)
                {
                    isExpanding = false;
                    isRetracting = true;
                }
            }
            else // facing left
            {
                fist.transform.position = new Vector2(fist.transform.position.x - speed * Time.deltaTime, transform.position.y);

                if(fist.transform.position.x <= transform.position.x - maxDistance)
                {
                    isExpanding = false;
                    isRetracting = true;
                }
            }
        }
        else if (isRetracting)
        {
            if (movement.isFacingRight)
            {
                fist.transform.position = new Vector2(fist.transform.position.x - speed * Time.deltaTime, transform.position.y);

                if(fist.transform.position.x <= transform.position.x + offset)
                {
                    Destroy(fist);
                    isRetracting = false;
                }
            }
            else // is facing left
            {
                fist.transform.position = new Vector2(fist.transform.position.x + speed * Time.deltaTime, transform.position.y);

                if(fist.transform.position.x >= transform.position.x - offset)
                {
                    Destroy(fist);
                    isRetracting = false;
                }
            }
        }

        if (!IsPunching() && Input.GetButtonDown("Fire1"))
        {
            Punch();
        }
    }

    void OnTriggerEnter2D()
    {

    }

    void Punch()
    {
        isExpanding = true;

        if (movement.IsFacingLeft())
        {
            fist = Instantiate(fistPrefab, new Vector2(transform.position.x - offset, transform.position.y), transform.rotation);
        }
        else
        {
            fist = Instantiate(fistPrefab, new Vector2(transform.position.x + offset, transform.position.y), transform.rotation);
        }
    }

    public bool IsPunching()
    {
        return isExpanding || isRetracting;
    }

}
