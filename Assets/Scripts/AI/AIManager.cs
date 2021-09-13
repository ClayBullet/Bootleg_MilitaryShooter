using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public abstract class AIManager : MonoBehaviour, IIdentifier, IDisarm
{
    #region Fields
    public Action actionToDoNow;
    public float distanceViewCharacter;
    [HideInInspector]public NavMeshAgent agentNavMeshH;
    public bool isHappeningAnSpecialSituationBool;
    public Transform currentTransform;
    public PossibleActions actionsPossibles;
    public float minimumDistanceBetweenPlayerAndNPC
    {
        get
        {
            if (currentTransform != null)
                return Mathf.Sqrt(Vector3.Distance(currentTransform.position, transform.position));
            else
                return Mathf.Infinity;
        }
    }

    [SerializeField] protected int idTeam;
    public int identifier { get { return idTeam; } set {  } }

    [Header("ENEMY SCRIPTABLES")]
    [Space]
    public NPC_Scriptable scriptableNPC;


    public WeaponScriptable currentEnemyGun;

    public WeaponSlot slotWeaponPublic;

    [HideInInspector] public IdentifierTeam teamID;


    public Animator animator;

     public IHealth assignedTargetHealth;

    [HideInInspector] public IdentifierTeam enemyIdentifierTeam;

    [HideInInspector] public PainController controlPain;

    protected StunBehaviour _behaviourStun;
    #endregion

    #region Constructors
    #endregion

    #region Getters
    #endregion

    #region Setters
    #endregion

    #region Public_Methods
    public abstract void DoSomethingWithThis<T>(T parameter);

    public virtual void DoSomethingWithAGun<T>(T paramenter, WeaponScriptable gun, Vector3 coordsForCasting)
    {
        if(gun.modeShoot == ShootMode.RayCast)
        {
           
        }
    }
    public abstract void DoSomething<T>(T paramenter);

    public abstract void DoSomethingSpecial<T>(T paramenter);

    public abstract void DoSomethingWhenIDie();

    public abstract void DoSomethingWithThisCoordenatesAndThisQuaternions(Vector3 coord, Quaternion quat);

    public abstract void DoSomethingIfYouDontHaveATool();

    public abstract void DoCancelCurrentAction();

    public abstract void StunAction();

    public abstract void FinishStunAction();


    public void SearchACurrentTargetToAttack(Transform t)
    {
        if(t != null)
        {
            currentTransform = t;
        }
       
    }

    public void AssignedIHealth<T>(T currentFormat)
    {
        if(currentFormat.GetType() == typeof(Transform))
        {
            Transform t = currentFormat as Transform;

            assignedTargetHealth = t.GetComponentInParent<IHealth>();
        }
    }
    #endregion

    #region Private_Methods
    protected void Awake()
    {
        agentNavMeshH = GetComponent<NavMeshAgent>();
        _behaviourStun = GetComponent<StunBehaviour>();
        teamID = GetComponent<IdentifierTeam>();
        animator = GetComponentInChildren<Animator>();
        controlPain = GetComponentInChildren<PainController>();
    }

  

    protected void Update()
    {
        if(actionToDoNow != null)
        {            actionToDoNow.Invoke();
        }

        if(GameManager.managerGame.managerAnimation != null)
            GameManager.managerGame.managerAnimation.Animation_Float(animator, AnimationManager.movementNPCCharacter, agentNavMeshH.speed);
    }

    public void Disarmed()
    {
        _behaviourStun.iAmStunnedBool = true;
        _behaviourStun.kindStun = StunKind.Disarm;
    }

    public void ArmedWithAGun(WeaponScriptable gun)
    {
       
    }


    #endregion

    #region Static_Methods
    #endregion
}
