using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New NPC Scriptable", menuName = " New NPC Scriptable")]
public class NPC_Scriptable : ScriptableObject
{
    public string nameEnemy;

    [Header("CAN THROW PROJECTILES")]
    [Space]
    public bool isAvailableThanCanThrowProjectiles;
    public GameObject objProjectile;


    [Header("SPECIAL SKILL")]
    [Space]
    public float coolDownToStartTheCombatForUseTheSpecialSkill;
    public float coolDownToReloadTheSpecialSkill;

    [Header("HEALTH ")]
    [Space]
    public float maximumHealth;

    [Header("DAMAGE")]
    [Space]
    public float baseDamage;

    [Header("BEHAVIOUR AI")]
    [Space]
    public AI_BEHAVIOUR behaviourAI;
}
public enum AI_BEHAVIOUR
{
    BRAVE,
    COWARD
}