using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {

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
        if(health < 0)
        {
            health = 0;
        }
        Debug.Log(health);
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
    }

    public int GetHealth()
    {
        return health;
    }
}
