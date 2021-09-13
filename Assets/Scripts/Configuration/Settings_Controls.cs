using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Settings_Controls : MonoBehaviour
{
    private bool _waitUntilISaidBool;
    public Slider sliderSensibility;
    public Text toggleText;
    public GameObject toggleButton;
  //  public InstructionsControls controlsInstructions;
    private bool _controlBool;
    private void Start()
    {
        _controlBool = PlayerPrefs.GetInt("UseGamePad") == 0;
        ChangeCurrentScriptableInput();
            ModifySensibility(PlayerPrefs.GetFloat(SettingsManager.PLAYERPREF_sensibility));
        _waitUntilISaidBool = true;

      
    }

    public void ModifySensibility(float sen)
    {
       

        if(sliderSensibility != null)
            sliderSensibility.value = sen;

        if (!_waitUntilISaidBool) return;

        PlayerPrefs.SetFloat(SettingsManager.PLAYERPREF_sensibility, sen);
    }

 
    public void ChangeCurrentScriptableInput()
    {
        if (_controlBool)
        {

            

            toggleButton.SetActive(true);
            toggleText.text = "USE GAMEPAD";

            PlayerPrefs.SetInt("UseGamePad", 0);

            _controlBool = false;
        }
        else
        {
           

            toggleButton.SetActive(false);
            toggleText.text = "USE KEYBOARD";

            PlayerPrefs.SetInt("UseGamePad", 1);

            _controlBool = true;


        }

        //if(GameManager.managerGame.managerPlayer != null)
        //{
        //    GameManager.managerGame.managerPlayer.ControlCanvas(GameManager.managerGame.managerInput.input.isAGamePadBool);
        //}
    }
}
