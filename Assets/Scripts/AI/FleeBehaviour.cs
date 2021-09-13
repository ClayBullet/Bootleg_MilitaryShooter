using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
public class FleeBehaviour : StateMachine_Root
{
    #region Fields
    private AttackBehaviour _behaviourAttack;
    private PatrolBehaviour _behaviourPatrol;
    private IdleBehaviour _behaviourIdle;
    [SerializeField] private Vector2 rangeForFlee;
    [SerializeField] private float minimumDistanceToConsiderSave;
    private Vector3 _currentTargetToReach;
    #endregion

    #region Constructors
    #endregion

    #region Getters
    #endregion

    #region Setters
    #endregion

    #region Public_Methods
    #endregion

    #region Private_Methods
    private void Awake()
    {
        _behaviourAttack = GetComponent<AttackBehaviour>();
        _behaviourPatrol = GetComponent<PatrolBehaviour>();
        _behaviourIdle = GetComponent<IdleBehaviour>();
    }
    #endregion

    #region Static_Methods
    #endregion
    public override void ChangeableState(PossibleActions actionPossible)
    {
        switch (actionPossible)
        {
            case PossibleActions.Attack:
                managerAI.actionToDoNow = _behaviourAttack.EnterState;
                break;
            case PossibleActions.Patrol:
                managerAI.actionToDoNow = _behaviourPatrol.EnterState;

                break;
            case PossibleActions.Idle:
                managerAI.actionToDoNow = _behaviourPatrol.EnterState;

                break;
        }
    }

    public override void EnterState()
    {
        managerAI.actionsPossibles = PossibleActions.Flee;
        Vector3 oppositeDir = (managerAI.currentTransform.position - transform.position).normalized * -1;
        oppositeDir *= Random.Range(rangeForFlee.x, rangeForFlee.y);
        _currentTargetToReach = oppositeDir;
        managerAI.agentNavMeshH.SetDestination(oppositeDir);
        managerAI.actionToDoNow = StayState;
    }

    public override void ExitState()
    {
        ChangeableState(PossibleActions.Patrol);
    }

    public override void StayState()
    {
       if(minimumDistanceToConsiderSave > Vector3.Distance(transform.position, _currentTargetToReach))
        {
            managerAI.actionToDoNow = ExitState;
        }
    }
}
