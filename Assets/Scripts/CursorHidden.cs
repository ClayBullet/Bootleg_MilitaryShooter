using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorHidden : MonoBehaviour
{
    private void Awake()
    {
        GameManager.managerGame.hiddenCursor = this;
    }
    private void OnLevelWasLoaded(int level)
    {
        GameManager.managerGame.hiddenCursor = this;
    }

    private void Start()
    {
        CursorIsVisible(false);
    }
    public void  CursorIsVisible(bool isVisibleBool)
    {
        if (!isVisibleBool)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
