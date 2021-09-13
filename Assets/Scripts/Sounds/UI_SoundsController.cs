using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class UI_SoundsController : MonoBehaviour
{
    [SerializeField] private AudioSource audioUI;

    public void PlayClip(AudioClip clip)
    {
        GameManager.managerGame.managerSound.SoundClip(clip, audioUI, 1f, .75f, .85f, false, false);
    }
}
