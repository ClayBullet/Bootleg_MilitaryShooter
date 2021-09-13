using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISoldier : AIManager
{
   
    #region Fields
    #endregion

    #region Constructors
    #endregion

    #region Getters
    #endregion

    #region Setters
    #endregion

    #region Public_Methods
    public override void DoSomething<T>(T paramenter)
    {
        
    }

    public override void DoSomethingIfYouDontHaveATool()
    {
       
    }

    public override void DoSomethingSpecial<T>(T parameter)
    {
        if (parameter.GetType() == typeof(GameObject))
        {

        }

    }
    public override void DoCancelCurrentAction()
    {
    }

    public override void DoSomethingWhenIDie()
    {
       
    }

  
    public override void DoSomethingWithThis<T>(T parameter)
    {
       if(parameter.GetType() == typeof(GameObject))
        {

        }
    }

    public override void DoSomethingWithThisCoordenatesAndThisQuaternions(Vector3 coord, Quaternion quat)
    {
    }

    public override void StunAction()
    {
       
    }

    public override void FinishStunAction()
    {
        
    }
    #endregion

    #region Private_Methods


    #endregion

    #region Static_Methods
    #endregion

}
