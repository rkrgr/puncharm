using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class RespawnPoint : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            SceneState.Instance.lastRespawnPoint = this;
            Debug.Log("New RespwanPoint set", gameObject);
        }
    }



}
