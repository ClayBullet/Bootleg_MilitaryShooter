using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PopUpGlobalManager : MonoBehaviour
{
    public string popUpDeath;
    [HideInInspector]public PopUpManager managerPopUpSettings;
    public List<PopUpGroupAsset> popupGroupAssetsList = new List<PopUpGroupAsset>();

    private void Awake()
    {
        GameManager.managerGame.managerGlobalPopUp = this;
    }

    public void ActivateDeathPopUpManager()
    {
        managerPopUpSettings.ShowPopUpGroupAndCloseAllManagers(popUpDeath);
    }
   
    public void DeactiveDeathPopUpManager()
    {
        managerPopUpSettings.CloseCurrentPopUpProcess();
    }
}
