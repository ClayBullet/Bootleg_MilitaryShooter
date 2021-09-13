using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum PopUpActionType
{
    OPEN, CLOSE
}
public enum PopUpsManagerState
{
    IDLE, CLOSING, OPENING
}
public enum LayerToPopUp
{
    DISABLE_INPUTS,
    UI_IN_GAME,
    WORLD_CANVAS,
    INDESTRUCTIBLE

}
public enum PopUpInteractibility
{
    BUTTONS,
    KEYS,
    EVENTS
}
[System.Serializable]
public class PopUpActionIntention
{
    public PopUpActionType actionType;
    public string assetName;
    public Action onIdleCallback;

    public PopUpActionIntention(PopUpActionType _actionType, string _assetName, Action _onIdleCallback = null)
    {
        actionType = _actionType;
        assetName = _assetName;
        onIdleCallback = _onIdleCallback;
    }
}
public class PopUpManager : MonoBehaviour
{
    public Transform placeHolderTransform;
    public List<PopUpActionIntention> intentions = new List<PopUpActionIntention>();
    public List<PopUpLayer> currentGroupPopUps = new List<PopUpLayer>();
    public PopUpGroupAsset currentGroup = null;
    public PopUpGroupAsset nextGroup = null;
    public PopUpsManagerState statePopUp;
    public LayerToPopUp popUpLayerToInteract;
    public PopUpInteractibility interactibilityPopUp;
    public KeyCode key;
    protected PopUpActionIntention _currentIntention;
    public PopUpLayer mainLayer;

    public Action onClose;
    public Action onOpen;

   
  
    public void ShowPopUpGroup(string popUpGroup, Action onIdle)
    {
        intentions.Add(new PopUpActionIntention(PopUpActionType.OPEN, popUpGroup, onIdle));
    }
  
    public void ExecuteItentions()
    {

        if (intentions.Count > 0 && statePopUp == PopUpsManagerState.IDLE
            || intentions.Count > 0 && currentGroup != null && statePopUp != PopUpsManagerState.IDLE)
        {

            _currentIntention = intentions[0];
            intentions.RemoveAt(0);
            intentions.TrimExcess();

            if (_currentIntention.actionType == PopUpActionType.CLOSE)
            {
                CloseCurrentPopUpProcess();
            }
            else if(_currentIntention.actionType == PopUpActionType.OPEN)
            {

                doShowPopUpGroup(_currentIntention.assetName);
                
            }
        }
    }
    public void doShowPopUpGroup(string popUpGroup)
    {
        PopUpGroupAsset currentGroupAssetPopUp = null;
        for (int i = 0; i < GameManager.managerGame.managerGlobalPopUp.popupGroupAssetsList.Count; i++)
        {
            if (popUpGroup == GameManager.managerGame.managerGlobalPopUp.popupGroupAssetsList[i].name)
            {
                currentGroupAssetPopUp = GameManager.managerGame.managerGlobalPopUp.popupGroupAssetsList[i];
                break;
            }
        }

        nextGroup = currentGroupAssetPopUp;


        if (currentGroupAssetPopUp != null)
            ShowOrHideAPopUp();
    }

    public void onlyAssignNewShowPopUp(string popUpGroup)
    {
        PopUpGroupAsset currentGroupAssetPopUp = null;

        for (int i = 0; i < GameManager.managerGame.managerGlobalPopUp.popupGroupAssetsList.Count; i++)
        {
            if (popUpGroup == GameManager.managerGame.managerGlobalPopUp.popupGroupAssetsList[i].name)
            {
                currentGroupAssetPopUp = GameManager.managerGame.managerGlobalPopUp.popupGroupAssetsList[i];
                break;
            }
        }


        currentGroup = currentGroupAssetPopUp;
        statePopUp = PopUpsManagerState.OPENING;
    }
    public void ShowPopUpGroupAndCloseAllManagers_ForButtons(string popupGroup)
    {
        ShowPopUpGroupAndCloseAllManagers(popupGroup, null);
    }

    public void ShowPopUpGroupAndCloseAllManagers(string popupGroup)
    {
        ShowPopUpGroupAndCloseAllManagers(popupGroup, null);
    }
    public void ShowPopUpGroupAndCloseAllManagers_ForKeys(string popupGroup)
    {
        ShowPopUpGroupIfAllPopUpsAreClosed(popupGroup, null);
    }
    public void ShowPopUpGroupAndCloseAllManagers(string popupGroup, Action onIdleCallback)
    {
        PopUpManager[] managers = GameObject.FindObjectsOfType<PopUpManager>();

       foreach(PopUpManager mgr in managers)
        {       
            mgr.CloseCurrentPopUpProcess();
        }

        PopUpActionIntention popUpAction = new PopUpActionIntention(PopUpActionType.OPEN, popupGroup);

        intentions.Add(popUpAction);


        ExecuteItentions();
    }

    public void ShowPopUpGroupIfAllPopUpsAreClosed(string popUpGroup, Action onIdleCallback)
    {
        PopUpManager[] managers = GameObject.FindObjectsOfType<PopUpManager>();

        foreach (PopUpManager mgr in managers)
        {
            if (mgr.currentGroup != null)
                return;
        }
        PopUpActionIntention popUpAction = new PopUpActionIntention(PopUpActionType.OPEN, popUpGroup);

        intentions.Add(popUpAction);
        ExecuteItentions();
    }

    public void CloseTheCurrentPopUpOpened(string popupGroup)
    {
        PopUpActionIntention popUpAction = new PopUpActionIntention(PopUpActionType.CLOSE, popupGroup);

        intentions.Add(popUpAction);

        ExecuteItentions();
    }
    public void CloseCurrentPopUpProcess()
    {
        if (statePopUp == PopUpsManagerState.IDLE)
            return;

        if (currentGroup != null)
        {
            statePopUp = PopUpsManagerState.CLOSING;

        }

        mainLayer.EnaOrDisaButtons(true);

        if(intentions.Count == 0)
        {
            if(onClose != null)
                onClose.Invoke();

           
            CloseOld();
        }
    }

    protected virtual void OnAllPopupsClosed(string namePopUp)
    {
        
    }

    public void ShowOrHideAPopUp()
    {

        if(currentGroup != null)
        {
            CloseOld();
        }
        else
        {
            OpenNew();
        }
    }
    public virtual void OpenNew()
    {

        statePopUp = PopUpsManagerState.OPENING;
        currentGroup = nextGroup;

        nextGroup = null;
        foreach (PopUpLayer currentPopUp in currentGroupPopUps)
        {
            currentPopUp.OnOpen(currentGroup);         
        }
        for (int i = 0; i < currentGroup.prefab.Count; i++)
        {

            PopUpLayer go = Instantiate(currentGroup.prefab[i].popupPrefab);

            if(go.manager == null)
                go.manager = this;

            go.gameObject.SetActive(true);
            go.transform.SetParent(placeHolderTransform, false);
            go.OnOpen(currentGroup);
            go.EnaOrDisaButtons(true);

     
         
            currentGroupPopUps.Add(go);
        }

        if(LayerToPopUp.DISABLE_INPUTS == popUpLayerToInteract)
        {
            if(GameManager.managerGame.managerInput != null)
                GameManager.managerGame.managerInput.disableInputsBool = true;
        }
    }

    public void CloseOld()
    {

        List<PopUpLayer> deleteablePopUpLayer = new List<PopUpLayer>();
        foreach (PopUpLayer currentPopUp in currentGroupPopUps)
        {
            currentPopUp.EnaOrDisaButtons(false);
            currentPopUp.OnClose(currentGroup);
            if(currentPopUp.gameObject != null && currentGroup != currentPopUp)
            {

                Destroy(currentPopUp.gameObject);
                deleteablePopUpLayer.Add(currentPopUp);

            }

        }

        for (int i = 0; i < deleteablePopUpLayer.Count; i++)
        {
            currentGroupPopUps.Remove(deleteablePopUpLayer[i]);
        }

        currentGroup = null;

        statePopUp = PopUpsManagerState.IDLE;

        if (LayerToPopUp.DISABLE_INPUTS == popUpLayerToInteract)
        {
            if(GameManager.managerGame.managerInput != null)
                GameManager.managerGame.managerInput.disableInputsBool = false;
        }
    }

    public IEnumerator CloseWhenInteractionHappen_KEYCODES(string popup)
    {
        yield return new WaitUntil(() => Input.GetKeyDown(key));
        CloseTheCurrentPopUpOpened(popup);
    }
}
