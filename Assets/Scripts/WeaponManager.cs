using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class WeaponManager : MonoBehaviour
{
    [SerializeField] protected List<WeaponScriptable> scriptableWeapons = new List<WeaponScriptable>();
    #region AnimationsRegion
    public Animator animatorCurrentWeapon;
    public string shootAnimation = "ShootBoolean";
    public string reloadAnimation = "ReloadBoolean";
    #endregion

    #region FXGuns
    public GameObject fxBulletImpact;
    public GameObject fxBulletBlood;
    #endregion



    public Dictionary<int, WeaponScriptable> weaponDictionary
    {
        get
        {
            Dictionary<int, WeaponScriptable> _dictionary = new Dictionary<int, WeaponScriptable>();
            for (int i = 0; i < scriptableWeapons.Count; i++)
            {
                _dictionary.Add(scriptableWeapons[i].GetInstanceID(), scriptableWeapons[i]);
            }

            return _dictionary;
        }
    }

    public List<WeaponSlot> allTheWeapons = new List<WeaponSlot>();


    [SerializeField] private LayerMask maskGunLayer;
    [SerializeField] private LayerMask maskInvisibleLayer;
    private void Awake()
    {
        GameManager.managerGame.managerWeapon = this;
    }

    

 
    #region DamageWeaponSystem

    public void WeaponDamage(WeaponScriptable currentWeapon, IDamage Idamage)
    {
        Idamage.ReceiveDamage(currentWeapon.damageBody);
    }
    #endregion

    #region ReloadSystem

    public void ChargeTheCurrentWeapon(int currentMagazine, WeaponScriptable currentWeapon, ref int currentMagazineCapacity)
    {
        for (int i = currentMagazine; i < currentWeapon.limitMagazine; i++)
        {
            currentMagazineCapacity--;

            currentMagazine++;
        }
    }
    #endregion

    #region WeaponChange
    public void WeaponChangeAbleBetweenPrimaryAndSecondaryWeapon()
    {

    }
    #endregion

    #region FXSupport

    public void fxInstantiation(GameObject fx, Vector3 position, Quaternion rotation, WeaponScriptable gun = null)
    {

        if (GameManager.managerGame.managerPool.dictionaryPool.ContainsKey(fx))
        {
            GameObject go = GameManager.managerGame.managerPool.objectsForPool.UseTheGameObject(GameManager.managerGame.managerPool.dictionaryPool[fx], position, rotation);
            if(go != null && go.GetComponent<ProjectileSound>() != null)
            go.GetComponent<ProjectileSound>().IfInvokeForWeapon(gun);
            if(go != null)
            {
                go.SetActive(true);
                StartCoroutine(GameManager.managerGame.managerPool.objectsForPool.DelayForDisable(go, 1f));
            }
           
        }
       
    }

    public void PutInTheCorrectLayerTheGun(WeaponSlot slotWeapon)
    {

        foreach (WeaponSlot slot in allTheWeapons)
        {
            if (slot != null && slot != slotWeapon)
            {
                slot.gameObject.SetActive(false);
            }
        }

        slotWeapon.gameObject.SetActive(true);
    }


    #endregion


    #region HitMarker

    [SerializeField] private GameObject hitMarkerObj;
    [SerializeField] private float maxDelayHitMarker;
    public bool isAccessToHitMarkerBool = true;
    private float _currentDelayHitMarker;
    public void ActivateHitMarker()
    {
        _currentDelayHitMarker += Time.deltaTime;

        if (_currentDelayHitMarker > maxDelayHitMarker)
        {
            hitMarkerObj.SetActive(false);
            _currentDelayHitMarker = 0f;
            isAccessToHitMarkerBool = true;

            currentActionForMake -= ActivateHitMarker;

        }
        else
        {
            isAccessToHitMarkerBool = false;
            if (!hitMarkerObj.activeSelf)
            {
                hitMarkerObj.SetActive(true);
            }
        }

    }


    #endregion
    public Action currentActionForMake;
    private void LateUpdate()
    {
        if(currentActionForMake != null)
        {
            currentActionForMake.Invoke();
        }
    }
}
