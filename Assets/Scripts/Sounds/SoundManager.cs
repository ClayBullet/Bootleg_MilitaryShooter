using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
[DisallowMultipleComponent]
public class SoundManager : MonoBehaviour {

   
    public AudioMixer mixerAudio;

    private const string musicTriggerPref = "MusicTrigger";
    private const string effectsTriggerPref = "EffectsTrigger";


    [Header("GENERAL SOUNDS")]
    [Space]
    public AudioClip clipExplossion;
    public AudioSource ambienceAudioSource;
    private void Awake()
    {
        GameManager.managerGame.managerSound = this;

    }

    private void Start()
    {
        SceneManager.sceneLoaded += AccessToTheManagerSound;
    }
   

    private void AccessToTheManagerSound(Scene scn, LoadSceneMode modeSceneLoad)
    {
        GameManager.managerGame.managerSound = this;

    }
    public void SoundClip(AudioClip clipAudio, AudioSource sourceAudio, float volume, float minPitch, float maxPitch, bool isLoop, bool isPlayBool)
    {
       
            sourceAudio.Stop();
            sourceAudio.clip = clipAudio;
            sourceAudio.loop = isLoop;
            sourceAudio.pitch = ExtensionMethods.GaussianStyleForPitchSound(minPitch, maxPitch) + .20f;
            sourceAudio.volume = volume;
            sourceAudio.Play();
        
         
       

    }

    public void Music_EnableDisable()
    {
        if (PlayerPrefs.GetInt(musicTriggerPref) == 1)
        {
            mixerAudio.SetFloat("MusicVolume", -80f);
            PlayerPrefs.SetInt(musicTriggerPref, 0);
        }
        else
        {
            mixerAudio.SetFloat("MusicVolume", 20f);
            PlayerPrefs.SetInt(musicTriggerPref, 1);

        }
    }

    public void Effects_EnableDisable()
    {
        if (PlayerPrefs.GetInt(effectsTriggerPref) == 1)
        {
            mixerAudio.SetFloat("EffectsVolume", -80f);
            PlayerPrefs.SetInt(effectsTriggerPref, 0);
        }
        else
        {
            mixerAudio.SetFloat("EffectsVolume", 20f);
            PlayerPrefs.SetInt(effectsTriggerPref, 1);

        }
    }
  
   
    public void EffectsManager(float musicFloat)
    {
        float music = Mathf.Log10(musicFloat) * 20;
        mixerAudio.SetFloat("EffectsVolume", music);
        //  PlayerPrefs.GetFloat("MusicManage", music);
    }
   
}
