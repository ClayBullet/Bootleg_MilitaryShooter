using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Throw Object")]
public class ThrowScriptable : ScriptableObject
{
    public string idName;

    [TextArea] public string textDescription;

   
    public GameObject throwPrefab;

    [Header("WEAPON THROWABLE")]
    [Space]
    public float throwableForce;

    public float delayForExplossion;

    public GameObject effectExplossion;
}
[System.Serializable]
public class AreaDamage
{
    public string areaString;
    public float distanceArea;
    public float damageArea;

    public AreaDamage(string _areaString, float _distanceArea, float _damageArea)
    {
        _areaString = areaString;
        _damageArea = damageArea;
        _distanceArea = distanceArea;
    }
}