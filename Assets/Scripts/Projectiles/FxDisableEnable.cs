using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FxDisableEnable : MonoBehaviour
{
    public float limitLife;

    private void OnEnable()
    {
        Invoke("DisableThis", limitLife);
    }

    public void DisableThis()
    {
        this.gameObject.SetActive(false);
    }
}
