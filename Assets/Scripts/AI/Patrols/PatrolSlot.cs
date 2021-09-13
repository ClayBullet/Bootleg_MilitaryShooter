using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class PatrolSlot : MonoBehaviour
{
    [SerializeField] private float radiusSphereGizmosSlot;

    [SerializeField] private float timeForStayHereFloatPrivate;

    public float timeForStayHereFloatPublic
    {
        get
        {
            return timeForStayHereFloatPrivate;
        }
    }


    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, radiusSphereGizmosSlot);
    }
}
