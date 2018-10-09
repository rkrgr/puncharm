using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeRobot : Enemy {

    public Rigidbody2D grenade;
    public float grenadeSpeed = 1f;

    public float attackCooldown = 1f;
    float currentAttackCooldown = 0f;
	
    void Update () {
        if(currentAttackCooldown <= 0f)
        {
            currentAttackCooldown = attackCooldown;
            Rigidbody2D grenadeClone = (Rigidbody2D)Instantiate(grenade, transform.position, transform.rotation);
            grenadeClone.velocity = (Vector2.left + Vector2.up * 5).normalized * grenadeSpeed;
        }
        
        if(currentAttackCooldown > 0f)
        {
            currentAttackCooldown -= Time.deltaTime;
        }
    }
}
