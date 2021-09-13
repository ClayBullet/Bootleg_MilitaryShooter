using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PopUpButtonsManager : PopUpManager
{
    public PopUp_OnionLayer onionLayerPopUp;
    public bool findAboveYouBool;

    public void Start()
    {
        if(findAboveYouBool)
            onionLayerPopUp = GetComponentInParent<PopUp_OnionLayer>();

        StartCoroutine(TakeValuesWhenIsAvailable());
    }

    private IEnumerator TakeValuesWhenIsAvailable()
    {
        yield return new WaitUntil(() => onionLayerPopUp != null && onionLayerPopUp.takePlaceHolder != null);
        placeHolderTransform = onionLayerPopUp.takePlaceHolder;
        mainLayer = onionLayerPopUp;
    }

    public void SceneStorageCharge(string sceneLevel)
    {
        SceneManager.LoadScene(sceneLevel);
    }

}
