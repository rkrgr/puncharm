using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class MeleeAttack : MonoBehaviour {

    public int damage;
    public float attackCooldown;

    float currentCooldown = 0f;

    void Update()
    {
        if(currentCooldown > 0f)
        {
            if(currentCooldown < Time.deltaTime)
            {
                currentCooldown = 0f;
            }
            else
            {
                currentCooldown -= Time.deltaTime;
            }
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        PlayerDamage playerDamage = col.GetComponent<PlayerDamage>();
        if(playerDamage != null && currentCooldown == 0f)
        {
            playerDamage.Hit(damage);
        }
    }

}
