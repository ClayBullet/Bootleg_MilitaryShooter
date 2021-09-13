using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneSystem : MonoBehaviour
{
    [Header("SCENE SYSTEM")]
    [Space]
    [SerializeField] private SceneScoreSystem[] scoreSceneSystem;

    
    private void Start()
    {
        if(GameManager.managerGame.managerTime != null)
                GameManager.managerGame.managerTime.RecoverTimeScale();

        //for (int i = 0; i < scoreSceneSystem.Length; i++)
        //{
        //    SceneCanvas canScen = scoreSceneSystem[i].chargeableScene.canvasScene;
        //    canScen.imgScene.sprite = scoreSceneSystem[i].spriteScene;
        //    canScen.nameText.text = scoreSceneSystem[i].scene;
        //    int valueForChage = i;
        //    canScen.btn.onClick.AddListener(() => ChargeNameLevel(canScen.sceneString));
        //}

    }

    public void ChargeThisLevel(int id)
    {
        SceneManager.LoadScene(scoreSceneSystem[id].scene);
        
    }

    public void ChargeNameLevel(string id)
    {
        SceneManager.LoadScene(id);

    }


}

[System.Serializable]
public class SceneScoreSystem
{
    public int idScene;
    public string scene;
    public float timeForEndTheLevel;
    public Sprite spriteScene;
    public SceneChargeable chargeableScene;
    public SceneScoreSystem(int _idScene, string _scene, float _timeForEndTheLevel, Sprite _spriteScene, SceneChargeable _chargeableScene)
    {
        _idScene = idScene;
        _scene = scene;
        _timeForEndTheLevel = timeForEndTheLevel;
        _chargeableScene = chargeableScene;
    }
}

[System.Serializable]
public class SceneCanvas
{
    public Text nameText;
    public Text textTime;
    public Image imgScene;
    public Image[] stars;
    public Button btn;
    public string sceneString;
    public SceneCanvas(Text _textTime, Image _imgScene, Image[] _stars, Button _btn, string _sceneString, Text _nameText)
    {
        _textTime = textTime;
        _imgScene = imgScene;
        _stars = stars;
        _btn = btn;
        _sceneString = sceneString;
        _nameText = nameText;
    }
}