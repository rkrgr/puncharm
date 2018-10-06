using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SwitchableObject : MonoBehaviour
{
    public HashSet<Transform> SwitchedBy { get; private set; }

    public SwitchableObject()
    {
        SwitchedBy = new HashSet<Transform>();
    }

    public abstract void Switch(bool active, GameObject sender);

    private void OnDrawGizmosSelected()
    {
        foreach(Transform target in SwitchedBy)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, target.transform.position);
        }
    }
}
