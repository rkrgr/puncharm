using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arm : MonoBehaviour {

    public bool mouseControle = true;

    public Vector2 positionOffset;
    public float rotationOffset;

    public Transform player;

    public string[] collideWith;
    [Range(0, 10)]
    public float collideDistanceOffset;

    Fist fist;

    int collideLayerMask;

    void Awake()
    {
        fist = gameObject.GetComponentInChildren<Fist>();
    }

    void Start()
    {
        collideLayerMask = LayerMask.GetMask(collideWith);
    }

	void Update ()
    {
        Vector2 armDirection = mouseControle ? MoveArmMouse() : MoveArmJoystick();

        float dis = fist.fistDistance - collideDistanceOffset;
        Debug.Log(dis);
        if (dis < 0) dis = 0;
        RaycastHit2D hit = CheckRaycast(armDirection, dis);

        bool canMove = !fist.isPunching || !hit.collider;

        if(canMove)
        {
            Quaternion quatOld = transform.rotation;
            float rotZ = Mathf.Atan2(armDirection.y, armDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rotZ + rotationOffset);
        }
    }

    void LateUpdate()
    {
        transform.position = player.position + new Vector3(positionOffset.x, positionOffset.y);
    }

    private Vector3 MoveArmJoystick()
    {
        return new Vector3(Input.GetAxisRaw("RightJoystickHorizontal"), -Input.GetAxisRaw("RightJoystickVertical"));
    }

    private Vector3 MoveArmMouse()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
    }

    private RaycastHit2D CheckRaycast(Vector2 direction, float currentDistance)
    {
        return Physics2D.Raycast(transform.position, direction, currentDistance, collideLayerMask);
    }
}
