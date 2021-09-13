using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatrixCombatResolution : MonoBehaviour
{
    #region Fields
    public List<Vector3> avoidableCoordinates = new List<Vector3>();
    #endregion

    #region Constructors
    #endregion

    #region Getters
    #endregion

    #region Setters
    #endregion

    #region Public_Methods

  

    public void AvoidableCoordenates(Vector3 coords, bool isRemovable)
    {
        if (isRemovable)
        {
            avoidableCoordinates.Remove(coords);
        }
        else
        {
            if (!avoidableCoordinates.Contains(coords))
                avoidableCoordinates.Add(coords);
        }
    }


    #endregion

    #region Private_Methods

    private void Awake()
    {
        GameManager.managerGame.resolutionCombatMatrix = this;
    }
    #endregion

    #region Static_Methods
    #endregion
}
