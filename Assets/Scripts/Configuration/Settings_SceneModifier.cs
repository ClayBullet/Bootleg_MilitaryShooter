using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Settings_SceneModifier : MonoBehaviour
{
   public void Scene_Modifier(string scene)
    {
        Resume_Button();

        SceneManager.LoadScene(scene);
    }

    public void ResetCurrentScene()
    {
        Resume_Button();

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Resume_Button()
    {
        GameManager.managerGame.managerSettings.playerSettings.PauseGame();
    }

    public void Quit_Button()
    {
        Application.Quit();
    }
}
