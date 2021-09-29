using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FModTesting : MonoBehaviour
{
	#region Fields
	private FMODUnity.StudioEventEmitter _studioEventEmitter;
    private FMOD.Studio.Bus _sfxBus;
    private FMOD.Studio.Bus _musicBus;
    [FMODUnity.EventRef]
    public string fmodEvent;

    private FMOD.Studio.EventInstance instance;

    [SerializeField] [Range(0, 1)] private float currentVolumeFromSFX;
    [SerializeField] [Range(0, 1)] private float currentVolumeFromMusic;
    #endregion

    #region Constructors
    #endregion

    #region Getters
    #endregion

    #region Setters
    #endregion

    #region Public_Methods
    #endregion

    #region Private_Methods

    private void Awake()
    {
        _studioEventEmitter = GetComponent<FMODUnity.StudioEventEmitter>();
    }

    private void Start()
    {
        instance = FMODUnity.RuntimeManager.CreateInstance(fmodEvent);
        instance.start();
        
        _sfxBus = FMODUnity.RuntimeManager.GetBus("bus:/SFX");
        _musicBus = FMODUnity.RuntimeManager.GetBus("bus:/VO");
        _musicBus.setVolume(currentVolumeFromMusic);
        StartCoroutine(ReproduceCurrentAudio_Coroutine());

    }
    private IEnumerator ReproduceCurrentAudio_Coroutine()
    {
        FMOD.ChannelGroup groupChannel;
        
        string takeName;
        while (true)
        {
            
            yield return new WaitForSeconds(.5f);
            _studioEventEmitter.Params[0].Value = 1;
            instance.getChannelGroup(out groupChannel);
            groupChannel.getName(out takeName, 10);
            _studioEventEmitter.Play();
            _sfxBus.setVolume(currentVolumeFromSFX);
          
            
        }
    }
    #endregion

    #region Static_Methods
    #endregion
}
