using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpMainMenuManager : PopUpManager
{
    public string popUpNameToStart;
    private void Start()
    {
        statePopUp = PopUpsManagerState.OPENING;
        ShowPopUpGroupAndCloseAllManagers(popUpNameToStart);
        GameManager.managerGame.managerGlobalPopUp.managerPopUpSettings = this;
    }

   
}
