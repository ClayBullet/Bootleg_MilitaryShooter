using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    #region Fields

    #region PlayerAnimations
    public static readonly string movementPlayerAnimation = "SpeedCharacter";
    public static readonly string jumpPlayerAnimation = "JumpPlayer_Bool";
    public static readonly string fallingPlayerAnimation = "LandingPlayer_Bool";
    public static readonly string dashPlayerAnimation = "Dash_Bool";
    public static readonly string attackPlayerAnimation = "Attack_Bool";
    public static readonly string comboPlayerAnimation = "Combo_Floor";
    public static readonly string irisUnleashPlayerAnimation = "IrisMode";
    public static readonly string anotherHappyLanding = "JumpLanding";
    public static readonly string doubleJumpPlayerAnimation = "DoubleJumpBool";
    public static readonly string projectionMirrorAnimation = "IsAffectForTheIrisProjectionBool";
    public static readonly string projectionSpeedMirrorAnimation = "Explode_Animation";
    public static readonly string floorModeAnimation = "IsInFloorBool";
    public static readonly string stateAnimationJumpDescending = "Jump_Descending";

    public static readonly string isStunnedBoolean = "isStunnedBool";
    public static readonly string isRecoveringBool = "isRecoveringBool";

    public static readonly string stateMovementBlendPlayerHorizontal = "X_Movement";
    public static readonly string stateMovementBlendPlayerVertical = "Y_Movement";

    #endregion

    #region NPCAnimations
    public static readonly string movementNPCCharacter = "SpeedMovement";
    public static readonly string punhcingNPC = "IsPunchingBool";
    #endregion
    public static readonly string headTouched = "Damage_Head";
    public static readonly string bodyTouched = "Damage_Body";
    public static readonly string armRightTouched = "Damage_RightArm";
    public static readonly string armLeftTouched = "Damage_LeftArm";
    public static readonly string legsTouched = "Damage_Legs";
    public static readonly string isDamageable = "isDamage";


    #endregion

    #region Constructors
    #endregion

    #region Getters
    #endregion

    #region Setters
    #endregion

    #region Public_Methods
    public void Animation_Float(Animator animator, string value, float f)
    {
        if (animator != null)
            animator.SetFloat(value, f);
    }

    public void Animation_Bool(Animator animator, string value, bool boolean)
    {
        if (animator != null)
        {
            animator.SetBool(value, boolean);
        }
    }

    public void Animation_Int(Animator animator, string value, int integer)
    {
        if (animator != null)
            animator.SetInteger(value, integer);
    }

    public void AnimationModifierSpeed(Animator animator, string value, float speed)
    {
        animator.SetFloat(value, speed);
    }

    public void Animation_Trigger(Animator animator, string value)
    {
        animator.SetTrigger(value);
    }
    public void AnimationCombatLogic(Animator animator, string value)
    {
        animator.SetBool(value, true);
    }
   
    public bool IsTheCurrentStateAnimatorBool(Animator anim, string value)
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName(value))
        {
            return true;
        }

        return false;
    }
    #endregion

    #region Private_Methods
    private void Awake()
    {
        GameManager.managerGame.managerAnimation = this;
    }
    #endregion

    #region Static_Methods
    #endregion
}
