using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class MeleeActionPlayer : MonoBehaviour
{
    #region Fields
    [SerializeField] private float maxDistanceForMeleeAttack;
    [SerializeField] private float currentDelayMelee;
    private bool _isCurrentlyTriggerBool;
    private WeaponPlayerManager _managerWeaponPlayer;
    #endregion

    #region Constructors
    #endregion

    #region Getters
    #endregion

    #region Setters
    #endregion

    #region Public_Methods
    #endregion

    #region Private_Methods

    private void Start()
    {

        GameManager.managerGame.managerInput.DownPress_HitButton += HittingAction;
        _managerWeaponPlayer = GetComponent<WeaponPlayerManager>();
    }

    private void HittingAction()
    {
        _isCurrentlyTriggerBool = true;
        StartCoroutine(DelayHit_Coroutine());
    }

    private IEnumerator DelayHit_Coroutine()
    {
        yield return new WaitForSeconds(currentDelayMelee);
        _isCurrentlyTriggerBool = false;
    }
    private void LateUpdate()
    {
        if (_isCurrentlyTriggerBool)
        {
            RaycastHit hit;
            Ray ray = _managerWeaponPlayer.playerCamera.ScreenPointToRay(_managerWeaponPlayer.currentCrosshairTransform.position);
            if (Physics.Raycast(ray, out hit, maxDistanceForMeleeAttack))
            {
                if (hit.collider.CompareTag("Enemy"))
                {
                    hit.collider.GetComponent<IDamage>().ReceiveDamage(1000000);
                }
            }
        }
    }
    #endregion

    #region Static_Methods
    #endregion
}
