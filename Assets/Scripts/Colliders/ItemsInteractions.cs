using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsInteractions : MonoBehaviour
{
    private void Awake()
    {
        GameManager.managerGame.interactionItems = this;

    }
    public void DetonateEffect(ColliderBridge bridgeCollider)
    {


        StartCoroutine(AvoidFreezeScreen(bridgeCollider));
    }

    private IEnumerator AvoidFreezeScreen(ColliderBridge bridgeCollider)
    {
        yield return new WaitForEndOfFrame();
        //WeaponScriptable managerWeaponPlayer = bridgeCollider.gameObject.GetComponent<ProjectileOwner>().gunShooted;

        //GameManager.managerGame.managerSound.SoundClip(GameManager.managerGame.managerSound.clipExplossion, GameManager.managerGame.managerSound.ambienceAudioSource, 1f, .75f, .85f, false, false);

        //if (bridgeCollider.gameObject.GetComponent<ProjectileFX>() != null)
        //{

        //    bridgeCollider.gameObject.GetComponent<ProjectileFX>().ProjectileActive(bridgeCollider.gameObject.transform.position, bridgeCollider.gameObject.transform.rotation);
        //}


        //bridgeCollider.gameObject.SetActive(false);

    }
    public void DamageFall(ColliderBridge bridgeCollider)
    {
        if(bridgeCollider.enterCollider.GetComponent<IDamage>() != null)
        {
            bridgeCollider.enterCollider.gameObject.GetComponent<IDamage>().ReceiveDamage(10000);
        }
    }

    //public void RocketJumpForce_Player(ColliderBridge bridgeCollider)
    //{

    //    if (movDemo != null)
    //    {
    //        RaycastHit hit;

    //        Ray rayRight = new Ray(bridgeCollider.transform.position, bridgeCollider.transform.right);
    //        Ray rayForward = new Ray(bridgeCollider.transform.position, bridgeCollider.transform.forward);
    //        Ray rayUp = new Ray(bridgeCollider.transform.position, bridgeCollider.transform.up);
    //        if (Physics.Raycast(rayRight, out hit))
    //        {
    //            movDemo.CalculateForRocketEffect(hit.point, bridgeCollider.transform.right, bridgeCollider.transform.up);
    //        }
    //        else if (Physics.Raycast(rayForward, out hit))
    //        {
    //            movDemo.CalculateForRocketEffect(hit.point, bridgeCollider.transform.forward, bridgeCollider.transform.up);
    //        }
    //        else if(Physics.Raycast(rayUp, out hit))
    //        {
    //            movDemo.CalculateForRocketEffect(hit.point, bridgeCollider.transform.right, bridgeCollider.transform.up);
    //        }
    //    }
    //}
}
