using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpManagerClickOutside : PopUpManager
{
    public string currentPopUpForSpawn;
    public void LateUpdate()
    {
        if(currentGroupPopUps.Count > 0)
        {
            CloseTheCurrentPopUpOpened(currentPopUpForSpawn);
        }
    }
}
