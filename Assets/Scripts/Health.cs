﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

    public event Action OnDeath;
    public int maxHealth;

    int health;

    void Start()
    {
        health = maxHealth;
    }

    public bool IsDead()
    {
        return health == 0;
    }

	public void TakeDamage(int damage)
    {
        health -= damage;
        if(health <= 0)
        {
            health = 0;
            if (OnDeath != null) OnDeath();
        }
    }

    public void TakeHeal(int heal)
    {
        health += heal;
        if(health > maxHealth)
        {
            health = maxHealth;
        }
    }

    public void HealFull()
    {
        health = maxHealth;
    }

    public void Die()
    {
        health = 0;
        if(OnDeath != null) OnDeath();
    }

    public int GetHealth()
    {
        return health;
    }
}
