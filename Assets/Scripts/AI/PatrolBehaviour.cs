using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolBehaviour : StateMachine_Root
{

    #region Fields
    [Header("PATROL VARIABLES")]
    [Space]
    public float distanceWithTheClosestWaypointToUse;
    public float distanceWithThePlayerToStartTheChase;
    public PatrolSystem patrolWayPointsAssigned;
    [HideInInspector] public Transform lastCheckPointThanYouVistiHidden;
    private IdleBehaviour _behaviourIdle;
    private AttackBehaviour _behaviourAttack;
    private SearchGunBehaviour _behaviourSearchGun;
    private FleeBehaviour _behaviourFlee;
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
            case PossibleActions.Attack:
                managerAI.actionToDoNow = _behaviourAttack.EnterState;
                break;
            case PossibleActions.Idle:
                managerAI.actionToDoNow = _behaviourIdle.EnterState;
                break;
            case PossibleActions.SearchGun:
                managerAI.actionToDoNow = _behaviourSearchGun.EnterState;
                break;
            case PossibleActions.Flee:
                managerAI.actionToDoNow = _behaviourFlee.EnterState;
                break;

        }
    }
    public override void EnterState()
    {
      //  managerAI.SearchACurrentTargetToAttack();

        if(patrolWayPointsAssigned != null)
                patrolWayPointsAssigned.breakPatrolLoopBool = false;

        managerAI.actionToDoNow = StayState;
    }

    public override void ExitState()
    {

        if(managerAI.scriptableNPC.behaviourAI == AI_BEHAVIOUR.COWARD)
        {
            ChangeableState(PossibleActions.Flee);
            return;
        }
        patrolWayPointsAssigned.breakPatrolLoopBool = true;

        if (managerAI.currentEnemyGun == null)
            ChangeableState(PossibleActions.SearchGun);

         else 
        {
            ChangeableState(PossibleActions.Attack);
        }

    }

    public override void StayState()
    {

        if (patrolWayPointsAssigned != null && !patrolWayPointsAssigned.breakPatrolLoopBool)
        {
            if(!patrolWayPointsAssigned.isCurrentlyOpenThePatrolAccessCoroutineBool && patrolWayPointsAssigned != null)
                StartCoroutine(patrolWayPointsAssigned.Patrol_Coroutine(managerAI.agentNavMeshH));

            if(distanceWithThePlayerToStartTheChase > Mathf.Sqrt(Vector3.Distance(transform.position, managerAI.currentTransform.position)))
            {
                patrolWayPointsAssigned.breakPatrolLoopBool = true;
            }
        }
        else
        {
            managerAI.actionToDoNow = ExitState;
        }

        if(managerAI.currentEnemyGun == null)
        {
            managerAI.actionToDoNow = ExitState;
        }
    }
    #endregion

    #region Private_Methods
    private void Awake()
    {
        _behaviourSearchGun = GetComponent<SearchGunBehaviour>();
        _behaviourAttack = GetComponent<AttackBehaviour>();
        _behaviourFlee = GetComponent<FleeBehaviour>();
    }
    #endregion

    #region Static_Methods
    #endregion

}
