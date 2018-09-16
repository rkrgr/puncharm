using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmController : MonoBehaviour {

    public bool mouseControle = true;

    public Vector2 positionOffset;
    public float rotationOffset;

    public Transform player;
    public GameObject arm;
    public GameObject fist;

    public float punchSpeed;
    public float punchDistance;

    bool isPunching = false;
    float fistDistance;
    Vector3 initialFistPosition;
    bool fistExpanding;
	
	void Update () {

        if(!isPunching)
        {
            Vector2 armDirection = mouseControle    ? Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position 
                                                    : new Vector3(Input.GetAxisRaw("RightJoystickHorizontal"), -Input.GetAxisRaw("RightJoystickVertical"));
            float rotZ = Mathf.Atan2(armDirection.y, armDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rotZ + rotationOffset);

            if (Input.GetButtonDown("Fire1"))
            {
                initialFistPosition = fist.transform.localPosition;
                isPunching = true;
                fistExpanding = true;
            }
        }
        else
        {
            fistDistance = Vector3.Distance(initialFistPosition, fist.transform.localPosition);
            if(fistExpanding && fistDistance >= punchDistance)
            {
                fistExpanding = false;
            }
            else if(!fistExpanding && fistDistance <= 1)
            {
                isPunching = false;
                fist.transform.localPosition = initialFistPosition;
            }
        }
	}

    void FixedUpdate()
    {
        if (isPunching)
        {
            if(fistExpanding)
            {
                fist.transform.Translate(Vector2.down * punchSpeed * Time.fixedDeltaTime);
            }
            else
            {
                fist.transform.Translate(-Vector2.down * punchSpeed * Time.fixedDeltaTime);
            }
        }
    }

    void LateUpdate()
    {
        transform.position = player.position + new Vector3(positionOffset.x, positionOffset.y);
    }
}
