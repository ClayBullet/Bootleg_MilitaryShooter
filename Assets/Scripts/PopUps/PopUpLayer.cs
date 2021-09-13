using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public abstract class PopUpLayer : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, ICancelHandler, IDeselectHandler, IDropHandler,
    IInitializePotentialDragHandler, IMoveHandler, IPointerClickHandler, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler,
    IScrollHandler, ISelectHandler, ISubmitHandler, IUpdateSelectedHandler
{
    [SerializeField] protected CanvasGroup group;
    public PopUpManager manager;

    public bool interactive;
    public Action disableButtons;
    public Action enableButtons;

    #region PopUpLayerBase
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (interactive) BeginDragHandler(eventData);
    }
    protected void BeginDragHandler(PointerEventData eventData) { }

    public void OnCancel(BaseEventData eventData)
    {
        if (interactive) CancelHandler(eventData);
    }
    protected void CancelHandler(BaseEventData eventData) { }

    public void OnDeselect(BaseEventData eventData)
    {
        if (interactive) DeselectHandler(eventData);
    }
    protected virtual void DeselectHandler(BaseEventData eventData) { }

    public void OnDrag(PointerEventData eventData)
    {
        if (interactive) DragHandler(eventData);
    }
    protected virtual void DragHandler(PointerEventData eventData) { }

    public void OnDrop(PointerEventData eventData)
    {
        if (interactive) DropHandler(eventData);
    }
    protected void DropHandler(PointerEventData eventData) { }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (interactive) EndDragHandler(eventData);
    }
    protected void EndDragHandler(PointerEventData eventData) { }

    public void OnInitializePotentialDrag(PointerEventData eventData)
    {
        if (interactive) InitializePotentialDragHandler(eventData);
    }
    protected void InitializePotentialDragHandler(PointerEventData eventData) { }


    public void OnMove(AxisEventData eventData)
    {
        if (interactive) MoveHandler(eventData);
    }

    protected  void MoveHandler(AxisEventData eventData) { }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (interactive) PointerClickHandler(eventData);
    }
    protected  void PointerClickHandler(PointerEventData eventData) { }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (interactive) PointerDownHandler(eventData);
    }
    protected  void PointerDownHandler(PointerEventData eventData) { }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (interactive) PointerEnterHandler(eventData);
    }
    protected void PointerEnterHandler(PointerEventData eventData) { }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(interactive) PointerExitHandler(eventData);
    }
    protected void PointerExitHandler(PointerEventData eventData) { }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (interactive) PointerUpHandler(eventData);
    }
    protected void PointerUpHandler(PointerEventData eventData) { }

    public void OnScroll(PointerEventData eventData)
    {
        if (interactive) ScrollHandler(eventData);
    }
    protected  void ScrollHandler(PointerEventData eventData) { }

    public void OnSelect(BaseEventData eventData)
    {
        if (interactive) SelectHandler(eventData);
    }
    protected void SelectHandler(BaseEventData eventData) { }

    public void OnSubmit(BaseEventData eventData)
    {
        if (interactive) SubmitHandler(eventData);
    }
    protected void SubmitHandler(BaseEventData eventData) { }

    public void OnUpdateSelected(BaseEventData eventData)
    {
        if (interactive) UpdateSelectedHandler(eventData);
    }
    protected void UpdateSelectedHandler(BaseEventData eventData) { }

    #endregion



    public Transform takePlaceHolder;
    public PopUpLayer mainPopUpLayer;
    public bool baseLayer;
    public string idPopUp;
    protected void Awake()
    {
        if (!baseLayer)
        {
            PopUpLayer[] layerPopUp = FindObjectsOfType<PopUpLayer>();

            for (int i = 0; i < layerPopUp.Length; i++)
            {
                if (layerPopUp[i].baseLayer)
                {
                    takePlaceHolder = layerPopUp[i].takePlaceHolder;
                    mainPopUpLayer = layerPopUp[i];
                    break;
                }
            }
        }

    }
    protected abstract void DoIn();

    protected abstract void DoOut();

    public void EnaOrDisaButtons(bool isInteractable)
    {
        interactive = isInteractable;

        //group.interactable = isInteractable;
        //group.blocksRaycasts = isInteractable;

        //if (isInteractable && enableButtons != null) enableButtons.Invoke();
        //else if(disableButtons != null) disableButtons.Invoke();
    }

    public virtual void OnOpen(PopUpGroupAsset popUpGroup)
    {
        manager.onlyAssignNewShowPopUp(popUpGroup.name);
        manager.statePopUp = PopUpsManagerState.OPENING;

        if(!baseLayer)
            manager.currentGroupPopUps.Add(this);
    }
   
    public virtual void OnClose(PopUpGroupAsset popUpGroup)
    {

    }

    public virtual void CloseTheCurrentPopUp(string popUp)
    {
        manager.CloseTheCurrentPopUpOpened(popUp);
    }
}
