using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ProjectileSound : MonoBehaviour
{
    [HideInInspector]public AudioSource sourceAudio;

    private void Awake()
    {
        if (sourceAudio == null)
            sourceAudio = GetComponent<AudioSource>();
    }

    
    public void IfInvokeForWeapon(WeaponScriptable gun)
    {
        GameManager.managerGame.managerSound.SoundClip(gun.clipHit, sourceAudio, 1f, .75f, .85f, false, false);

    }
}
