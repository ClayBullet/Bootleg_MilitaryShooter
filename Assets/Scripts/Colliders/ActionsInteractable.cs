using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionsInteractable : MonoBehaviour
{
    public void Entry_TutoZone(ColliderBridge bridgeCollider)
    {
       //REHACER EL TUTORIAL AQUI - RECORDATORIO
    }

    public void ReceiveDamageForCollision(ColliderBridge bridgeCollider)
    {
        if (bridgeCollider.enterCol != null && bridgeCollider.enterCol.gameObject.GetComponent<IDamage>() != null)
        {
            bridgeCollider.enterCol.gameObject.GetComponent<IDamage>().ReceiveDamage(1000000f);
        }
    }

    public void ReceiveDamage_Player(ColliderBridge bridgeCollider)
    {
        //if (bridgeCollider.enterCollision.gameObject.GetComponent<IDamage>() != null)
        //{
        //    bridgeCollider.enterCollision.gameObject.GetComponent<IDamage>().ReceiveDamage(10000f);

        //}
    }
    
    public void ShowNewTutorial_Collider(ColliderBridge bridgeCollider)
    {
      
    }
}
