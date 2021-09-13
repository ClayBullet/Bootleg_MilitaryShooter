using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchGunBehaviour : StateMachine_Root
{
    #region Fields
    [Header("SEARCH PROPERTIES")]
    [Space]
    [SerializeField] private float minimumDistanceToThinkIfICanTakeThisGun;
    [SerializeField] private float minimumToTakeAGun;
    public Transform placeHolderObj;
    private WeaponSlot _slotTakeAnGun;
    private GameObject _objGun;
    private AttackBehaviour _behaviourAttack;
    private PatrolBehaviour _behaviourPatrol;
    #endregion

    #region Constructors
    #endregion

    #region Getters
    #endregion

    #region Setters
    #endregion

    #region Public_Methods
    public override void ChangeableState(PossibleActions actionPossible)
    {
        switch (actionPossible)
        {
            case PossibleActions.Attack:
                managerAI.actionToDoNow = _behaviourAttack.EnterState;
                break;
            case PossibleActions.Patrol:
                managerAI.actionToDoNow = _behaviourPatrol.EnterState;
                break;
        }
    }

    public override void EnterState()
    {
        if(managerAI.currentEnemyGun != null)
        {
            managerAI.actionToDoNow = ExitState;
            return;
        }

        WeaponSlot[] slotWeaponsArray = FindObjectsOfType<WeaponSlot>();
        float maxDistance = Mathf.Infinity;
        for (int i = 0; i < slotWeaponsArray.Length; i++)
        {
            if(Vector3.Distance(transform.position, slotWeaponsArray[i].transform.position) < maxDistance && !slotWeaponsArray[i].currentOwner)
            {
                _slotTakeAnGun = slotWeaponsArray[i];
               
                maxDistance = Vector3.Distance(transform.position, slotWeaponsArray[i].transform.position);
            }
        }
        if (_slotTakeAnGun != null)
        {
            _slotTakeAnGun.currentOwner = this.gameObject;
            managerAI.actionToDoNow = StayState;
        }
        else
            ChangeableState(PossibleActions.Attack);

    }

    public override void ExitState()
    {
        managerAI.currentEnemyGun = _slotTakeAnGun.gunScriptable;
        managerAI.slotWeaponPublic = _slotTakeAnGun;
        _slotTakeAnGun.gameObject.SetActive(false);

        GameObject go = Instantiate(_slotTakeAnGun.gunScriptable.objectWeapon, placeHolderObj.position, placeHolderObj.rotation);
        _objGun = go;
        go.transform.SetParent(placeHolderObj);
        ChangeableState(PossibleActions.Attack);
    }

    public override void StayState()
    {
       
        if (minimumToTakeAGun > Vector3.Distance(_slotTakeAnGun.transform.position, transform.position))
        {
            managerAI.actionToDoNow = ExitState;

        }
        else
        {
            managerAI.agentNavMeshH.SetDestination(_slotTakeAnGun.transform.position);
        }

    }

    public bool existAWeaponNear()
    {
        WeaponSlot[] slotWeaponsArray = FindObjectsOfType<WeaponSlot>();

        foreach(WeaponSlot slot in slotWeaponsArray)
        {
            if (Vector3.Distance(transform.position, slot.transform.position) < minimumDistanceToThinkIfICanTakeThisGun && !slot.currentOwner)
                return true;
        }
        return false;
    }

    public void RemoveWeaponSlot()
    {
        Destroy(_objGun);
    }
    #endregion

    #region Private_Methods

    private void Awake()
    {
        _behaviourPatrol = GetComponent<PatrolBehaviour>();
        _behaviourAttack = GetComponent<AttackBehaviour>();
    }
    #endregion

    #region Static_Methods
    #endregion

}
