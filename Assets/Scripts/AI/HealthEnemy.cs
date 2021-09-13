using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class HealthEnemy : MonoBehaviour, IHealth, IDeath, IDamage, IStun
{
    #region Fields
    public float maxHealth { get; set; }
    public float currentHealth { get; set; }
    public GameObject ownerHealth { get; set; }
    public Type currentFunctionForAccess { get; set; }
    public object currentObject { get; set; }
    public bool indamageable { get; set ; }
    public bool isInShootable { get; set; }
    public Action actionWhenIsHitting { get; set; }
    public Action actionWhenIsDeath { get; set; }
    public bool isStillAlive { get; set; }
    public float damageToDo { get; set; }

    private AttackBehaviour _behaviourAttack;
    private StunBehaviour _behaviourStun;
    private AIManager _managerAI;
    #endregion

    #region Constructors
    #endregion

    #region Getters
    #endregion

    #region Setters
    #endregion

    #region Public_Methods
    public void DeathState()
    {
       
        isStillAlive = false;
        this.gameObject.SetActive(false);
    }

    public void ReceiveDamage(float damage)
    {

        if (actionWhenIsHitting != null)
            actionWhenIsHitting.Invoke();


        currentHealth -= damage;

        if(currentHealth <= 0)
        {
            DeathState();
        }
    }

    public void RecoverHealth(float recoverHealth)
    {
    }
    #endregion

    #region Private_Methods

    private void Awake()
    {
        isStillAlive = true;
        _behaviourAttack = GetComponent<AttackBehaviour>();
        _behaviourStun = GetComponent<StunBehaviour>();
        _managerAI = GetComponent<AIManager>();
    }

    private void Start()
    {
        actionWhenIsHitting += _behaviourAttack.InterruptCurrentMeleeAttack;
        maxHealth = _managerAI.scriptableNPC.maximumHealth;
        currentHealth = maxHealth;
        damageToDo = _managerAI.scriptableNPC.baseDamage;
    }

    public void PointsWhenYouReceiveTheDamage(Vector3 coords)
    {
       
    }

    private void OnDisable()
    {
        isStillAlive = false;
    }

    public void BeStunedByAHit()
    {
        _behaviourStun.iAmStunnedBool = true;
        _managerAI.teamID.focusInHimButDontDoAnythingWithHim = true;
    }

    public void DamageState(DamageStates stateDamage)
    {
        throw new NotImplementedException();
    }
    #endregion

    #region Static_Methods
    #endregion


}
