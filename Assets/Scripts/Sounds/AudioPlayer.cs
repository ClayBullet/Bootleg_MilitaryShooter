using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class AudioPlayer : MonoBehaviour
{
    [Header("AUDIO PLAYER")]
    [Space]
    public AudioSource audioPlayer;
    public AudioSource audioGun;
    [Header("CLIP")]
    [Space]
    public AudioClip clipJump;
    public AudioClip clipFall;
    public AudioClip clipDeath;
}
