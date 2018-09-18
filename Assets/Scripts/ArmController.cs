using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmController : MonoBehaviour {

    public bool mouseControle = true;

    public Vector2 positionOffset;
    public float rotationOffset;

    public Transform player;
	
	void Update () {

        Vector2 armDirection = mouseControle ? Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position
                                                : new Vector3(Input.GetAxisRaw("RightJoystickHorizontal"), -Input.GetAxisRaw("RightJoystickVertical"));
        float rotZ = Mathf.Atan2(armDirection.y, armDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + rotationOffset);

    }

    void LateUpdate()
    {
        transform.position = player.position + new Vector3(positionOffset.x, positionOffset.y);
    }
}
