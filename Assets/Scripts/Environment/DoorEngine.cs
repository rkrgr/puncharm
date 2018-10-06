using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class DoorEngine : SwitchableObject {

    private Collider2D doorCollider;
    private Renderer doorRenderer;

    private void Awake()
    {
        doorCollider = GetComponent<Collider2D>();
        doorRenderer = GetComponent<Renderer>();
    }

    public override void Switch(bool active, GameObject sender)
    {
        if(active)
        {
            DeactivateCollider();
            SwitchSpriteToDeactivated();
        }
        else
        {
            ActivateCollider();
        }
    }

    private void SwitchSpriteToDeactivated()
    {
        doorRenderer.enabled = false;
    }

    private void ActivateCollider()
    {
        if(doorCollider)
        {
            doorCollider.enabled = true;
        }
    }

    private void DeactivateCollider()
    {
        if (doorCollider)
        {
            doorCollider.enabled = false;
        }
    }
}
