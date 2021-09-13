using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AttackBehaviour : StateMachine_Root
{
    #region Fields
    [Header("ATTACK VARIABLES")]
    [Space]
    public float distanceTOThePlayerToStopTheChase;
    public float maxDelayBeforeShoot;
    public float maxDelayBeforeToExecuteTheSpecialAction;
    private float _currentDelayForShoot;
  
    private IdleBehaviour _behaviourIdle;
    private PatrolBehaviour _behaviourPatrol;
    private StunBehaviour _behaviourStun;
    private SearchGunBehaviour _behaviourSearchAGun;
    private bool _isCurrentlyReadyForForAimingBool;
   
    [Header("MELEE ATTACK ")]
    [Space]
    public float distanceToMeleeAttack;
    public Vector2 rangeForHitting;
    protected bool isTheCurrentInterruptAttackBool;

    [Header("SPACE BETWEEN ATTACKS")]
    [Space]
    [SerializeField] private Vector2 maxDelayAfterAnAttackBool;
    [SerializeField] private float maxDelayBtwAttack;
    private float _currentDelayBetweenAttacks;


  

   

    [SerializeField] private Attack_Phases _phasesAttack;

    [Header("RETRY STATES")]
    [Space]
    [SerializeField] private Retry_States statesRetirement;
    [SerializeField] private float distanceToRetirement;
    private bool _isChoosedTheRetirementStyleTakeYetBool;

    [SerializeField] private LayerMask maskForGuns;

    [Header("FIRE GUN SCHEME")]
    [Space]
    [SerializeField] private Transform gunCasting;

     public bool isCurrentlyAttackingToTheTargetBool;

    private bool _cancelAllTheCurrentCoroutinesInsideHereBool;

    [Header("ATTACK METHOD")]
    [Space]
    public AttackMethod methodAttack;

    public PossibleActions actionPossible;
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
            case PossibleActions.Idle:
                managerAI.actionToDoNow = _behaviourIdle.EnterState;
                break;
            case PossibleActions.Stun:
                managerAI.actionToDoNow = _behaviourStun.EnterState;
                break;
            case PossibleActions.Attack:
                managerAI.actionToDoNow = EnterState;
                break;
            case PossibleActions.SearchGun:
                managerAI.actionToDoNow = _behaviourSearchAGun.EnterState;
                break;
        }
    }

    public override void EnterState()
    {
        managerAI.actionsPossibles = PossibleActions.Attack;

        _cancelAllTheCurrentCoroutinesInsideHereBool = false;

        _phasesAttack = Attack_Phases.ReadyForAttack;



        if (_behaviourSearchAGun != null && 
            _behaviourSearchAGun.existAWeaponNear() && managerAI.currentEnemyGun == null)
        {
            ChangeableState(PossibleActions.SearchGun);
            return;
        }

        managerAI.enemyIdentifierTeam = _selectTheCurrentTargetToGo();

        if (managerAI.enemyIdentifierTeam != null)
        {
            managerAI.currentTransform = managerAI.enemyIdentifierTeam.transform;

            if (managerAI.currentTransform != null)
            {
                managerAI.AssignedIHealth<Transform>(managerAI.currentTransform);
                managerAI.actionToDoNow = StayState;
            }
           

        }
        else
            ChangeableState(PossibleActions.Idle);

    }

    public override void ExitState()
    {

        _cancelAllTheCurrentCoroutinesInsideHereBool = true;


        managerAI.currentTransform = null;
        
                managerAI.enemyIdentifierTeam = null;
    

        isTheCurrentInterruptAttackBool = true;
        isCurrentlyAttackingToTheTargetBool = false;
        // managerAI.DoCancelCurrentAction();

        if (_selectTheCurrentTargetToGo() != null && !_behaviourStun.iAmStunnedBool)
        {
            managerAI.actionToDoNow = EnterState;
            return;
        }
        else if (_behaviourStun.iAmStunnedBool)
        {
            ChangeableState(PossibleActions.Stun);
            return;
        }

     
         ChangeableState(PossibleActions.Idle);
    }

    public override void StayState()
    {
        OrientatedToTheTarget();
        ChangeCurrentCombatState();

       

        switch (_phasesAttack)
        {
            case Attack_Phases.ReadyForAttack:
                ActionsBeforeAttack();
                break;
            case Attack_Phases.Attacking:
                ActionsDuringTheAttack();
                break;
            case Attack_Phases.Retry:
                ActionsAfterAttack();
                break;
        }

    }


    public void InterruptCurrentMeleeAttack()
    {
        isTheCurrentInterruptAttackBool = managerAI.currentTransform == null ||  distanceToMeleeAttack > Vector3.Distance(transform.position, managerAI.currentTransform.position);

     
        if (isTheCurrentInterruptAttackBool)
        {
            EntryInRetryState();
            managerAI.DoCancelCurrentAction();
        }
    }

    public void EntryInRetryState()
    {

        Invoke("ReadyForRetry", .5f);
    }

    private void ReadyForRetry()
    {
        _phasesAttack = Attack_Phases.Retry;
    }
    #endregion

    #region Private_Methods
    private void Awake()
    {
        _behaviourIdle = GetComponent<IdleBehaviour>();
        _behaviourPatrol = GetComponent<PatrolBehaviour>();
        _behaviourStun = GetComponent<StunBehaviour>();
        _behaviourSearchAGun = GetComponent<SearchGunBehaviour>();
    }

    private new void Start()
    {
        base.Start();
        statesRetirement = Retry_States.StayAway;
    }


    private IdentifierTeam _selectTheCurrentTargetToGo()
    {
        IdentifierTeam[] teamIdentifier = FindObjectsOfType<IdentifierTeam>();

        float maxDistance = Mathf.Infinity;
        IdentifierTeam teamID = null;
        foreach(IdentifierTeam id in teamIdentifier)
        {
            if(id.currentID != managerAI.teamID.currentID &&
                maxDistance > Vector3.Distance(transform.position, id.transform.position)
               )
            {
                teamID = id;
                maxDistance = Vector3.Distance(transform.position, id.transform.position);
            }
        }

       
        return teamID;
    }
    private void CancelCurrentMovement()
    {
        bool isCanceled = distanceToMeleeAttack < Vector3.Distance(transform.position, managerAI.currentTransform.position);

        if (isCanceled)
        {
            InterruptCurrentMeleeAttack();
        }
    }
    
    private void RefreshReloadNewAttack()
    {

      
            if (_currentDelayBetweenAttacks > maxDelayBtwAttack)
            {
                _isChoosedTheRetirementStyleTakeYetBool = false;
                _currentDelayBetweenAttacks = 0f;
                _phasesAttack = Attack_Phases.ReadyForAttack;
            }

            _currentDelayBetweenAttacks += Time.deltaTime;
       
      
    }

    private void OrientatedToTheTarget()
    {
       

        transform.LookAt(new Vector3(managerAI.currentTransform.position.x, 0f, managerAI.currentTransform.position.z));
        transform.rotation = Quaternion.Euler(0f, transform.eulerAngles.y, transform.eulerAngles.z);

    }
    private IEnumerator AimAndGetReadyTheGunToShootThePlayerCharacter_Coroutine()
    {
        _isCurrentlyReadyForForAimingBool = true;
        float _currentTime = 0f;
        bool _isStillAvailableToShootBool = true;
        while (_currentTime < maxDelayBeforeShoot)
        {
            _currentTime += Time.deltaTime;

            yield return new WaitForEndOfFrame();
        }

        if (_cancelAllTheCurrentCoroutinesInsideHereBool)
            yield break;

        if (_isStillAvailableToShootBool)
        {
            managerAI.DoSomethingWithAGun<GameObject>(managerAI.currentTransform.gameObject, managerAI.currentEnemyGun, gunCasting.position);
        }
        _isCurrentlyReadyForForAimingBool = false;

        _phasesAttack = Attack_Phases.Retry;
    }
    private IEnumerator ReadyForCombatStatus_Coroutine()
    {
        isCurrentlyAttackingToTheTargetBool = true;
       // isTheCurrentInterruptAttackBool = true;

        float _currentTime = 0f;
        float maxDelay = Random.Range(rangeForHitting.x, rangeForHitting.y);
        while (_currentTime < maxDelay)
        {
            if (isTheCurrentInterruptAttackBool) break;
            _currentTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        if (_cancelAllTheCurrentCoroutinesInsideHereBool)
            yield break;
        if (!isTheCurrentInterruptAttackBool)
        {
            managerAI.DoSomething<GameObject>(managerAI.currentTransform.gameObject);
        }
        else
        {
            isCurrentlyAttackingToTheTargetBool = false;
          
            yield break;
        }

        //  GameManager.managerGame.managerMartialArts.enemiesInteresInTheSameThreaten.Remove(managerAI.currentTransform.gameObject);

        isTheCurrentInterruptAttackBool = false;

        maxDelayBtwAttack = Random.Range(maxDelayAfterAnAttackBool.x, maxDelayAfterAnAttackBool.y);

    }
    private IEnumerator DoSomethingSpecialAction_Coroutine()
    {
        float _currentTime = 0f;
        bool _isStillAvailableToHappenTheSpecialAction = true;
        _isCurrentlyReadyForForAimingBool = true;
        managerAI.isHappeningAnSpecialSituationBool = false;

        while (_currentTime < maxDelayBeforeToExecuteTheSpecialAction)
        {
            _isCurrentlyReadyForForAimingBool = true;

            _currentTime += Time.deltaTime;

            yield return new WaitForEndOfFrame();
        }

        if (_isStillAvailableToHappenTheSpecialAction)
        {
            managerAI.DoSomethingSpecial<GameObject>(managerAI.currentTransform.gameObject);
        }

        _isCurrentlyReadyForForAimingBool = false;

    }

 

   

    private void ChangeCurrentCombatState()
    {


        if(_behaviourStun.iAmStunnedBool ||
            managerAI.assignedTargetHealth != null && !managerAI.assignedTargetHealth.isStillAlive
            || !managerAI.currentTransform.gameObject.activeSelf)
        {
            managerAI.actionToDoNow = ExitState;
            
            return;
        }


    }

    private void MinDistanceToStopSpeed()
    {
       
            if (Vector3.Distance(managerAI.transform.position, managerAI.currentTransform.position) < managerAI.currentEnemyGun.distanceEnemy)
            {
                managerAI.agentNavMeshH.isStopped = true;
            }
            else
            {
                managerAI.agentNavMeshH.isStopped = false;
            }
        

        managerAI.agentNavMeshH.SetDestination(managerAI.currentTransform.position);
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        if(managerAI != null && managerAI.currentTransform != null && managerAI.currentEnemyGun == null)
            Gizmos.DrawLine(transform.position, managerAI.currentTransform.position);
        else if(managerAI != null && managerAI.currentTransform != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, managerAI.currentTransform.position);
        }
    }


    private void ActionsBeforeAttack()
    {
       
        if (managerAI.currentEnemyGun != null)
        {
          
            if(maxDelayBeforeShoot < _currentDelayForShoot)
            {
                _phasesAttack = Attack_Phases.Attacking;
                _currentDelayForShoot = 0f;
            }
            _currentDelayForShoot += Time.deltaTime;
        }


        else if (managerAI.currentEnemyGun == null)
        {
            bool isHittingBool = !managerAI.isHappeningAnSpecialSituationBool && managerAI.currentEnemyGun != null || !managerAI.isHappeningAnSpecialSituationBool
                && distanceToMeleeAttack > Vector3.Distance(transform.position, managerAI.currentTransform.position)
                || managerAI.isHappeningAnSpecialSituationBool;


            if (isHittingBool && !managerAI.enemyIdentifierTeam.focusInHimButDontDoAnythingWithHim)
            {
                _phasesAttack = Attack_Phases.Attacking;
            }
        }



        MinDistanceToStopSpeed();

    }

    private void ActionsDuringTheAttack()
    {
            
        if(managerAI.currentEnemyGun == null && !isCurrentlyAttackingToTheTargetBool)
        {

            managerAI.agentNavMeshH.isStopped = true;
            CancelCurrentMovement();
            if (!_isCurrentlyReadyForForAimingBool )
            {
                
                if (!managerAI.isHappeningAnSpecialSituationBool)
                {
                    StartCoroutine(ReadyForCombatStatus_Coroutine());
                    return;
                }
                else
                {
                    StartCoroutine(DoSomethingSpecialAction_Coroutine());
                    return;
                }
            }


        }
        else
        {
            if (managerAI.currentEnemyGun != null && !_isCurrentlyReadyForForAimingBool && _dontInterruptTheCurrentShootBool())
            {
                StartCoroutine(AimAndGetReadyTheGunToShootThePlayerCharacter_Coroutine());
                return;
            }
        }

    }

    private void ActionsAfterAttack()
    {
        isCurrentlyAttackingToTheTargetBool = false;

       
        if (!managerAI.currentTransform.gameObject.activeSelf)
        {
            managerAI.actionToDoNow = ExitState;
            return;
        }

        if (!managerAI.teamID.focusInHimButDontDoAnythingWithHim)
        {
            //managerAI.DoCancelCurrentAction();
            switch (statesRetirement)
            {
                case Retry_States.StayAway:
                    StayAway();
                    break;
                case Retry_States.TurnAround:
                    break;
            }

        }


       

      //  managerAI.agentNavMeshH.isStopped = false;
       RefreshReloadNewAttack();
    }

    private void StayAway()
    {
        if (!_isChoosedTheRetirementStyleTakeYetBool)
        {
            Vector3 coordsToRun = -transform.forward * distanceToRetirement;
            managerAI.agentNavMeshH.SetDestination(coordsToRun);
            managerAI.agentNavMeshH.isStopped = false;
            _isChoosedTheRetirementStyleTakeYetBool = true;
        }
     
    }

    private bool _dontInterruptTheCurrentShootBool()
    {
        if(managerAI.currentEnemyGun != null)
        {
            RaycastHit hit;
            if(Physics.Linecast(managerAI.currentTransform.position, transform.position, out hit, maskForGuns))
            {
                if(hit.collider.transform != managerAI.currentTransform && hit.collider.transform != transform)
                {
                    return false;
                }
            }
            return true;
        }
        return false;
    }

  
    #endregion

    #region Static_Methods
    #endregion

}
public enum Direction
{
    Forward,
    BackWard,
    Right,
    Left
}

public enum Attack_Phases
{
    ReadyForAttack,
    Attacking,
    Retry
}

public enum Retry_States
{
    StayAway,
    TurnAround
}

public enum AttackMethod
{
    CoverAttack,
    RegularAttack,
    SpecialAttack
}