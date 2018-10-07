using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class DoorEngine : SwitchableObject {

    public Sprite openedSprite;
    public Sprite closedSprite;

    private Collider2D doorCollider;
    private SpriteRenderer doorRenderer;

    private void Awake()
    {
        doorCollider = GetComponent<Collider2D>();
        doorRenderer = GetComponent<SpriteRenderer>();
    }

    public override void Switch(bool active, GameObject sender)
    {
        if(active)
        {
            DeactivateCollider();
            SwitchSpriteToOpened();
        }
        else
        {
            ActivateCollider();
            SwitchSpriteToClosed();
        }
    }

    private void SwitchSpriteToClosed()
    {
        doorRenderer.sprite = closedSprite;
    }

    private void SwitchSpriteToOpened()
    {
        doorRenderer.sprite = openedSprite;
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
