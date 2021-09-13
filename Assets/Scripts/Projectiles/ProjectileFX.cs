using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileFX : MonoBehaviour
{
   
    public void ProjectileActive(Vector3 coordinates, Quaternion rotation)
    {
        GameObject go = GameManager.managerGame.managerPool.objectsForPool.UseTheGameObject(GameManager.managerGame.managerPool.explossionBasics, coordinates, rotation);
        ParticleSystem[] systemParticles = GetComponentsInChildren<ParticleSystem>();
        foreach (ParticleSystem particle in systemParticles)
        {
            particle.Play();
        }
        go.SetActive(true);
    }
}
