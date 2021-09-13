using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimPlayer : MonoBehaviour
{
    private bool _aimSystemBool;
    public float curentFOV;
    [SerializeField] private Transform currentWeaponTransform;
    [SerializeField] private Transform originWeaponTransform;
    [SerializeField] private Transform aimPosition;
    [SerializeField] private float aimDelayPlayer;
    private bool _accessToTheCoroutineBool;
    private float _currentTime;
    private bool _addValueToCurrentFovBool;
     Camera mainCamera;
    [SerializeField] private Camera gunCamera;
    [SerializeField] private Transform crosshair;
    private Quaternion _takeRotation;
    private WeaponPlayerManager _weaponPlayerManager;
    private void Start()
    {
        _weaponPlayerManager = GetComponent<WeaponPlayerManager>();
        mainCamera = GetComponentInChildren<Camera>();

        AssignControls();
        _accessToTheCoroutineBool = true;
        _takeRotation = currentWeaponTransform.rotation;
        originWeaponTransform.position = currentWeaponTransform.position;
    }

    public void AssignControls()
    {
        GameManager.managerGame.managerInput.DownPress_AimButton += ZoomActivate;
        GameManager.managerGame.managerInput.UpPress_AimButton += ZoomDesactivate;
    }
    void Update()
    {
        AimSystem();
       
         
    }

    public void ZoomActivate()
    {
            _aimSystemBool = true;
    }
    public void ZoomDesactivate()
    {
        _aimSystemBool = false;
    }

    private void AimSystem()
    {
       
            if (_aimSystemBool)
            {
                if (_addValueToCurrentFovBool)
                {
                    mainCamera.fieldOfView = curentFOV - _weaponPlayerManager.currentWeaponPublic.fieldOfView;
                    gunCamera.fieldOfView = curentFOV - _weaponPlayerManager.currentWeaponPublic.fieldOfView;
                    _addValueToCurrentFovBool = false;
                }
            _currentTime += Time.deltaTime;
                if (currentWeaponTransform.position != aimPosition.position)
            {
                currentWeaponTransform.position = Vector3.Lerp(currentWeaponTransform.position, aimPosition.position, _currentTime);

            }
            else
                _currentTime = 0f;
           // currentWeaponTransform.rotation = aimPosition.rotation;


        }
        else
            {
                mainCamera.fieldOfView = curentFOV;
                gunCamera.fieldOfView = curentFOV;
                _addValueToCurrentFovBool = true;

                _currentTime += Time.deltaTime;
   
                if (currentWeaponTransform.position != originWeaponTransform.position)
                {
                    currentWeaponTransform.position = Vector3.Lerp(currentWeaponTransform.position, originWeaponTransform.position, _currentTime);
                }
            else
                    _currentTime = 0f;

            }
        

    }

   

}
