using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPlayWeaponPlayer : MonoBehaviour
{
   
    public void MainCameraMovement(Transform objectForShake, float force, float time)
    {
        GameManager.managerGame.managerDoTween.ScreenShake(force, time, objectForShake);

    }



}
