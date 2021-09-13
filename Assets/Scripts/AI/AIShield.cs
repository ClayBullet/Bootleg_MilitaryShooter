using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIShield : AIManager
{
    #region Fields
    [SerializeField] private Transform shieldT;
    [SerializeField] private float angleToShoot;
    
    #endregion

    #region Constructors
    #endregion

    #region Getters
    #endregion

    #region Setters
    #endregion

    #region Public_Methods
    public void TakeDamageableShield()
    {
        _behaviourStun.iAmStunnedBool = true;
    }
    public override void DoCancelCurrentAction()
    {
       
    }

    public override void DoSomething<T>(T paramenter)
    {
        
    }

    public override void DoSomethingIfYouDontHaveATool()
    {
       
    }

    public override void DoSomethingSpecial<T>(T paramenter)
    {
       
    }

    public override void DoSomethingWhenIDie()
    {
       
    }

    public override void DoSomethingWithThis<T>(T parameter)
    {
    }

    public override void DoSomethingWithThisCoordenatesAndThisQuaternions(Vector3 coord, Quaternion quat)
    {
    }
    #endregion

    #region Private_Methods
    
    public override void StunAction()
    {
        StartCoroutine(Stun_IEnumerator(angleToShoot));
    }

    public override void FinishStunAction()
    {
        StartCoroutine(Stun_IEnumerator(0));

    }

    private IEnumerator Stun_IEnumerator(float angleToRotate)
    {
        float _currentTime = 0f;
        float _currentShieldPosInZ = shieldT.eulerAngles.z;
        while (shieldT.eulerAngles.z != angleToRotate)
        {
            shieldT.transform.rotation = Quaternion.Euler(0, 0, Mathf.Lerp(_currentShieldPosInZ, angleToRotate, _currentTime));
            _currentTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }
    #endregion

    #region Static_Methods
    #endregion

}
