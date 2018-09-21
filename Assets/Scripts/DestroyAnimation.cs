using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAnimation : MonoBehaviour {

    public float destroyAfter = 1f;

    void Start () {
        Invoke("DestoryGameObject", destroyAfter);
    }

	void DestoryGameObject()
    {
        Destroy(gameObject);
    }
}
