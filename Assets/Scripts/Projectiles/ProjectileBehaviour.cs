using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    public float speedMovement;
    public float limitLife;
    public Vector3 coordinatesRotate;
    public bool isDemoManProjectileBool;

    public bool indamageable { get; set; }
    public bool isInShootable { get; set; }

    [SerializeField] private LayerMask maskGun;
    
    private void OnEnable()
    {
        Invoke("DisableThis", limitLife);
    }
    private void Start()
    {
        isInShootable = true;
    }
    private void FixedUpdate()
    {
        transform.Translate(transform.forward * speedMovement * Time.deltaTime, Space.World);
       
    }

   


    public void DisableThis()
    {

        this.gameObject.SetActive(false);
    }

    public void OnDisable()
    {
        CancelInvoke("DisableThis");

    }


    public void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;

        Gizmos.DrawRay(transform.position, transform.forward * 1f);
    }
}
