using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings_Resolution : MonoBehaviour
{
    [HideInInspector] public float fovChangeable;
    public Slider sliderFOV;
    private bool _waitUntilISaidBool;

    private void Start()
    {
        InitialValuesFov();
        InitialAspectRatio();
        _waitUntilISaidBool = true;
      //  DropDown_Activate();
    }
    public void InitialValuesFov()
    {
        fovChangeable = PlayerPrefs.GetFloat(SettingsManager.PLAYERPREF_FOVPlayer);

        ChangeTheFieldOfView(fovChangeable);
    }

    public void ChangeTheFieldOfView(float value)
    {

        fovChangeable = value;

        sliderFOV.value = value;
       
        if (!_waitUntilISaidBool) return;
        PlayerPrefs.SetFloat(SettingsManager.PLAYERPREF_FOVPlayer, value);
    }

    [SerializeField] private Resolutions[] currentResolutions;
    [SerializeField] private Dropdown dropDownAspectRatio;

    [HideInInspector] public bool fullScreenWindowBool;

    public void InitialAspectRatio()
    {
        DropDown_Activate();
        if (PlayerPrefs.GetInt(SettingsManager.PLAYERPREF_IsWindowed) == 0)
        {
            fullScreenWindowBool = true;
        }
        else
        {
            fullScreenWindowBool = false;

        }
        DropDown_Use(PlayerPrefs.GetInt(SettingsManager.PLAYERPREF_width_resolution) 
                                + " || " + PlayerPrefs.GetInt(SettingsManager.PLAYERPREF_height_resolution));

    }
    public void WindowedGame(bool isWindowedBool)
    {
        if (!isWindowedBool)
        {
            Screen.fullScreen = false;
            fullScreenWindowBool = false;

            PlayerPrefs.SetFloat(SettingsManager.PLAYERPREF_IsWindowed, 0);
        }
        else
        {
            Screen.fullScreen = true;
            fullScreenWindowBool = true;

            PlayerPrefs.SetFloat(SettingsManager.PLAYERPREF_IsWindowed, 1);
        }

    }
    public void DropDown_Activate()
    {
        List<string> stringsAboutResolutions = new List<string>();

        foreach (Resolutions res in currentResolutions)
        {
            stringsAboutResolutions.Add(res.width + " || " + res.height);
        }

        dropDownAspectRatio.AddOptions(stringsAboutResolutions);
    }

    public void DropDown_UseRemotely()
    {
        DropDown_Use(dropDownAspectRatio.options[dropDownAspectRatio.value].text);
    }
    public void DropDown_Use(string currentOption)
    {
        Resolutions _currentResolution = null;
        foreach (Resolutions res in currentResolutions)
        {
            if (currentOption == res.width + " || " + res.height)
            {
                _currentResolution = res;
                break;
            }

        }

        if (_currentResolution != null)
            Screen.SetResolution(_currentResolution.width, _currentResolution.height, Windowed(fullScreenWindowBool));
    }

    public FullScreenMode Windowed(bool modeScreenFull)
    {
        if (modeScreenFull)
        {
            PlayerPrefs.SetFloat(SettingsManager.PLAYERPREF_IsWindowed, 0);
            return FullScreenMode.FullScreenWindow;

        }
        else
        {
            PlayerPrefs.SetFloat(SettingsManager.PLAYERPREF_IsWindowed, 1);

            return FullScreenMode.Windowed;

        }
    }

    public void CurrentRes()
    {
        if (fullScreenWindowBool)
        {
            fullScreenWindowBool = false;
        }
        else {
            fullScreenWindowBool = true;
        } 
    }

}
