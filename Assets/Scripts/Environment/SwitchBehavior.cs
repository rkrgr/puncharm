using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SwitchBehavior : MonoBehaviour {

    public Sprite deactivatedSprite;
    public Sprite activatedSprite;
    public SwitchableObject target;
    public bool IsActivated { get; private set; }

    private SpriteRenderer spriteRenderer;

    #region ForEditor
#if UNITY_EDITOR
    private SwitchableObject oldtarget;
#endif
    #endregion

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if(!IsActivated)
            {
                IsActivated = true;
                spriteRenderer.sprite = activatedSprite;
                target.Switch(true, gameObject);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        if (target != null)
        {
            // Draws a blue line from this transform to the target
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, target.transform.position);
        }
    }

    private void OnValidate()
    {
        if (target)
        {
            if (oldtarget) oldtarget.SwitchedBy.Remove(transform);
            target.SwitchedBy.Add(transform);
            oldtarget = target;
        }
        else
        {
            if (oldtarget) oldtarget.SwitchedBy.Remove(transform);
            oldtarget = null;
        }
    }


}
