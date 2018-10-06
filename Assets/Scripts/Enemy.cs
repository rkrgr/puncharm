using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
public abstract class Enemy : MonoBehaviour {

    public int damage;

    protected Health health;

    protected virtual void Awake()
    {
        health = GetComponent<Health>();
    }

    public virtual void TakeDamage(int receiveDamage)
    {
        health.TakeDamage(receiveDamage);
        if (health.IsDead())
        {
            Destroy(gameObject);
        }
    }

    protected void Die()
    {
        Destroy(gameObject);
    }

}
