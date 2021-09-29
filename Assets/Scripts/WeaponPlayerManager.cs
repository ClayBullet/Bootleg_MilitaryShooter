using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class WeaponPlayerManager : MonoBehaviour
{
    [SerializeField] private WeaponScriptable currentWeaponMainly;
    public WeaponScriptable currentWeaponPublic
    {
        get
        {
            return currentWeaponMainly;
        }
    }
    private WeaponSlot _currentSlotWeapon;
    [SerializeField] private WeaponScriptable primaryWeapon;
    public WeaponScriptable primaryWeaponPublic
    {
        get
        {
            return primaryWeapon;
        }
    }

    [SerializeField] private WeaponScriptable secondaryWeapon;
    public WeaponScriptable secondaryWeaponForChooseInPublic
    {
        get
        {
            return secondaryWeapon;
        }
    }
    private GameObject _currentWeaponModel;


    [Header("CROSSHAIR SYSTEM")]
    [Space]
    [SerializeField] private Transform crosshairRect;

    public Transform currentCrosshairTransform
    {
        get
        {
            return crosshairRect;
        }
    }
    private bool _reloadButtonBool;
    private bool _cadenceBool;

    public Transform weaponGrabbleTransform;
    public Vector3 offsetGrabble;
    [Header("WEAPON SYSTEM FOR CHOICE")]
    [Space]
    [SerializeField] private float delayForPickUpWeapon;
    [SerializeField] private float maxTimeDelay;
    [SerializeField] private Image weaponImageSystem;
    private GameObject _currentPickableGunObject;
    private WeaponScriptable _pickableWeapon;
    private float _currentTime;
    [SerializeField] private bool iAmTtryToPickAWeaponBool;


    [Header("CURRENT MAGAZINE")]
    [Space]
    [SerializeField] private int currentMagazine;

    [Header("THROWABLE CHARACTER")]
    [Space]
    [SerializeField] private ThrowScriptable throwableWeapon;
    [SerializeField] private ThrowScriptable alternativeThrowableWeapon;
    [SerializeField] private Transform respawnThrowableObject;

    [Header("IS WEAPON PICKABLE")]
    [Space]
    public bool isWeaponPickableBool;

    [Header("CHOOSE FOR INSTANTIATION SYSTEM")]
    [Space]
    public Transform transformLocationForInstantion;

    public Camera playerCamera;

    [SerializeField] private Image imageCurrentWeapon;

    
    private GunPlayWeaponPlayer _playerWeaponGun
    {
        get
        {
            return GetComponent<GunPlayWeaponPlayer>();
        }
    }

    private WeaponInterface _interfaceWeapon
    {
        get
        {
            return GetComponent<WeaponInterface>();
        }
    }

    public LayerMask gunLayer;

    private bool _dontShootGeneralBool;
    public Action<Ray, LayerMask> concretActionWithTheRay;

    public Text textGun;
    private readonly string changeGun = "TAKE NEW GUN ";


    private void Start()
    {
        GameManager.managerGame.managerPlayerWeapon = this;

        _cadenceBool = true;
        _reloadButtonBool = true;
        WeaponGrabble(primaryWeapon);
        StartCoroutine(ReloadCoroutine());
        AssignKeys();
        InterfaceGun();

    }

    private void AssignKeys()
    {
        GameManager.managerGame.managerInput.DownPress_FireButton += Shoot_Action;
        GameManager.managerGame.managerInput.UpPress_FireButton += Shoot_Action_Stop;
        GameManager.managerGame.managerInput.DownPress_ThrowButton += Throw_Action;
        GameManager.managerGame.managerInput.DownPress_ReloadButton += Reload_Action;
        GameManager.managerGame.managerInput.DownPress_SwitchButton += ChangeGun;
        GameManager.managerGame.managerInput.DownPress_AlternativeThrowButton += AlternativeThrowable_Action;
    }

    public void TakeRewindForThis(GameObject slotObj)
    {

    }
    [ContextMenu("CHANGE GUN KIND")]
    public void ChangeGun()
    {
        if (primaryWeapon == currentWeaponMainly)
            WeaponGrabble(secondaryWeapon, true);
        else
            WeaponGrabble(primaryWeapon, true);
    }

    public void LeftGunHere(Transform currentTransform, GameObject lastGun)
    {
        Destroy(lastGun);
        GameObject go = Instantiate(currentWeaponMainly.pickableWeapon, currentTransform.position, currentTransform.rotation);
        go.GetComponent<WeaponPickUp>().isPickableGunBool = true;
    }
    public void WeaponGrabble(WeaponScriptable gun, bool changeWeaponBool = false)
    {
        if (_currentWeaponModel != null)
        {
            _currentWeaponModel.SetActive(false);
            if (_currentPickableGunObject != null)
            {
                GameObject pickObject = Instantiate(currentWeaponMainly.pickableWeapon, _currentPickableGunObject.transform.position, Quaternion.identity);
                pickObject.GetComponent<WeaponPickUp>().isPickableGunBool = false;
                pickObject.SetActive(true);
            }
            _currentWeaponModel = null;
        }
        GameObject go = Instantiate(gun.objectWeapon, weaponGrabbleTransform.position + gun.weaponOffset, weaponGrabbleTransform.rotation);

        _currentSlotWeapon = go.GetComponent<WeaponSlot>();

        GameManager.managerGame.managerWeapon.allTheWeapons.Add(_currentSlotWeapon);
       go.transform.SetParent(weaponGrabbleTransform);
        go.transform.position += offsetGrabble;
        if(go.GetComponent<Animator>() != null)
        GameManager.managerGame.managerWeapon.animatorCurrentWeapon = go.GetComponent<Animator>();

        if (changeWeaponBool)
        {
            if (primaryWeapon == currentWeaponMainly)
            {
                secondaryWeapon = primaryWeapon;
                primaryWeapon = gun;
            }
            else if (secondaryWeapon == currentWeaponMainly)
            {
                primaryWeapon = secondaryWeapon;
                secondaryWeapon = gun;

            }
        }

        _currentWeaponModel = go;
        currentWeaponMainly = gun;

        if (!changeWeaponBool)
            primaryWeapon = currentWeaponMainly;



        if (_currentSlotWeapon.imageGun != null)
        {
            imageCurrentWeapon = _currentSlotWeapon.imageGun;
            AccessAndDisableAllTheGuns();
            _currentSlotWeapon.EnableSlider();
        }

        GameManager.managerGame.playerInventory.PrepareTheCurrentMagazine(gun);

    }

    public void Shoot_Action()
    {
        if (!_dontShootGeneralBool) return;

        if(_cadenceBool)
        {
            AvaibleForShoot();
        }
    }

    public void Shoot_Action_Stop()
    {
        if (!_dontShootGeneralBool) return;

      
    }

    public void Throw_Action()
    {
        if (!_dontShootGeneralBool) return;

        ThrowableSystem();

    }

    public void AlternativeThrowable_Action()
    {
        if (!_dontShootGeneralBool) return;

        AlternativeThrowableSystem();
    }

    public void Reload_Action()
    {
        if (!_dontShootGeneralBool) return;

        GameManager.managerGame.playerInventory.ReloadTheGun(currentWeaponMainly);
        if (_reloadButtonBool)
            StartCoroutine(ReloadCoroutine());
    }
    private void LateUpdate()
    {
        _dontShootGeneralBool =  !GameManager.managerGame.managerTime.isTimeFreezingBool;
    }


    private void InterfaceGun()
    {
       // _interfaceWeapon.MagazineRepresentation(currentWeaponMainly);
    }
    #region NormalWeapons

    public void AvaibleForShoot()
    {

        if (!GameManager.managerGame.playerInventory.checkCurrentAmmo(currentWeaponMainly)) return;
        if(_playerWeaponGun != null && currentWeaponMainly != null && playerCamera.transform != null)
        {

            _playerWeaponGun.MainCameraMovement(playerCamera.transform, currentWeaponMainly.forceShake, currentWeaponMainly.timeShake);
        }

        Ray ray = playerCamera.ScreenPointToRay(crosshairRect.transform.position);

        switch (currentWeaponMainly.modeShoot)
        {
            case ShootMode.Instantiation:
                GameManager.managerGame.modeShoots.InstantiateWeaponModel(transformLocationForInstantion, ray, currentWeaponMainly);
                break;
            case ShootMode.RayCast:
                GameManager.managerGame.modeShoots.RayCastFuncionality(ray, currentWeaponMainly, gunLayer);
               
                break;
          
        }

        if (concretActionWithTheRay != null)
            concretActionWithTheRay.Invoke(ray, gunLayer);



        StartCoroutine(CadenceDelay());
        StartCoroutine(TurnOutTheLights());
        

    }

    private IEnumerator TurnOutTheLights()
    {
        _currentSlotWeapon.TurnLight(true);
        yield return new WaitForSeconds(.1f);
        _currentSlotWeapon.TurnLight(false);
    }

    private IEnumerator CadenceDelay()
    {
        _cadenceBool = false;

        AnimatedGuns(Gun_Animations.Shoot);
        //if (playerAudio.audioGun != null)
        //{
        //    GameManager.managerGame.managerSound.SoundClip(currentWeaponMainly.clipShoot, playerAudio.audioGun, 1f, .75f, .85f, false, playerAudio.audioGun.isPlaying);
        //}



        yield return new WaitForSeconds(_currentSlotWeapon.gunScriptable.delayAfterShoot);

        AnimatedGuns(Gun_Animations.Reload);
        //GameManager.managerGame.managerAnimation.PlayerAnimationBool(GameManager.managerGame.managerAnimation.animPistolBool, true, animatorP);

     
        if (currentWeaponMainly.uniqueBehaiourWeaponIsAvailableBool)
            GameManager.managerGame.behaviourGunUnique.ControlGun_EmptyMethod();


        //GameManager.managerGame.managerPlayer.Reloading_PrintText(currentWeaponMainly.delayForCadence);

        if (currentWeaponMainly.uniqueBehaiourWeaponIsAvailableBool)
            GameManager.managerGame.behaviourGunUnique.ControlGun_EmptyMethod();

       



        for (float i = 0; i < currentWeaponMainly.delayForCadence; i+= .002f)
        {
           yield return new WaitForEndOfFrame();
        }


        //GameManager.managerGame.managerPlayer.FinishReload_PrintText();

        AnimatedGuns(Gun_Animations.Idle);

        yield return new WaitForSeconds(.1f);

        _cadenceBool = true;


    }

 

    #endregion

    #region ThrowableWeapon

    private void ThrowableSystem()
    {
        if (throwableWeapon != null)
        {

            GameObject go = Instantiate(throwableWeapon.throwPrefab, respawnThrowableObject.transform.position, transform.rotation);
            go.GetComponent<Rigidbody>().AddForce(playerCamera.transform.forward * throwableWeapon.throwableForce, ForceMode.Impulse);
            StartCoroutine(go.GetComponent<ThrowObjectPlayer>().WeaponDelay_Coroutine(throwableWeapon));
            GameManager.managerGame.playerInventory.AddThrowable(throwableWeapon, -1);
        }
    }

    private void AlternativeThrowableSystem()
    {
        if(alternativeThrowableWeapon != null)
        {
            GameObject go = Instantiate(alternativeThrowableWeapon.throwPrefab, respawnThrowableObject.transform.position, transform.rotation);
            go.GetComponent<Rigidbody>().AddForce(playerCamera.transform.forward * alternativeThrowableWeapon.throwableForce, ForceMode.Impulse);
            StartCoroutine(go.GetComponent<ThrowObjectPlayer>().WeaponDelay_Coroutine(alternativeThrowableWeapon));
            GameManager.managerGame.playerInventory.AddThrowable(alternativeThrowableWeapon, -1);
        }
    }
    #endregion

    #region ReloadingSystem

    private IEnumerator ReloadCoroutine()
    {
        _reloadButtonBool = false;

        yield return new WaitForSeconds(currentWeaponMainly.delayForReload);

        if (takeCorrectBullets(currentWeaponMainly) > 0)
        {
            for (int i = currentMagazine; i < currentWeaponMainly.limitMagazine; i++)
            {
                if (takeCorrectBullets(currentWeaponMainly) <= 0) break;

                currentMagazine++;
                ReduceBulletType(currentWeaponMainly);
            }
        }


        _reloadButtonBool = true;

    }

    private int takeCorrectBullets(WeaponScriptable gunScriptable)
    {
        //switch (gunScriptable.kindWeapon)
        //{
        //    case AmmoKind.pistolAmmo:
        //        return GameManager.managerGame.playerInventory.ammoPistol;
        //    case AmmoKind.rifleAmmo:
        //        return GameManager.managerGame.playerInventory.ammoRifle;
        //    case AmmoKind.shotgunAmmo:
        //        return GameManager.managerGame.playerInventory.ammoShotgun;
        //    case AmmoKind.submachineAmmo:
        //        return GameManager.managerGame.playerInventory.ammoSubmachine;
        //}

        return 0;
    }

    private void ReduceBulletType(WeaponScriptable gunScriptable)
    {
        //switch (gunScriptable.kindWeapon)
        //{
        //    case AmmoKind.pistolAmmo:
        //        GameManager.managerGame.playerInventory.ammoPistol--;
        //        break;
        //    case AmmoKind.rifleAmmo:
        //        GameManager.managerGame.playerInventory.ammoRifle--;

        //        break;
        //    case AmmoKind.shotgunAmmo:
        //        GameManager.managerGame.playerInventory.ammoShotgun--;

        //        break;
        //    case AmmoKind.submachineAmmo:
        //        GameManager.managerGame.playerInventory.ammoSubmachine--;

        //        break;
        //}
    }
    #endregion

    #region PickUpWeapon

    public void PickingANewWeapon(WeaponScriptable gun)
    {
        WeaponGrabble(gun);
    }

    public void AvailablePickableWeapon(bool iAmOverAPickableWeapon, WeaponScriptable gun, GameObject objectPick = null)
    {
        if (iAmOverAPickableWeapon)
        {
            _pickableWeapon = gun;
            weaponImageSystem.fillAmount = 0f;
            _currentPickableGunObject = objectPick;
            _currentPickableGunObject.SetActive(true);
            weaponImageSystem.gameObject.SetActive(true);

        }
        else
        {
            weaponImageSystem.fillAmount = 0f;
            _currentPickableGunObject = null;
            _pickableWeapon = null;
            weaponImageSystem.gameObject.SetActive(false);

        }
    }

    public void AvailableGun(WeaponScriptable gun, bool isOverBool)
    {
        if (isOverBool)
            textGun.text = changeGun + gun.weaponName;
        else
            textGun.text = "";
    }
    #endregion



    public void AccessAndDisableAllTheGuns()
    {
        AnimatedGuns(Gun_Animations.Idle);
    }

    private void AnimatedGuns(Gun_Animations animGuns)
    {
       
            switch (animGuns)
            {
                case Gun_Animations.Idle:
                    _currentSlotWeapon.AnimationToExecuted(_currentSlotWeapon.idleAnim, true);
                    _currentSlotWeapon.AnimationToExecuted(_currentSlotWeapon.reloadAnim, false);
                    _currentSlotWeapon.AnimationToExecuted(_currentSlotWeapon.attackAnim, false);
                    break;
                case Gun_Animations.Reload:
                    _currentSlotWeapon.AnimationToExecuted(_currentSlotWeapon.idleAnim, false);
                    _currentSlotWeapon.AnimationToExecuted(_currentSlotWeapon.reloadAnim, true);
                    _currentSlotWeapon.AnimationToExecuted(_currentSlotWeapon.attackAnim, false);
                    break;
                case Gun_Animations.Shoot:
                    _currentSlotWeapon.AnimationToExecuted(_currentSlotWeapon.idleAnim, false);
                    _currentSlotWeapon.AnimationToExecuted(_currentSlotWeapon.reloadAnim, false);
                    _currentSlotWeapon.AnimationToExecuted(_currentSlotWeapon.attackAnim, true);
                    break;
            }
        
       
       
    }
}
enum Gun_Animations
{
    Idle,
    Reload,
    Shoot
}