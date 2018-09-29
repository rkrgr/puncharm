using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundCheck : MonoBehaviour {

    bool isGrounded;

    public bool IsGrounded()
    {
        return isGrounded;
    }

    void OnTriggerEnter2D()
    {
        isGrounded = true;
    }

    void OnTriggerExit2D()
    {
        isGrounded = false;
    }

}
