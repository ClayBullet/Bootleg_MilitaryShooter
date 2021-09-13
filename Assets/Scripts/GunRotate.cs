using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunRotate : MonoBehaviour
{
    public Vector3 rotationEuler;
    public Vector3 positionOffset;
    private void LateUpdate()
    {
        
        transform.localRotation = Quaternion.Euler(rotationEuler);
        transform.localPosition = positionOffset;

    }
}
