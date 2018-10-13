using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeRobot : Enemy {

    public Rigidbody2D grenade;
    public float grenadeSpeed = 1f;

    public float attackCooldown = 1f;
    float currentAttackCooldown = 0f;

    public Transform player;

    Vector2 faceDirection = Vector2.right;

    void Update () {
        if(player.position.x < transform.position.x && faceDirection == Vector2.right)
        {
            FlipCharacter();
        }
        else if(player.position.x > transform.position.x && faceDirection == Vector2.left)
        {
            FlipCharacter();
        }

        if(currentAttackCooldown <= 0f)
        {
            currentAttackCooldown = attackCooldown;
            Rigidbody2D grenadeClone = (Rigidbody2D)Instantiate(grenade, transform.position, transform.rotation);
            grenadeClone.velocity = (faceDirection + Vector2.up * 5).normalized * grenadeSpeed;
        }
        
        if(currentAttackCooldown > 0f)
        {
            currentAttackCooldown -= Time.deltaTime;
        }
    }

    void FlipCharacter()
    {
        Vector3 scale = transform.localScale;
        scale.x = -scale.x;
        transform.localScale = scale;

        if (faceDirection == Vector2.right)
        {
            faceDirection = Vector2.left;
        }
        else
        {
            faceDirection = Vector2.right;
        }
    }
}
