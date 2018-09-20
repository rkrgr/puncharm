using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arm : MonoBehaviour {

    public Vector2 positionOffset;
    public float rotationOffset;

    public Transform player;

    public Transform aimMark;

    Punch punch;

    void Awake()
    {
        punch = gameObject.GetComponent<Punch>();
    }

    void Start()
    {
        Physics2D.IgnoreLayerCollision(8, 8);
    }

	void Update ()
    {
        Vector2 armDirection;
        if (punch.isPunching)
        {
            armDirection = aimMark.position - transform.position;
        }
        else
        {
            armDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        }
        float rotZ = Mathf.Atan2(armDirection.y, armDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + rotationOffset);
        transform.position = player.position + new Vector3(positionOffset.x, positionOffset.y);
    }
}
