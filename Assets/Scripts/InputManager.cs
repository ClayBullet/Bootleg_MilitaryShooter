using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InputManager : MonoBehaviour
{

    public InputScriptable input;


    #region ActionsForDo
    public Action up_Axis_Move, down_Axis_Move, free_Horizontal_Axis, right_Axis_Move, left_Axis_Move, freeze_Y_Axis, free_Vertical_Axis;
    public Action<float, float> movementAxis;
    public Action<float, float> cameraAxis;

    public Action OnPressed_JumpButton, UpPress_JumpButton, DownPress_JumpButton;

    public Action OnPressed_FireButton, UpPress_FireButton, DownPress_FireButton;

    public Action OnPressed_AimButton, UpPress_AimButton, DownPress_AimButton;

    public Action OnPressed_ReloadButton, UpPress_ReloadButton, DownPress_ReloadButton;

    public Action OnPressed_ThrowButton, UpPress_ThrowButton, DownPress_ThrowButton;

    public Action OnPressed_AlternativeThrowButton, UpPress_AlternativeThrowButton, DownPress_AlternativeThrowButton;

    public Action OnPressed_SprintBtn, UpPress_SprintBtn, DownPress_SprintBtn;

    public Action DownPress_SwitchButton, UpPress_SwitchButton, OnPressed_SwitchButton;

    public Action DownPress_DashButton, UpPress_DashButton, OnPressed_DashButton;

    public Action DownPress_EscapeButton, UpPress_EscapeButton, OnPressed_EscapeButton;

    public Action DownPress_RewindButton;

    public Action specialEventForTake;


    public Action OnPressed_RegularAction, UpPress_RegularAction, DownPress_RegularAction;
    public Action OnPressed_AltAction, UpPress_AltAction, DownPress_AltAction;



    #endregion

    public bool disableInputsBool;

    public bool concretActionsForRealizeBool;

    public Action keepActionsBeforeTheSpecialEvent;

    private Action _removableSpecialEvent;
    private Action _removableActionWhenIsFinished;
    private Special_Events _evtSpecial;

    public float adjustValueGamePad;
    public float adjustValueMouse;

    [SerializeField] private float multiplySenMouse;

    private void Awake()
    {
        GameManager.managerGame.managerInput = this;
    }
 

    private void Update()
    {


        if (!disableInputsBool)
        {
            Character_Movement();
            Camera_Axis();
            Jump_Character();

            Shoot_Action();
            Reload_Action();
            //Grabble_Action();
            Sight_Action();
            Throw_Action();
            Grabble_Action();
            Sprint_Action();
            AlternativeThrowableAction();

        }
        else if (concretActionsForRealizeBool)
        {
            if (specialEventForTake != null)
                specialEventForTake.Invoke();
        }


        if (!concretActionsForRealizeBool)
            Escape_Action_Down();


    }

    private void AlternativeThrowableAction()
    {
        if (input.alternativeThrowableButton.isRegisterTheCurrentActionBool(EntryRegister.Key_Down))
        {
            DownPress_AlternativeThrowButton.Invoke();
        }
        else if (input.alternativeThrowableButton.isRegisterTheCurrentActionBool(EntryRegister.Key_UP))
        {
            UpPress_AlternativeThrowButton.Invoke();
        }
    }

    private void Sprint_Action()
    {
        if (input.sprintBtn.isRegisterTheCurrentActionBool(EntryRegister.Key_Down))
        {
            DownPress_SprintBtn.Invoke();
        }
        else if (input.sprintBtn.isRegisterTheCurrentActionBool(EntryRegister.Key_UP))
        {
            UpPress_SprintBtn.Invoke();
        }
    }

    private void AlternativeAttack_Action()
    {
        if (input.hitAltButton.isRegisterTheCurrentActionBool(EntryRegister.Key_Down))
        {
            DownPress_AltAction.Invoke();
        }
        else if (input.hitAltButton.isRegisterTheCurrentActionBool(EntryRegister.Key_UP))
        {
            UpPress_AltAction.Invoke();
        }
    }

    private void RegularAttack_Action()
    {
        if (input.hitButton.isRegisterTheCurrentActionBool(EntryRegister.Key_Down))
        {
            DownPress_RegularAction.Invoke();
        }
    }

    private void Dash_Action()
    {
        if (input.dashButton.isRegisterTheCurrentActionBool(EntryRegister.Key_Down))
        {
            DownPress_DashButton.Invoke();
        }


        //if (input.jumpBtn.isRegisterTheCurrentActionBool(EntryRegister.Key_UP))
        //{
        //    UpPress_JumpButton.Invoke();
        //}
    }

    public void Camera_Axis()
    {   
        cameraAxis.Invoke(input.axisCameraX.registerAxis(), input.axisCameraY.registerAxis());
    }

    public void Character_Movement()
    {
        if (input.axisMovementX.isRegisterTheCurrentActionBool(EntryRegister.Key_UP)) right_Axis_Move.Invoke();
        else if (input.axisMovementX.isRegisterTheCurrentActionBool(EntryRegister.Key_Down)) left_Axis_Move.Invoke();

        if (input.axisMovementY.isRegisterTheCurrentActionBool(EntryRegister.Key_UP)) down_Axis_Move.Invoke();
        else if (input.axisMovementY.isRegisterTheCurrentActionBool(EntryRegister.Key_Down)) up_Axis_Move.Invoke();

        if(movementAxis != null)
        {

            movementAxis.Invoke(Mathf.RoundToInt(input.axisMovementX.registerAxis()), 
                                Mathf.RoundToInt(input.axisMovementY.registerAxis()));
        }
    }

    public void Jump_Character()
    {
        if (input.jumpBtn.isRegisterTheCurrentActionBool(EntryRegister.Key_Down))
        {
            DownPress_JumpButton.Invoke();
        }
       

        //if (input.jumpBtn.isRegisterTheCurrentActionBool(EntryRegister.Key_UP))
        //{
        //    UpPress_JumpButton.Invoke();
        //}

    }

    public void Reload_Action()
    {
        if (input.reloadButton.isRegisterTheCurrentActionBool(EntryRegister.Key_Down))
        {
            DownPress_ReloadButton.Invoke();
        }
    }

    public void Throw_Action()
    {
        if (input.throwableButton.isRegisterTheCurrentActionBool(EntryRegister.Key_Down))
        {
            DownPress_ThrowButton.Invoke();
        }
    }

    public void Shoot_Action()
    {

        if (input.fireButton.isRegisterTheCurrentActionBool(EntryRegister.Key_Down))
        {
            DownPress_FireButton.Invoke();
        }
    }

    public void Grabble_Action()
    {
        if (input.switchButton.isRegisterTheCurrentActionBool(EntryRegister.Key_Down))
        {
            DownPress_SwitchButton();
        }
    }

  
    public void Sight_Action()
    {
        if (input.aimButton.isRegisterTheCurrentActionBool(EntryRegister.Key_Down))
        {
            DownPress_AimButton.Invoke();
        }
        else if (input.aimButton.isRegisterTheCurrentActionBool(EntryRegister.Key_UP))
        {
            UpPress_AimButton.Invoke();
        }
    }

    public void Escape_Action_Down()
    {
        if (input.escapeButton.isRegisterTheCurrentActionBool(EntryRegister.Key_Down))
        {
            DownPress_EscapeButton.Invoke();
        }
    }

    public void ChangeWeapon()
    {
        if (input.switchButton.isRegisterTheCurrentActionBool(EntryRegister.Key_Down))
        {
            DownPress_SwitchButton.Invoke();
        }
    }

    public void ButtonsAndActionsAvailableForSpecialActions(Special_Events eventForDo, Action act)
    {
        disableInputsBool = true;
        concretActionsForRealizeBool = true;
        _removableActionWhenIsFinished = act;
        switch (eventForDo)
        {
            case Special_Events.ATTACK_BUTTON:
                specialEventForTake += Shoot_Action;
                DownPress_FireButton += act;
                _removableSpecialEvent += Shoot_Action;
                break;
            case Special_Events.JUMP_SPECIAL:
                specialEventForTake += Jump_Character;
                DownPress_JumpButton += act;
                _removableSpecialEvent += Jump_Character;
                break;
            case Special_Events.REWIND_BUTTON:
                specialEventForTake += ChangeWeapon;
                DownPress_SwitchButton += act;
                _removableSpecialEvent += ChangeWeapon;
                break;
        
        }
    }

    public void ClearTheButtons()
    {
        specialEventForTake -= _removableSpecialEvent;

        switch (_evtSpecial)
        {
            case Special_Events.JUMP_SPECIAL:
                DownPress_JumpButton -= _removableActionWhenIsFinished;

                break;
            case Special_Events.REWIND_BUTTON:
                DownPress_SwitchButton -= _removableActionWhenIsFinished;
                break;
            case Special_Events.ATTACK_BUTTON:
                DownPress_FireButton -= _removableActionWhenIsFinished;
                break;
            case Special_Events.BACK_BUTTON:
                DownPress_RewindButton -= _removableActionWhenIsFinished;
                break;
        }

        concretActionsForRealizeBool = false;
        disableInputsBool = false;
    }


}

[System.Serializable]
public class InputActions
{
    public string nameAction;
    public KeyCode key_KeyBoard;
    public string axis_KeyBoard;
    public KeyCode key_GamePad;
    public string axis_GamePad;
    public bool isAnAxisBool;
    public float finalNumberToAxis;
    public EntryRegister registerEntry;
    public bool isPassDownBool;

    public bool isAGamePadControllerBool()
    { 
        if(Input.GetKeyDown(key_KeyBoard) || Input.GetAxis(axis_KeyBoard) != 0 || Input.GetKeyUp(key_KeyBoard) || Input.GetKey(key_KeyBoard))
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public bool isRegisterTheCurrentActionBool(EntryRegister register)
    {
      

        if (!isAnAxisBool)
        {
            if (!isAGamePadControllerBool())
            {

                switch (register)
                {
                    case EntryRegister.Key_Down:
                        isPassDownBool = true;
                        return Input.GetKeyDown(key_KeyBoard);
                    case EntryRegister.Key_Stay:
                        return Input.GetKey(key_KeyBoard);
                    case EntryRegister.Key_UP:
                        return Input.GetKeyUp(key_KeyBoard);

                }
            }
            else
            {

                switch (register)
                {
                    case EntryRegister.Key_Down:
                        return Input.GetKeyDown(key_GamePad);
                    case EntryRegister.Key_Stay:
                        return Input.GetKey(key_GamePad);
                    case EntryRegister.Key_UP:
                        return Input.GetKeyUp(key_GamePad);
                }

            }
           
        }
        else
        {
            if (!isAGamePadControllerBool())
            {

                switch (register)
                    {
                    case EntryRegister.Key_Down:
                        return Input.GetAxis(axis_KeyBoard) < -finalNumberToAxis;
                    case EntryRegister.Key_Stay:
                        return Input.GetAxis(axis_KeyBoard) == 0;
                    case EntryRegister.Key_UP:
                        return Input.GetAxis(axis_KeyBoard) > finalNumberToAxis;
                    }
            }
            else
            {

                switch (register)
                {
                    case EntryRegister.Key_Down:
                        return Input.GetAxis(axis_GamePad) < -finalNumberToAxis;
                    case EntryRegister.Key_Stay:
                        return Input.GetAxis(axis_GamePad) == 0;
                    case EntryRegister.Key_UP:
                        return Input.GetAxis(axis_GamePad) > finalNumberToAxis;
                }
            }

            
        }

        return false;
    }

    public float registerAxis()
    {
        if (isAGamePadControllerBool())
        {
            return Input.GetAxis(axis_GamePad);
        }
        else
        {
            if (axis_KeyBoard != "")
                return Input.GetAxis(axis_KeyBoard);
            else
            {
                return Input.GetKeyDown(key_KeyBoard) ? 0 : 1;

            }
                
        }

    }

}
public enum EntryRegister { Key_UP, Key_Down, Key_Stay}

public enum InputAxis { Cross_Vertical, Cross_Horizontal, JoystickRight_Vertical, JoystickRight_Horizontal, JoystickLeft_Horizontal, JoystickLeft_Vertical }
public enum Special_Events { JUMP_SPECIAL, ATTACK_BUTTON, REWIND_BUTTON, BACK_BUTTON }