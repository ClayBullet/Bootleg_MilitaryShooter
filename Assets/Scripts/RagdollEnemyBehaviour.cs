using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollEnemyBehaviour : MonoBehaviour
{
    [SerializeField] private float speedForce;
    [SerializeField] private Collider[] allColliders;
    [SerializeField] private Rigidbody[] allRigidbodies;

    

   

  
    private void Start()
    {
        setRigidbodyState(true);
        setColliderState(false);
    }

    public void DeathStateRagdoll(Transform oppositeForce)
    {
        setRigidbodyState(false, oppositeForce);
        setColliderState(true);
    }
    public void setRigidbodyState(bool stateBool, Transform oppositeForce = null)
    {
        Rigidbody[] rigidbodies = GetComponentsInChildren<Rigidbody>();

        foreach (Rigidbody rgb in rigidbodies)
        {
            rgb.isKinematic = stateBool;
            if(oppositeForce != null)
            rgb.AddForce(-oppositeForce.position * speedForce);
        }

        GetComponent<Rigidbody>().isKinematic = !stateBool;

    }

    public void setColliderState(bool stateBool)
    {
        Collider[] colliders = GetComponentsInChildren<Collider>();

       
        GetComponent<Collider>().enabled = !stateBool;

    }

  
    
  
   
}
