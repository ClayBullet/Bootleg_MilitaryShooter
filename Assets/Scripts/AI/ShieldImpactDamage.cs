using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldImpactDamage : MonoBehaviour, IHealth, IDamage, IDeath
{
    #region Fields
    [SerializeField] private float maxShieldHealth;
    public float maxHealth { get; set; }
    public float currentHealth { get; set; }
    public GameObject ownerHealth { get; set; }
    public Type currentFunctionForAccess { get; set; }
    public object currentObject { get; set; }
    public bool isStillAlive { get; set; }
    public bool indamageable { get; set; }
    public bool isInShootable { get; set; }

    [SerializeField] private float divisibleDamage;
    private AIShield _shieldAI;
    #endregion

    #region Constructors
    #endregion

    #region Getters
    #endregion

    #region Setters
    #endregion

    #region Public_Methods
    public void DamageState(DamageStates stateDamage)
    {
        
    }

    public void DeathState()
    {
        throw new NotImplementedException();
    }

    public void ReceiveDamage(float damage)
    {
        currentHealth -= damage;

        if(currentHealth % divisibleDamage == 0)
        {
            _shieldAI.TakeDamageableShield();
        }
    }

    public void RecoverHealth(float recoverHealth)
    {
        throw new NotImplementedException();
    }
    #endregion

    #region Private_Methods
    private void Awake()
    {
        _shieldAI = GetComponentInParent<AIShield>();
    }

    private void Start()
    {
        maxHealth = maxShieldHealth;
        currentHealth = maxHealth;
        isInShootable = true;
    }
    #endregion

    #region Static_Methods
    #endregion



}
