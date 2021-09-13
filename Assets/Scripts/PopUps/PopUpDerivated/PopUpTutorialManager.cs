using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PopUpTutorialManager : PopUpManager
{
    public bool initTheFirstTutorialBool;
    public string popUpNameToStart;
   private PopUp_TutorialLayer layerTutorialPopUp;
    private void Start()
    {
        if (initTheFirstTutorialBool)
        {
            ShowPopUpGroupAndCloseAllManagers(popUpNameToStart);

        }
    }

    public override void OpenNew()
    {
        base.OpenNew();

        for(int i = 0; i < currentGroupPopUps.Count; i++)
        {
            PopUp_TutorialLayer go = currentGroupPopUps[i] as PopUp_TutorialLayer;
            layerTutorialPopUp = go;
            //break;
        }
    }

    //public override void OpenNew()
    //{
    //    for (int j = 0; j < currentGroupPopUps.Count; j++)
    //    {
    //        PopUp_TutorialLayer go = currentGroupPopUps[j] as PopUp_TutorialLayer;
    //        layerTutorialPopUp = go;
    //        go.ChargeTheData(_objTutorial);
    //        break;
    //    }
    //    //base.OpenNew();

    //}
 

  
}
