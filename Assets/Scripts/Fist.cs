using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fist : MonoBehaviour {

    GameObject player;
    FistAttack fistAttack;

    void Awake()
    {
        player = GameObject.FindWithTag("Player");
        fistAttack = player.GetComponent<FistAttack>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        CompositeCollider2D tileCollider = col.GetComponent<CompositeCollider2D>();
        if(tileCollider != null)
        {
            fistAttack.CollideWithWall();
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        CompositeCollider2D tileCollider = col.GetComponent<CompositeCollider2D>();
        if (tileCollider != null)
        {
            fistAttack.CollideWithWall();
        }
    }

}
