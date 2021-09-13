using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPlayer : MonoBehaviour, IHealth, IDamage, IDeath
{
    [SerializeField] private float pCurrentHealth;
    [SerializeField] private float pMaxHealth;
    public float maxHealth { get; set; }
    public float currentHealth { get; set; }
    public GameObject ownerHealth { get; set; }
    public Type currentFunctionForAccess { get; set; }
    public object currentObject { get; set; }
    public bool indamageable { get; set; }
    public bool isInShootable { get; set; }
    public bool isStillAlive { get; set; }


    [SerializeField] private PhotoCamera cameraPhoto;
    private void Start()
    {
        currentHealth = pCurrentHealth;
        maxHealth = pMaxHealth;
       // isInShootable = true;
    }
    public void ReceiveDamage(float damage)
    {
       
            currentHealth -= damage;
            pCurrentHealth = currentHealth;
            if (currentHealth <= 0f)
                DeathState();
        
       
    }

    public void RecoverHealth(float recoverHealth)
    {
        currentHealth += recoverHealth;
        pCurrentHealth = currentHealth;

        if (currentHealth > maxHealth)
            currentHealth = maxHealth;
    }


    public void ExecutionAI(AIManager managerAI)
    {
    }

    public void DeathState()
    {
     
            //if(executedAI != null)
            //GameManager.managerGame.executerRewind.ExecutionerRewind(executedAI);
            GameManager.managerGame.managerGlobalPopUp.ActivateDeathPopUpManager();
            GameManager.managerGame.managerInput.concretActionsForRealizeBool = true;
          
        


    }

    public void DamageState(DamageStates stateDamage)
    {

        switch (stateDamage)
        {
            case DamageStates.Stun:
                cameraPhoto.CallAll();
                break;
        }
    }
}
