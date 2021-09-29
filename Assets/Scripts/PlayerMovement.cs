using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class PlayerMovement : MonoBehaviour
{
    [Header("SPEED MOVEMENT")]
    [Space]
    [SerializeField] protected float speedMovement;

    [Header("JUMP SYSTEM")]
    [Space]
    [SerializeField] protected float forceJump;
    [SerializeField] protected float speedJump;
    [SerializeField] protected float delayForAscending;
    [SerializeField] protected Transform checkHeadJump;
    [SerializeField] protected float distanceRay;
    [SerializeField] protected float coyoteTime;
    [SerializeField] protected float delayBetweenJumpsExecuted = 0.2f;
    [Header("DOUBLE JUMP")]
    [Space]
    [SerializeField] protected float forceJumpDouble;
    public bool doubleJumpBool;

    [Header("FALL SYSTEM")]
    [Space]
    [SerializeField] protected float speedFall;
    [SerializeField] protected Transform checkFeetFall;
    [SerializeField] protected float distanceRayInFall;
    [SerializeField] protected float timeFall;
    [SerializeField] protected float delayForLanding;
    public bool isInFloorBool;

    [SerializeField] protected LayerMask maskLayerRayCheck;

    [Header("CROUCH SYSTEM")]
    [Space]
    [SerializeField] protected Transform cameraPosition;
    [SerializeField] protected Transform headTransform;
    [SerializeField] protected Transform crouchHeadTransform;

    [Header("SPRINT SYSTEM")]
    [Space]
    [SerializeField] protected float incrementSpeed;

   
    protected bool crouchBool;

    protected Rigidbody rigibBodyPlayer;

    protected float velocityPlayer;
    [SerializeField] protected Animator playerAnimator;

    protected bool _isFallingBool;
    protected bool _isJumpBool;

    [HideInInspector] public bool unamovibleRightBool, unamovibleLeftBool, unamovibleForward, unamovibleBackWard;

    
    [Header("CAN USE THE ZIPLINE?")]
    [Space]
    public bool availableForUseTheZiplineBool;

    protected bool _breakFallEffectBool;

    protected bool _delayForCheckNewJumping;

    private bool _isMovingFowardBool, _isMovingBackBool, _isMovingRightBool, _isMovingLeftBool;
    private bool _isCurrentPutTheSprint;

    protected void Awake()
    {
        rigibBodyPlayer = GetComponent<Rigidbody>();
    }


    protected void Start()
    {
        doubleJumpBool = true;
        isInFloorBool = true;
        CreateDefaultActions();
    }

    protected void CreateDefaultActions()
    {
       
        GameManager.managerGame.managerInput.up_Axis_Move += MoveForward;
        GameManager.managerGame.managerInput.up_Axis_Move += FootStep;
        GameManager.managerGame.managerInput.down_Axis_Move += MoveBackWard;
        GameManager.managerGame.managerInput.down_Axis_Move += FootStep;
        GameManager.managerGame.managerInput.right_Axis_Move += MoveRight;
        GameManager.managerGame.managerInput.right_Axis_Move += FootStep;
        GameManager.managerGame.managerInput.left_Axis_Move += MoveLeft;
        GameManager.managerGame.managerInput.left_Axis_Move += FootStep;
        GameManager.managerGame.managerInput.DownPress_JumpButton += JumpCharacter;
        GameManager.managerGame.managerInput.DownPress_SprintBtn += SprintEnable;
        GameManager.managerGame.managerInput.UpPress_SprintBtn += SprintDisable;
        GameManager.managerGame.managerInput.free_Horizontal_Axis += StopRight;
        GameManager.managerGame.managerInput.free_Horizontal_Axis += StopLeft;
        GameManager.managerGame.managerInput.free_Vertical_Axis += StopForward;
        GameManager.managerGame.managerInput.free_Vertical_Axis += StopBackward;
    }


    


    protected void Update()
    {
           MovementAxis();
 
        if (!lowerThanMyFeetsBool() && !_isFallingBool && isInFloorBool) FallDescent();
 
    }



    #region MovementAxis

 
    private void SprintEnable()
    {
        if(!_isMovingBackBool && !_isMovingLeftBool && !_isMovingRightBool && _isMovingFowardBool)
        {
            speedMovement += incrementSpeed;
            _isCurrentPutTheSprint = true;
        }    
    }

    private void SprintDisable()
    {
        if (_isCurrentPutTheSprint)
        {
            speedMovement -= incrementSpeed;
            _isCurrentPutTheSprint = false;
        }
           
    }

    protected virtual void MoveRight() {
            transform.Translate(transform.right * Time.deltaTime * speedMovement, Space.World);
            _isMovingRightBool = true;
    }

    protected virtual void StopRight() { _isMovingRightBool = false;   }
    protected virtual void MoveLeft() {
            transform.Translate(-transform.right * Time.deltaTime * speedMovement, Space.World);
        _isMovingLeftBool = true;
    }
    protected virtual void StopLeft(){ _isMovingLeftBool = false; }
    protected void MoveForward()
    {
            transform.Translate(transform.forward * Time.deltaTime * speedMovement, Space.World);
        _isMovingFowardBool = true;
        
    }
    protected virtual void StopForward(){ _isMovingFowardBool = false;    }
    protected void MoveBackWard()
    {
         transform.Translate(-transform.forward * Time.deltaTime * speedMovement, Space.World);
        _isMovingBackBool = true;
    }

    protected virtual void StopBackward(){ _isMovingBackBool = false; }

    protected void FootStep()
    {
           // GameManager.managerGame.stepSoundFoot.Movement();
    }
    protected void MovementAxis()
    {
       

       
    
     //  GameManager.managerGame.managerAnimation.PlayerAnimationFloat(GameManager.managerGame.managerAnimation.animWalkPlayer, playerVelocity.velocityCharacter(), animatorP);
       
       


    }
    #endregion

    #region JumpCharacter

    protected void JumpCharacter()
    {

        if (isInFloorBool && lowerThanMyFeetsBool())
        {
            StartCoroutine(JumpAscending(Vector3.up, forceJump, speedJump));
        }
        //else if (doubleJumpBool)
        //{
        //    StartCoroutine(JumpAscending(Vector3.up, forceJumpDouble, speedJump, true));

        //}
    }

    protected IEnumerator JumpAscending(Vector3 vectorToGo, float force, float speed, bool isTheSecondJumpBool = false)
    {
        if (_delayForCheckNewJumping) { 
                
            yield break;
        } 
        _delayForCheckNewJumping = true;

        StartCoroutine(DelayForOverJumpAgain_Coroutine());

        if (!isInFloorBool)
            doubleJumpBool = false;

        isInFloorBool = false;

        rigibBodyPlayer.useGravity = false;

        _breakFallEffectBool = true;

       // GameManager.managerGame.managerSound.SoundClip(playerAudio.clipJump, playerAudio.audioPlayer, 1f, .75f, .85f, false, false);

        for (float i = 0; i < force; i++)
        {
            yield return new WaitForEndOfFrame();

            if (overMyHeadMadeIStopBool())
            {
                break;
            }

            

            if(!doubleJumpBool && !isTheSecondJumpBool)
            {
                yield break;
            }

            transform.Translate(vectorToGo * speed * Time.fixedDeltaTime, Space.World);


        }

        _breakFallEffectBool = false;

         StartCoroutine(FallDescending());
    }
    protected void FallDescent()
    {
        if(!reasonToStopFallBool())
            StartCoroutine(FallDescending());

    }
    protected IEnumerator FallDescending()
    {
        _isFallingBool = true;
        isInFloorBool = false;

        rigibBodyPlayer.useGravity = false;
        yield return new WaitForSecondsRealtime(coyoteTime);
        while (!lowerThanMyFeetsBool())
        {


            if (_breakFallEffectBool) {
                yield break;
            } 
            
           yield return new WaitUntil(() =>  Time.timeScale != Mathf.Epsilon);

            yield return new WaitForEndOfFrame();
            transform.Translate(Vector3.down * speedFall * Time.fixedDeltaTime, Space.World);

            
            
        }
        //GameManager.managerGame.managerSound.SoundClip(playerAudio.clipFall, playerAudio.audioPlayer, 1f, .75f, .85f, false, false);

        rigibBodyPlayer.useGravity = true;

        yield return new WaitForSecondsRealtime(delayForLanding);
        isInFloorBool = true;

        doubleJumpBool = true;
        _isFallingBool = false;

    }

    protected IEnumerator DelayForOverJumpAgain_Coroutine()
    {
        yield return new WaitForSeconds(delayBetweenJumpsExecuted);
        _delayForCheckNewJumping = false;
    }

    protected bool overMyHeadMadeIStopBool()
    {
        RaycastHit hit;
        if (Physics.Linecast(transform.position, checkHeadJump.transform.position, out hit, maskLayerRayCheck))
        {
            return true;
        }

        return false;
    }

    public bool lowerThanMyFeetsBool()
    {
        RaycastHit hit;
        if (Physics.Linecast(transform.position, checkFeetFall.transform.position, out hit, maskLayerRayCheck))
        {
            if (hit.collider.gameObject != this.gameObject)
                return true;
            else
                return false;
        }
        else
            return false;
    }

    #endregion

    #region CrouchSystem

    protected void CrouchSystem()
    {
        if (crouchBool)
        {
            ChangeCameraHeight(crouchHeadTransform);
            crouchBool = false;
        }
        else
        {
            ChangeCameraHeight(headTransform);

            crouchBool = true;

        }
    }


    protected void ChangeCameraHeight(Transform currentTransform)
    {
        cameraPosition.position = currentTransform.position;
    }

    public virtual bool reasonToFallBool()
    {
        return false;
    }

   

    public virtual bool reasonToStopFallBool()
    {
        return _breakFallEffectBool;
    }

    
    #endregion
}
