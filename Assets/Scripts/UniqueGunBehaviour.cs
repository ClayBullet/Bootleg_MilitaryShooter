using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class UniqueGunBehaviour : MonoBehaviour
{
    public Action gunAction;
    public Action<float, float> actionsThanNeedControlTime;
    public Action<string> actionsForControlGun_Empty;
    public Action freezeControlGun_Empty;
    private const int secondsBetweenActions = 1;

    private void Awake()
    {
        GameManager.managerGame.behaviourGunUnique = this;
    }
    public void ControlGunActions_Method(float time, float maxTime)
    {

        actionsThanNeedControlTime.Invoke(time, maxTime);
        
    }

    public void ControlGun_EmptyMethod()
    {
        actionsForControlGun_Empty.Invoke("");
    }

}
