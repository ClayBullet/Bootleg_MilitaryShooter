using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpKeyOnly : PopUpManager
{
    public string popUpToInvoke;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Escape_Action();
        }
    }
    public void Escape_Action()
    {
        if(currentGroupPopUps.Count <= 0)
        {
            ShowPopUpGroupAndCloseAllManagers_ForKeys(popUpToInvoke);
        }
        else
        {
            CloseTheCurrentPopUpOpened(popUpToInvoke);
        }
    }
}
