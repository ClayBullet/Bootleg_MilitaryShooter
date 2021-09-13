using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopEventUI : PopUpManager
{
	#region Fields
	[SerializeField] private PopUpLayerBase layerPopUp;
    #endregion

    #region Constructors
    #endregion

    #region Getters
    #endregion

    #region Setters
    #endregion

    #region Public_Methods
    #endregion

    #region Private_Methods
    private void Start()
    {
        placeHolderTransform = layerPopUp.manager.placeHolderTransform;
    }
    #endregion

    #region Static_Methods
    #endregion
}
