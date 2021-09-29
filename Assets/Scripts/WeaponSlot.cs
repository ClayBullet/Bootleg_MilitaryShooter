using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSlot : MonoBehaviour
{
    public Image imageGun;
    public WeaponScriptable gunScriptable;
    [SerializeField] private Transform gunPivot;
    [SerializeField] private ParticleSystem sysParticle;
    //[SerializeField] private GameObject emptyShell;
    public GameObject currentOwner;
    public Light currentLight;
    private Animator _animator;
    public readonly string attackAnim = "Attack";
    public readonly string aimAnim = "Aim";
    public readonly string idleAnim = "Idle";
    public readonly string reloadAnim = "Reload";

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

   
    private void Start()
    {
        if(imageGun != null)
             imageGun.gameObject.SetActive(false);
        if (sysParticle != null)
            sysParticle.Stop();
    }

    public void EnableSlider()
    {
        Invoke("AfterEnableSlider", .5f);
    }

    public void AfterEnableSlider()
    {
        imageGun.gameObject.SetActive(true);

    }

    public void EmptyShellReject()
    {
        GameObject go = null;
        if(gunScriptable.modeShoot == ShootMode.RayCast)
            go = GameManager.managerGame.managerPool.objectsForPool.UseTheGameObject(GameManager.managerGame.managerPool.emptyPistolList, gunPivot.position, Quaternion.identity);
        else if(gunScriptable.modeShoot == ShootMode.Instantiation)
        {
            go = GameManager.managerGame.managerPool.objectsForPool.UseTheGameObject(GameManager.managerGame.managerPool.emptyRocketCellList, gunPivot.position, Quaternion.identity);

        }



        go.SetActive(true);
        go.GetComponent<Rigidbody>().AddForce(transform.right * 2f, ForceMode.Impulse);
    }

    public void TurnLight(bool light)
    {
        if (light)
        {
            currentLight.intensity = 100;
        }
        else
        {
            currentLight.intensity = 0;
        }
    }

    public void ActivateEffects()
    {
        sysParticle.Play();
    }
    public void AnimationToExecuted(string currentAction, bool isExecutableBool)
    {
        if(_animator != null)
            _animator.SetBool(currentAction, isExecutableBool);
    }
}
