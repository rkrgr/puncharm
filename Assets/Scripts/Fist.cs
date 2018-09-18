using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fist : MonoBehaviour {

    public GameObject impactEffect;

    public float punchSpeed;
    public float punchDistance;

    public int damage;

    bool isPunching = false;
    float fistDistance;
    Vector3 initialFistPosition;
    bool fistExpanding;

    void Start()
    {
        initialFistPosition = transform.localPosition;
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            isPunching = true;
            fistExpanding = true;
        }

        if (isPunching)
        {
            fistDistance = Vector3.Distance(initialFistPosition, transform.localPosition);
            if (fistExpanding && fistDistance >= punchDistance)
            {
                fistExpanding = false;
            }
            else if (!fistExpanding && fistDistance <= 1)
            {
                isPunching = false;
                transform.localPosition = initialFistPosition;
            }
        }
    }

    void FixedUpdate()
    {
        if (isPunching)
        {
            if (fistExpanding)
            {
                transform.Translate(Vector2.down * punchSpeed * Time.fixedDeltaTime);
            }
            else
            {
                transform.Translate(-Vector2.down * punchSpeed * Time.fixedDeltaTime);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if(hitInfo.gameObject.layer == 9)
        {
            fistExpanding = false;
        }

        Enemy enemy = hitInfo.GetComponent<Enemy>();
        if (enemy != null && fistExpanding)
        {
            enemy.TakeDamage(damage);
            Instantiate(impactEffect, transform.position, transform.rotation);
            fistExpanding = false;
        }
    }
}
