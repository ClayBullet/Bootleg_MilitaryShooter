using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFMODTesting : MonoBehaviour
{
	#region Fields
	private FMODUnity.StudioEventEmitter _studioEvent;
    #endregion

    #region Constructors
    #endregion

    #region Getters
    #endregion

    #region Setters
    #endregion

    #region Public_Methods

    [ContextMenu("PLAY SOUND")]
    public void PlaySound()
    {
        _studioEvent.Play();
    }
    #endregion

    #region Private_Methods

    private void Awake()
    {
        _studioEvent = GetComponent<FMODUnity.StudioEventEmitter>();
    }
    #endregion

    #region Static_Methods
    #endregion
}
