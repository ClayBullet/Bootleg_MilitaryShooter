using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPlayer : MonoBehaviour
{
    [SerializeField] private AmmoType[] typeAmmo;
    private AmmoType _currentAmmoType;
    private Dictionary<WeaponScriptable, MagazineGun> weaponMagazineDictionary = new Dictionary<WeaponScriptable, MagazineGun>();
    public AmmoType currentAmmoType
    {
        get { return _currentAmmoType; }
    }
    [SerializeField] private MagazineGun currentMagazineGun;
    private void Awake()
    {
        GameManager.managerGame.playerInventory = this;
    }



    private void AddAmmo(AmmoType currentAmmoType, int amount)
    {
        currentAmmoType.currentAmmo += amount;
    }
    private int GetAmmo(AmmoType currentAmmoType)
    {
        return currentAmmoType.currentAmmo;
    }

    public void AddAmmo(WeaponScriptable gun, int currentAmmo){ AddAmmo(identifyCorrectGun(gun), currentAmmo); }
    public void GetAmmo(WeaponScriptable gun, ref int takeAmmoData) { takeAmmoData = GetAmmo(identifyCorrectGun(gun)); }
    public bool getTheCurrentAmmoBool(WeaponScriptable gun)
    {
        if (identifyCorrectGun(gun).currentAmmo > 0)
            return true;

        return false;
    }


    public void PrepareTheCurrentMagazine(WeaponScriptable currentGun)
    {
        if (!weaponMagazineDictionary.ContainsKey(currentGun))
        {
            MagazineGun gunMagazine = new MagazineGun();
            gunMagazine.limitMagazine = currentGun.limitMagazine;
            gunMagazine.currentMagazineUsed = _addCurrentAmmo(gunMagazine.currentMagazineUsed, currentGun.limitMagazine, currentGun);
            weaponMagazineDictionary.Add(currentGun, gunMagazine);
        }
         currentMagazineGun = weaponMagazineDictionary[currentGun];

    }

    public bool checkCurrentAmmo(WeaponScriptable currentGun)
    {
        if(weaponMagazineDictionary[currentGun].currentMagazineUsed <= 0)
        {

        }
        else
            weaponMagazineDictionary[currentGun].currentMagazineUsed -= 1;


        return weaponMagazineDictionary[currentGun].currentMagazineUsed > 0;
    }

    public void ReloadTheGun(WeaponScriptable gun)
    {
        weaponMagazineDictionary[gun].currentMagazineUsed = _addCurrentAmmo(weaponMagazineDictionary[gun].currentMagazineUsed, gun.limitMagazine, gun);
    }
    private int _addCurrentAmmo(int currentMagazine, int limitMagazine, WeaponScriptable gun)
    {
        int ammo = currentMagazine;
        for (int i = currentMagazine; i < limitMagazine; i++)
        {
            if (getTheCurrentAmmoBool(gun))
            {
                ammo += 1;
                AddAmmo(gun, -1);
            }
            else
            {
                break;
            }       
        }

        return ammo;
    }

    private void AddThrowable(AmmoType currentAmmoType, int amount){ currentAmmoType.currentAmmo += amount;}
    

    public void AddThrowable(ThrowScriptable throwable, int currentAmmo) { AddThrowable(identifyCorrectThrowable(throwable), -1); }

    public AmmoType identifyCorrectGun(WeaponScriptable gun)
    {
        foreach (AmmoType ammo in typeAmmo)
        {
            foreach (WeaponScriptable gunAssigned in ammo.ammoType.gunScriptableList)
            {
                if (gunAssigned == gun)
                    return ammo;
            }
        }
        return null;
    }

    public AmmoType identifyCorrectThrowable(ThrowScriptable throwable)
    {
        foreach(AmmoType ammo in typeAmmo)
        {
            foreach (ThrowScriptable gunAssigned in ammo.ammoType.throwScriptableList)
            {
                if (throwable == gunAssigned)
                    return ammo;
            }
        }
        return null;
    }

}

[System.Serializable]
public class AmmoType
{
    public string nameAmmo;
    public int currentAmmo;
    public AmmoScriptable ammoType;
    public AmmoType(string _nameAmmo, int _currentAmmo, AmmoScriptable _ammoType)
    {
        _nameAmmo = nameAmmo;
        _currentAmmo = currentAmmo;
        _ammoType = ammoType;
    }
}

[System.Serializable]
public class MagazineGun
{
    public int currentMagazineUsed;
    public int limitMagazine;
    public bool eachReloadStyleBool;
    
    public AmmoType typeAmmo;
    public MagazineGun() { }
    public MagazineGun(int _limitMagazine, bool _eachReloadStyleBool, AmmoType _typeAmmo)
    {
        _limitMagazine = limitMagazine;
        _eachReloadStyleBool = eachReloadStyleBool;
        _typeAmmo = typeAmmo;
    }
}

public enum AmmoKind
{
    pistolAmmo,
    shotgunAmmo,
    rifleAmmo,
    submachineAmmo,
    specialAmmo
}