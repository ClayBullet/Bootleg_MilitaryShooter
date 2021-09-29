using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class FiniteStateManager : MonoBehaviour
{
    #region Fields
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
        GameManager.managerGame.stateFiniteManager = this;
    }

    private void Start()
    {
        IdentifierTeam[] teamIDs = FindObjectsOfType<IdentifierTeam>();

        
    }
    #endregion

    #region Static_Methods
    #endregion
}

[System.Serializable]
public class ListObjs
{
    public List<AIManager> aiAllies = new List<AIManager>();
    public List<IdentifierTeam> aiEnemies = new List<IdentifierTeam>();

    public ListObjs(List<AIManager> _aiAllies, List<IdentifierTeam> _aiEnemies)
    {
        _aiAllies = aiAllies;
        _aiEnemies = aiEnemies;
    }
}
public enum PossibleActions
{
    Idle,
    Attack,
    Patrol,
    SearchGun,
    Stun,
    Cover,
    Chase,
    Flee,
    Scripted
}