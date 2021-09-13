using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseBehaviour : StateMachine_Root
{
    #region Fields
    private AttackBehaviour _behaviourAttack;
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
        throw new System.NotImplementedException();
    }

    public override void EnterState()
    {
        managerAI.actionsPossibles = PossibleActions.Chase;

    }

    public override void ExitState()
    {
        throw new System.NotImplementedException();
    }

    public override void StayState()
    {
        throw new System.NotImplementedException();
    }
    #endregion

    #region Private_Methods
    #endregion

    #region Static_Methods
    #endregion

}
