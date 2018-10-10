using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Grenade : MonoBehaviour {

    public int damage;

    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerDamage playerDamage = other.GetComponent<PlayerDamage>();
        if(playerDamage != null)
        {
            playerDamage.Hit(damage);
        }

        Destroy(gameObject);
    }

}
