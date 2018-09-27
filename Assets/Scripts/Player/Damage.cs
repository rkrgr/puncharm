using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour {

    public float invincibilityTime = 2f;

    PlayerHealth health;

    Animator animator;
    bool isInvincible = false;

    void Awake()
    {
        health = GetComponentInParent<PlayerHealth>();
        animator = GetComponentInParent<Animator>();
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if(!isInvincible)
        {
            Enemy enemy = col.GetComponent<Enemy>();
            if(enemy != null)
            {
                health.TakeDamage(enemy.damage);
                isInvincible = true;
                animator.SetLayerWeight(1, 1f);
                Invoke("ResetInvincible", invincibilityTime);
            }
        }
    }

    void ResetInvincible()
    {
        animator.SetLayerWeight(1, 0f);
        isInvincible = false;
    }

}
