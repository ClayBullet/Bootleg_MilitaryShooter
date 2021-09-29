using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = " New Weapon", menuName = " New Weapon ")]
public class WeaponScriptable : ScriptableObject
{

    [Header("WEAPON NAME")]
    [Space]
    public string weaponName;

    [Header("DAMAGE PROPERTY")]
    [Space]
    public float damageBody;
    public float damageHead;
    public float damageEnemy;
    [Header("CAPACITY WEAPON")]
    [Space]
    public int limitMagazine;
    public float delayForReload;
    [Header("WEAPON CADENCE")]
    [Space]
    public float delayForCadence;
    public float delayAfterShoot;
    [Header("FIELD OF VIEW")]
    [Space]
    [Range(0, 100)] public int fieldOfView;

    [Header("WEAPON MODEL")]
    [Space]
    public GameObject objectWeapon;

    [Header("PICKABLE WEAPON")]
    [Space]
    public GameObject pickableWeapon;
    public GameObject throwWeapon;

    [Header("WEAPON OFFSET")]
    [Space]
    public Vector3 weaponOffset;

    [Header("WEAPON THROWABLE")]
    [Space]
    public float throwableForce;
    [Header("AI VARIABLES - AI")]
    [Space]
    public float distanceEnemy;

    [Header("ATTACK MODE - AI")]
    [Space]
    public int burstFire;
    public int automaticFire;

    [Header("HEAR NOISE - AI")]
    [Space]
    public float hearNoiseAI;


    [Header("SHOOT MODE AVAILABLE")]
    [Space]
    public ShootMode modeShoot;

    [Header("INSTANTIATION MODE")]
    [Space]
    public GameObject projectileGun;
    public float speedProjectile;
    public float limitLife;

  


    [Header("SCREEN SHAKE")]
    [Space]
    public float timeShake = 0.05f;
    public float forceShake = 0.05f;

    [Header("WEAPON SHOOT BOOL")]
    [Space]
    public bool holdTheTriggerButtonForShootBool;
    public float maxTimeForShootBool;
    public float timeForFreezeWeapon;

    [Header("UNIQUE BEHAVIOUR FOR WEAPONS")]
    [Space]
    public bool uniqueBehaiourWeaponIsAvailableBool;

    [Header("SOUNDS WEAPONS")]
    [Space]
    public AudioClip clipShoot;
    public AudioClip clipHit;

    [Header("DISTANCE FOR SHOOT")]
    [Space]
    public float distanceToShoot;

    [Header("INTERFACE REPRESENTATION")]
    [Space]
    public Sprite spriteBullet;

    
}
public enum ShootMode
{
    RayCast,
    Instantiation
}