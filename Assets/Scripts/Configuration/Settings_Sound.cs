using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
public class Settings_Sound : MonoBehaviour
{
    public Slider sliderMusic;
    public Slider sliderEffects;

    public static string PLAYERPREF_Music_SLIDER = "MusicSliderValue";
    public static string PLAYERPREF_Effects_SLIDER = "EffectsSliderValue";
    private const string musicName = "MusicVFX";
    private const string effectsName = "EffectsVFX";
    private bool _waitUntilISaidBool;

    private void Start()
    {
        sliderMusic.value = PlayerPrefs.GetFloat(PLAYERPREF_Music_SLIDER);
        sliderEffects.value = PlayerPrefs.GetFloat(PLAYERPREF_Effects_SLIDER);
        MusicVfx(PlayerPrefs.GetFloat(PLAYERPREF_Music_SLIDER));
        EffectsVfx(PlayerPrefs.GetFloat(PLAYERPREF_Effects_SLIDER));
        _waitUntilISaidBool = true;

    }

    public void MusicVfx(float value)
    {

        float soundLogaritmic = Mathf.Log10(value) * 20;
        GameManager.managerGame.managerSettings.audioMixer.SetFloat(musicName, soundLogaritmic);
        sliderMusic.value = value;
        if (!_waitUntilISaidBool) return;

        PlayerPrefs.SetFloat(SettingsManager.PLAYERPREF_music, soundLogaritmic);

        PlayerPrefs.SetFloat(PLAYERPREF_Music_SLIDER, value);


    }
    public void EffectsVfx(float value)
    {

        float soundLogaritmic = Mathf.Log10(value) * 20;
        GameManager.managerGame.managerSettings.audioMixer.SetFloat(effectsName, soundLogaritmic);
        sliderEffects.value = value;
        if (!_waitUntilISaidBool) return;

        PlayerPrefs.SetFloat(SettingsManager.PLAYERPREF_Effects, soundLogaritmic);
        PlayerPrefs.SetFloat(PLAYERPREF_Effects_SLIDER, value);

    }


}
