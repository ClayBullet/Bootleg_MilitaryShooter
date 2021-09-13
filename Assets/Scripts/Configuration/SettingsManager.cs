using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using UnityEngine.Events;

[DisallowMultipleComponent]
public class SettingsManager : MonoBehaviour
{

    [Header("CANVAS GAMEOBJECT ESCAPE")]
    [Space]
    [SerializeField] private GameObject canvasGameObject;





    public static string PLAYERPREF_sensibility = "CurrentSensibility";
    public static string PLAYERPREF_music = "MusicVFX";
    public static string PLAYERPREF_Effects = "EffectsVFX";
    public static string PLAYERPREF_FOVPlayer = "FOVPlayer";
    public static string PLAYERPREF_width_resolution = "ResolutionWidth";
    public static string PLAYERPREF_height_resolution = "ResolutionHeight";
    public static string PLAYERPREF_IsWindowed = "ResolutionWindowed";
    public AudioMixer audioMixer;



    #region SlidersForSettingsValues
    public Slider sliderValue;
    public Slider sliderMusic;
    public Slider sliderEffects;

    #endregion

    private bool _waitUntilISaidBool;

    public Settings_Player playerSettings;

    private void Awake()
    {
        GameManager.managerGame.managerSettings = this;
    }

    private void Start()
    {


        audioMixer.SetFloat(PLAYERPREF_Effects, PlayerPrefs.GetFloat(PLAYERPREF_Effects));
        audioMixer.SetFloat(PLAYERPREF_music, PlayerPrefs.GetFloat(PLAYERPREF_music));

      

        //InitialAspectRatio();
        //InitialValuesFov();         
        _waitUntilISaidBool = true;
    }

 

  

  
    public void ResumeButton()
    {
        canvasGameObject.SetActive(false);
        GameManager.managerGame.hiddenCursor.CursorIsVisible(false);
        GameManager.managerGame.managerTime.RecoverTimeScale();
    }

    public void Quit()
    {
        Application.Quit();
    }

 


    

    #region PauseGame

    private bool pauseGameBool;


    public void PauseTheGame()
    {
        if (pauseGameBool)
        {
            GameManager.managerGame.managerTime.FreezeTimeScale();
            pauseGameBool = false;
        }
        else
        {
            GameManager.managerGame.managerTime.RecoverTimeScale();
            pauseGameBool = true;

        }

    }

    #endregion


    #region AspectRatio
   
   

    #endregion

    #region QuitOrResetGame

    public void QuitGame()
    {

        Application.Quit();
    }

    public void ResetGame()
    {

        string actualScene = SceneManager.GetActiveScene().name;

        SceneManager.LoadScene(actualScene);
    }

    public void ChargeCurrentLevel(string name)
    {
        SceneManager.LoadScene(name);
    }

    #endregion


    #region IndexCanvas

    [SerializeField] private GameObject indexSound;
    [SerializeField] private GameObject indexControls;
    [SerializeField] private GameObject indexAspectRatio;
    [SerializeField] private GameObject indexMain;
    [HideInInspector] public bool isActivateTheEscapeBool;

    public void ActivateIndex()
    {
        if (isActivateTheEscapeBool)
        {
            indexMain.SetActive(false);
            isActivateTheEscapeBool = false;
        }
        else
        {
            indexMain.SetActive(true);
            isActivateTheEscapeBool = true;
        }
    }


    public void EnaOrDisa(GameObject target)
    {
        if (target.activeSelf)
            target.SetActive(false);
        else
            target.SetActive(true);
    }
    #endregion
}
[System.Serializable]
public class Resolutions
{
    public int width;
    public int height;
    
    public Resolutions(int _width, int _height)
    {
        _width = width;
        _height = height;
    }
}