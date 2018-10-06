using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Helper.GameObjectExt;

public class PlayerGroundCheck : MonoBehaviour {

    public bool IsGrounded { get; private set; }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.IsInLayer("Obstacle"))
        {
            IsGrounded = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.IsInLayer("Obstacle"))
        {
            IsGrounded = false;
        }
    }
}
