using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{

    private float delayForTimeReduce;
    [SerializeField] private float reducedTime;
    private bool isTimedNowBarelyFreezeBool;

    private float normalTime;
    public bool isTimeFreezingBool;
    private float _currentTimeSinceReduced;
    //private void OnEnable()
    //{
    //    GameManager.managerGame.managerTime = this;

    //}
    private void Awake()
    {
        GameManager.managerGame.managerTime = this;
    }

    private void Start()
    {
        normalTime = 1f;
        RecoverTimeScale();
    }

  
    public void ReduceTimeScale(float timeForReduce)
    {
        if(Time.timeScale == normalTime)
        {
            delayForTimeReduce += timeForReduce;

            if (!isTimedNowBarelyFreezeBool)
                StartCoroutine(TimeReduced());

        }
       
    }

    private IEnumerator TimeReduced()
    {
        _currentTimeSinceReduced = normalTime;
        isTimedNowBarelyFreezeBool = true;
        Time.timeScale = reducedTime;
        yield return new WaitForSecondsRealtime(delayForTimeReduce);
        delayForTimeReduce = 0f;

        Time.timeScale = _currentTimeSinceReduced;
        isTimedNowBarelyFreezeBool = false;

    }

    public void FreezeTimeScale()
    {
        if (isTimeFreezingBool) return;

        Time.timeScale = Mathf.Epsilon;
        _currentTimeSinceReduced = Time.timeScale;
        isTimeFreezingBool = true;
    }

    public void ReduceTimeScaleWhenYouWin()
    {
        Time.timeScale = .8f;
        normalTime = .8f;
    }
    [ContextMenu(" Recover Time Scale")]
    public void RecoverTimeScale()
    {
        isTimeFreezingBool = false;

        Time.timeScale = normalTime;
    }

    
}
