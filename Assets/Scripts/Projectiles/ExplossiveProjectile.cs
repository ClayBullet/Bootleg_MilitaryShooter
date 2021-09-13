using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplossiveProjectile : MonoBehaviour
{
    [SerializeField] private float minDistance;
    [SerializeField] private LayerMask maskGun;
    private ProjectileOwner _ownerProjectile;
    private ProjectileFX _fxProjectile;
    private bool _explossiveBool;
    private void Awake()
    {
        _ownerProjectile = GetComponent<ProjectileOwner>();
        _fxProjectile = GetComponent<ProjectileFX>();
    }
    private void Update()
    {

        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, minDistance, maskGun))
        {
            GameManager.managerGame.managerSound.SoundClip(GameManager.managerGame.managerSound.clipExplossion, GameManager.managerGame.managerSound.ambienceAudioSource, 1f, .75f, .85f, false, false);
            Explossive();
            _fxProjectile.ProjectileActive(hit.point, Quaternion.identity);

        }
    }

    private void Explossive()
    {
        WeaponScriptable gunPlayer = _ownerProjectile.gunShooted;

        GameManager.managerGame.managerSound.SoundClip(GameManager.managerGame.managerSound.clipExplossion, GameManager.managerGame.managerSound.ambienceAudioSource, 1f, .75f, .85f, false, false);

        this.gameObject.SetActive(false);
    }
    public void OnDisable()
    {
        _explossiveBool = false;
    }
}
