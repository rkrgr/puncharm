using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour {

    public float invincibilityTime = 2f;

    Health health;

    Animator animator;
    bool isInvincible = false;

    void Awake()
    {
        health = GetComponentInParent<Health>();
        animator = GetComponentInParent<Animator>();
    }

    void OnTriggerStay2D(Collider2D col)
    {
        Enemy enemy = col.GetComponent<Enemy>();
        if(enemy != null)
        {
            Hit(enemy.damage);
        }
    }

    public void Hit(int damage)
    {
        if (!isInvincible)
        {
            health.TakeDamage(damage);
            isInvincible = true;
            animator.SetLayerWeight(1, 1f);
            Invoke("ResetInvincible", invincibilityTime);
        }
    }

    void ResetInvincible()
    {
        animator.SetLayerWeight(1, 0f);
        isInvincible = false;
    }

}
