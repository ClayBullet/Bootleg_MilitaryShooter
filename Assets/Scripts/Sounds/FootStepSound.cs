using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class FootStepSound : MonoBehaviour
{
    [SerializeField] private float rayLength;
    [SerializeField] private AudioClip clipFootMetal;
    [SerializeField] private AudioSource sourceFootStepAudio;
    [SerializeField] private float maxDelayBetweenSteps;
    [SerializeField] private LayerMask layerForTouch;
    private float delayBetweenSteps;
    public void Movement()
    {
        RaycastHit hit;

        if (Physics.Linecast(transform.position, Vector3.down * maxDelayBetweenSteps, out hit, layerForTouch))
        {
            if (hit.collider.CompareTag("Floor"))
            {
                delayBetweenSteps += Time.deltaTime;
                if(delayBetweenSteps > maxDelayBetweenSteps)
                {
                    GameManager.managerGame.managerSound.SoundClip(clipFootMetal, sourceFootStepAudio, 1f, .75f, .85f, false, false);
                    delayBetweenSteps = 0;
                }
            }
        }
    }
}
