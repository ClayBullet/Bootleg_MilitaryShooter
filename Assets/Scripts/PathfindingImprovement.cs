using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathfindingImprovement : MonoBehaviour
{
    #region Fields
    [SerializeField] private float tolerableDistance;

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
    private void Awake()
    {
        GameManager.managerGame.improvementPathfiding = this;
    }
    #endregion

    #region Static_Methods
    #endregion
}
[System.Serializable] 
public class CoordsAndTarget
{
    public Transform target;
    public Transform meTransform;
    public Vector3 currentCoordsToGo;

    public CoordsAndTarget(Transform _target, Transform _meTransform, Vector3 _currentCoordsToGo)
    {
        _target = target;
        _meTransform = meTransform;
        _currentCoordsToGo = currentCoordsToGo;
    }
}