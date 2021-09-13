using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpEventsManager : PopUpManager
{
    public Action normalActionForRealise { get { return InvokeThePopUp; } set { } }

    [SerializeField] private string invokeString;



    public bool doesntHaveAnScriptableSectorBool;

  
   
    public void InvokeThePopUp()
    {
        ShowPopUpGroupAndCloseAllManagers(invokeString);
    }
}
