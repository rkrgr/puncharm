using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Punch : MonoBehaviour {

    public GameObject impactEffect;

    public float punchSpeed;
    public float punchLoadSpeed;

    public float minPunchDistance;
    public float maxPunchDistance;

    private float currentAimDistance;

    public int damage;

    public Transform aimMark;
    public Transform fist;

    internal bool isPunching = false;
    bool isFistExpanding = false;
    bool isLoading = false;
    Vector3 initialLocalFistPosition;
    Vector2 aimMarkPos;

    void Start()
    {
        initialLocalFistPosition = fist.localPosition;
    }

    void Update()
    {
        if (!isPunching && Input.GetMouseButtonDown(0))
        {
            isLoading = true;
        }

        if (!isPunching && Input.GetMouseButtonUp(0))
        {
            aimMarkPos = aimMark.position;
            isPunching = true;
            isFistExpanding = true;
            isLoading = false;
        }

        if (isPunching)
        {
            aimMark.position = aimMarkPos;
        }
        
        if(!isPunching && !isLoading)
        {

            aimMark.localPosition = initialLocalFistPosition;
            aimMark.Translate(Vector3.down * minPunchDistance);
        }
    }

    void FixedUpdate()
    {
        if (isLoading)
        {
            aimMark.Translate(Vector3.down * punchLoadSpeed * Time.fixedDeltaTime);
        }

        if (isPunching)
        {
            if (isFistExpanding)
            {
                fist.position = Vector2.MoveTowards(fist.position, aimMark.position, punchSpeed * Time.fixedDeltaTime);

                if(fist.position == aimMark.position)
                {
                    isFistExpanding = false;
                }
            }
            else
            {
                fist.localPosition = Vector2.MoveTowards(fist.localPosition, initialLocalFistPosition, punchSpeed * Time.fixedDeltaTime);

                if (fist.localPosition == initialLocalFistPosition)
                {
                    isPunching = false;
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if(hitInfo.gameObject.layer == 9)
        {
            isFistExpanding = false;
        }

        Enemy enemy = hitInfo.GetComponent<Enemy>();
        if (enemy != null && isFistExpanding)
        {
            enemy.TakeDamage(damage);
            Instantiate(impactEffect, transform.position, transform.rotation);
            isFistExpanding = false;
        }
    }
}
