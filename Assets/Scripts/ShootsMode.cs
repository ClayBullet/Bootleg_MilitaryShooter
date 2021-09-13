using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootsMode : MonoBehaviour
{

    private void Awake()
    {
        GameManager.managerGame.modeShoots = this;
    }
    public void RayCastFuncionality(Ray ray, WeaponScriptable currentWeapon, LayerMask maskLayer)
    {

        RaycastHit hit;

       

        if (Physics.Raycast(ray, out hit, currentWeapon.distanceToShoot, maskLayer))
        {
            if (hit.collider.GetComponent<IDamage>() != null && hit.collider.GetComponent<IDamage>().isInShootable)
            {
              //  GameManager.managerGame.bloodSplash.BloodRay(hit.point, ray.direction);
                GameManager.managerGame.managerWeapon.WeaponDamage(currentWeapon, hit.collider.GetComponent<IDamage>());

               
            }
            else if (hit.collider.CompareTag("Player"))
            {

            }
            else
            {
                GameManager.managerGame.managerWeapon.fxInstantiation(GameManager.managerGame.managerWeapon.fxBulletImpact,
                                                                      hit.point,
                  
                                                                      Quaternion.LookRotation(transform.position), currentWeapon);
            }
        }

      
    }

    public void RaycastWithRocketJumpFuncionality_ExclusivelyDemoPlayer(Ray ray, WeaponScriptable currentWeapon, LayerMask maskLayer)
    {
        
    }

    public void InstantiateWeaponModel(Transform transformPos,Ray ray, WeaponScriptable gun)
    {

        GameObject go = GameManager.managerGame.managerPool.objectsForPool.UseTheGameObject(GameManager.managerGame.managerPool.dictionaryPool[gun.projectileGun], transformPos.position, Quaternion.LookRotation(ray.direction));
        go.GetComponent<ProjectileBehaviour>().speedMovement = gun.speedProjectile;
        go.GetComponent<ProjectileBehaviour>().limitLife = gun.limitLife;

        go.SetActive(true);

     
    }


    public void HoldWeaponForShoot_InstantiateState(Transform transformPos, Ray ray, WeaponScriptable gun, float maxTime, ref float currentTime, ref bool holdTheButtonBool, ref bool canConfirmTheShootBool)
    {


        currentTime += Time.deltaTime;
        holdTheButtonBool = true;
        canConfirmTheShootBool = false;
        if(maxTime < currentTime)
        {

            GameObject go = GameManager.managerGame.managerPool.objectsForPool.UseTheGameObject(GameManager.managerGame.managerPool.dictionaryPool[gun.projectileGun], transformPos.position, Quaternion.LookRotation(ray.direction));
            go.GetComponent<ProjectileBehaviour>().speedMovement = gun.speedProjectile;
            go.GetComponent<ProjectileBehaviour>().limitLife = gun.limitLife;

            go.SetActive(true);
            holdTheButtonBool = false;
            canConfirmTheShootBool = true;

            currentTime = 0f;
        }

      

    }
}
