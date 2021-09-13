using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[DisallowMultipleComponent]
public class StunBehaviour : StateMachine_Root
{
    #region Fields
    private AttackBehaviour _behaviourAttack;
    private SearchGunBehaviour _behaviourSearchAGun;
    public bool iAmStunnedBool;
    [Header("STUN CHARACTER")]
    [Space]
    [SerializeField] private float maxTimeForBeStuned;
    [SerializeField] private float maxTimeForRecoverEntirely;
    [SerializeField] private float maxDistanceToRandomizeRetry;
    [SerializeField] private LayerMask maskLayer;
    private float _currentTimeBeStunned;
    private float _currentTimeForRecoverEntirely;
    private Vector3 _retryCoords;
    private bool _isFinishedForBeingStunningBool;
    public StunKind kindStun;
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
           
        }
    }

    public override void EnterState()
    {
        _retryCoords = RandomNavSphere(transform.position, maxDistanceToRandomizeRetry, maskLayer);
        kindStun = StunKind.Stun;
        if (managerAI.agentNavMeshH.enabled)
        {
            managerAI.agentNavMeshH.SetDestination(_retryCoords);
            managerAI.agentNavMeshH.isStopped = true;
            managerAI.actionToDoNow = StayState;
        }

        managerAI.StunAction();
        switch (kindStun)
        {
            case StunKind.Disarm:
                if (managerAI.currentEnemyGun != null)
                {
                    managerAI.currentEnemyGun = null;
                    Destroy(managerAI.slotWeaponPublic.gameObject);
                }
                    
                break;
            case StunKind.Stun:
                if(managerAI.animator != null)
                    GameManager.managerGame.managerAnimation.Animation_Bool(managerAI.animator, AnimationManager.isStunnedBoolean, true);
                managerAI.teamID.focusInHimButDontDoAnythingWithHim = true;
               // ThrowTheCurrentGunIfExistOfCourse();
                break;
        }
      
        
    }

    public override void ExitState()
    {
        managerAI.FinishStunAction();
        kindStun = StunKind.Stun;
        managerAI.agentNavMeshH.isStopped = false;
        managerAI.teamID.focusInHimButDontDoAnythingWithHim = false;
        iAmStunnedBool = false;
        ChangeableState(PossibleActions.Attack);
    }

    public override void StayState()
    {
        if (!_isFinishedForBeingStunningBool)
        {
           
            if (_currentTimeBeStunned > maxTimeForBeStuned)
            {
                switch (kindStun)
                {
                    case StunKind.Stun:
                        if(managerAI.animator != null)
                        {
                            GameManager.managerGame.managerAnimation.Animation_Bool(managerAI.animator, AnimationManager.isRecoveringBool, true);
                            GameManager.managerGame.managerAnimation.Animation_Bool(managerAI.animator, AnimationManager.isStunnedBoolean, false);
                        }
                       

                        break;
                    case StunKind.Disarm:
                        break;
                }
                _isFinishedForBeingStunningBool = true;

                _currentTimeBeStunned = 0f;
            }
            _currentTimeBeStunned += Time.deltaTime;
        }
        else
        {
            if(_currentTimeForRecoverEntirely > maxTimeForRecoverEntirely)
            {
                if (managerAI.animator != null)
                    GameManager.managerGame.managerAnimation.Animation_Bool(managerAI.animator, AnimationManager.isRecoveringBool, false);

                managerAI.actionToDoNow = ExitState;
                _currentTimeForRecoverEntirely = 0;
                _isFinishedForBeingStunningBool = false;


            }
            _currentTimeForRecoverEntirely += Time.deltaTime;
        }
       
    }

   

    #endregion

    #region Private_Methods

    private void Awake()
    {
        _behaviourAttack = GetComponent<AttackBehaviour>();
        _behaviourSearchAGun = GetComponent<SearchGunBehaviour>();
    }

    private Vector3 RandomNavSphere(Vector3 origin, float distance, int layermask)
    {
        Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * distance;

        randomDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randomDirection, out navHit, distance, layermask);

        return navHit.position;
    }

    private void ThrowTheCurrentGunIfExistOfCourse()
    {
        if(managerAI.currentEnemyGun != null)
        {
            GameObject go = Instantiate(managerAI.currentEnemyGun.pickableWeapon, _behaviourSearchAGun.placeHolderObj.position, _behaviourSearchAGun.placeHolderObj.rotation);
            managerAI.currentEnemyGun = null;
            _behaviourSearchAGun.RemoveWeaponSlot();
        }
    }
    #endregion

    #region Static_Methods
    #endregion

}
public enum StunKind
{
    Disarm,
    Stun
}