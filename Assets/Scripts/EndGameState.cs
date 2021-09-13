using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameState : MonoBehaviour
{
    [Header("CANVAS FOR FINAL STATE")]
    [Space]
    [SerializeField] private PopUpManager managerPopUp;
    [SerializeField] private string popUpForOpen;
    public HealthEnemy[] enemiesInThisScene;
    public int numberOfEnemiesKilled;

    private bool _isAccessNowInTheEndLevelBool;

    private void Awake()
    {
        GameManager.managerGame.stateGameEnd = this;
    }
    private void Start()
    {
        enemiesInThisScene = FindObjectsOfType<HealthEnemy>();
        _isAccessNowInTheEndLevelBool = true;
    }


    private void LateUpdate()
    {
        if (isAllTheEnemiesInTheSceneDeathBool() && _isAccessNowInTheEndLevelBool)
        {
            EndTheLevel();
        }
    }
  
    public void EndTheLevel()
    {
        _isAccessNowInTheEndLevelBool = false;

        GameManager.managerGame.hiddenCursor.CursorIsVisible(true);

        managerPopUp.ShowPopUpGroupAndCloseAllManagers(popUpForOpen);
    }

    public bool isAllTheEnemiesInTheSceneDeathBool()
    {
        return false;
    }
}
