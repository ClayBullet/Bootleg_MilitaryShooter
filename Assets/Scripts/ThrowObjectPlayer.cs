using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowObjectPlayer : MonoBehaviour
{
    private DamageExplossion _explossionDamage;

    private void Awake()
    {
        _explossionDamage = GetComponent<DamageExplossion>();
    }
    public IEnumerator WeaponDelay_Coroutine(ThrowScriptable scriptableThrow)
    {
        yield return new WaitForSeconds(scriptableThrow.delayForExplossion);
        if(scriptableThrow.effectExplossion != null)
        {
            GameObject go = Instantiate(scriptableThrow.effectExplossion, transform.position, Quaternion.Euler(-90f, 90f, 0f));

        }
        _explossionDamage.CurrentExplossionDamage(transform.position, 100000, DamageStates.Stun);
        Destroy(this.gameObject);
    }
}
