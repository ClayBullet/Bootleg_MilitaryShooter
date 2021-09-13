using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpSettingsManager : PopUpManager
{

    public void Start()
    {
        GameManager.managerGame.managerGlobalPopUp.managerPopUpSettings = this;
    }
}
