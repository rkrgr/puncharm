using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fist : MonoBehaviour {

    public int damage = 50;

    GameObject player;
    FistAttack fistAttack;

    void Awake()
    {
        player = GameObject.FindWithTag("Player");
        fistAttack = player.GetComponent<FistAttack>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Enemy enemy = col.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
            fistAttack.Collide();
        }

        CompositeCollider2D tileCollider = col.GetComponent<CompositeCollider2D>();
        if(tileCollider != null)
        {
            fistAttack.Collide();
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        CompositeCollider2D tileCollider = col.GetComponent<CompositeCollider2D>();
        if (tileCollider != null)
        {
            fistAttack.Collide();
        }
    }

}
