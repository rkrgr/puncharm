using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowGameObject : MonoBehaviour
{


    public Transform follow;

    // Update is called once per frame
    void Update()
    {

        gameObject.transform.position = new Vector3(follow.position.x,follow.position.y,transform.position.z);
    }
}
