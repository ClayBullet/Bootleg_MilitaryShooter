using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings_Player : MonoBehaviour
{
    public PopUpManager managePopUpCurrent;
    public string popUpNameToStart;
    private void Start()
    {
        GameManager.managerGame.managerSettings.playerSettings = this;
        GameManager.managerGame.managerInput.DownPress_EscapeButton += PauseGame;

    }

    public void PauseGame()
    {
        if(managePopUpCurrent.currentGroupPopUps.Count <= 0)
        {
           
            GameManager.managerGame.managerTime.FreezeTimeScale();
            GameManager.managerGame.hiddenCursor.CursorIsVisible(true);
            managePopUpCurrent.ShowPopUpGroupIfAllPopUpsAreClosed(popUpNameToStart, null);
        }
        else
        {
            managePopUpCurrent.CloseTheCurrentPopUpOpened(popUpNameToStart);
            GameManager.managerGame.hiddenCursor.CursorIsVisible(false);
            GameManager.managerGame.managerTime.RecoverTimeScale();
        }

    }
}
