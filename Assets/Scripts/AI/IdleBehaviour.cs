using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleBehaviour : StateMachine_Root
{
    #region Fields
    private AttackBehaviour _behaviourAttack;
    private PatrolBehaviour _behaviourPatrol;
    private StunBehaviour _behaviourStun;
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
        switch (actionPossible)
        {
            case PossibleActions.Patrol:
                managerAI.actionToDoNow = _behaviourPatrol.EnterState;
                break;
            case PossibleActions.Attack:
                managerAI.actionToDoNow = _behaviourAttack.EnterState;
                break;
            case PossibleActions.Stun:
                managerAI.actionToDoNow = _behaviourStun.EnterState;
                break;
        }
    }

    public override void EnterState()
    {
        managerAI.actionsPossibles = PossibleActions.Idle;

        managerAI.actionToDoNow = StayState;
    }

    public override void ExitState()
    {
        if (!_behaviourStun.iAmStunnedBool)
            ChangeableState(PossibleActions.Attack);
        else
            ChangeableState(PossibleActions.Stun);
    }

    public override void StayState()
    {
       
        if(managerAI.minimumDistanceBetweenPlayerAndNPC < managerAI.distanceViewCharacter && managerAI.currentTransform != null)
                managerAI.actionToDoNow = ExitState;

        else if(_behaviourStun.iAmStunnedBool)
                managerAI.actionToDoNow = ExitState;


    }
    #endregion

    #region Private_Methods
    private void Awake()
    {
        _behaviourAttack = GetComponent<AttackBehaviour>();
        _behaviourPatrol = GetComponent<PatrolBehaviour>();
        _behaviourStun = GetComponent<StunBehaviour>();
    }
    #endregion

    #region Static_Methods
    #endregion

}


