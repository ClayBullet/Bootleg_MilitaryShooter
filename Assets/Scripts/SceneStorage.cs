using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneStorage : MonoBehaviour
{
    public void SceneStore(string scene)
    {
        SceneManager.LoadScene(scene);
    }


}
