using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PopUp_OnionLayer : PopUpLayer
{
    public PopUpLayerBase layerPopUp;

    public void Start()
    {
        manager = GameManager.managerGame.managerGlobalPopUp.managerPopUpSettings;
       
    }
    protected override void DoIn()
    {
        
    }

    protected override void DoOut()
    {
      
    }

    public void ShowPopUp(string PopUp)
    {
        manager.ShowPopUpGroupAndCloseAllManagers(PopUp);
    }
}
