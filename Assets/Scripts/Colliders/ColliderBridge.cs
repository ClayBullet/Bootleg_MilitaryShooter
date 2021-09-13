using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
public class ColliderBridge : MonoBehaviour
{
    public List<string> enterTag = new List<string>();
    public List<string> stayTag = new List<string>();
    public List<string> exitTag = new List<string>();

   [HideInInspector] public Collider enterCollider;
   [HideInInspector] public Collider stayCollider;
   [HideInInspector] public Collider exitCollider;

    [HideInInspector] public Collision enterCol;
    [HideInInspector] public Collision stayCol;
    [HideInInspector] public Collision exitCol;
    public UnityEvent enterEvent;
    public UnityEvent stayEvent;
    public UnityEvent exitEvent;

    private void OnTriggerEnter(Collider other)
    {
        if (enterTag.Contains(other.tag))
        {
            string currentTag = other.tag;
            if (other.CompareTag(currentTag))
            {
                enterCollider = other;
                enterEvent.Invoke();
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (stayTag.Contains(other.tag))
        {
            string currentTag = other.tag;

            if (other.CompareTag(currentTag))
            {
                stayCollider = other;
                stayEvent.Invoke();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (exitTag.Contains(other.tag))
        {
            string currentTag = other.tag;

            if (other.CompareTag(currentTag))
            {
                exitCollider = other;
                exitEvent.Invoke();
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (enterTag.Contains(collision.gameObject.tag))
        {
            string currentTag = collision.gameObject.tag;

            if (collision.gameObject.CompareTag(currentTag))
            {
                enterCol = collision;
                enterEvent.Invoke();
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (stayTag.Contains(collision.gameObject.tag))
        {
            string currentTag = collision.gameObject.tag;

            if (collision.gameObject.CompareTag(currentTag))
            {
                stayCol = collision;
                stayEvent.Invoke();
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (exitTag.Contains(collision.gameObject.tag))
        {
            string currentTag = collision.gameObject.tag;

            if (collision.gameObject.CompareTag(currentTag))
            {
                exitCol = collision;
                exitEvent.Invoke();
            }
        }
    }

}
[System.Serializable]
public class BridgeCollision
{
    public string actionName;
    public string tagAction;
    public ColliderType typeCollider;
    
    public BridgeCollision(string _actionName, string _tagAction,  ColliderType _typeCollider)
    {
        _actionName = actionName;
        _tagAction = tagAction;
        _typeCollider = typeCollider;
       
    }
}
public enum ColliderType
{
    Enter,
    Exit,
    Stay
}