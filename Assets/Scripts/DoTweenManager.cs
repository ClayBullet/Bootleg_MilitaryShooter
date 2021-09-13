using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using DG.Tweening;
using UnityEngine.UI;
public class DoTweenManager : MonoBehaviour
{
    private void Awake()
    {
        GameManager.managerGame.managerDoTween = this;
    }
   

    public void ScreenShake(float force, float time, Transform objectForShake)
    {
        //Sequence sequence = DOTween.Sequence();
        //Vector3 coordinates = objectForShake.position;
        //sequence.Append(objectForShake.DOShakePosition(time, force));
        //objectForShake.position = coordinates;
    }

    public void WobbleEffect(Transform objectForWobble, float value, float duration, ref bool boolean)
    {
        //Sequence sequence = DOTween.Sequence();
        //sequence.Append(objectForWobble.DOLocalMoveY(value, duration));
        //boolean = boolean ? false : true;
    }

    public void ObjectShake(Transform objectForShaking, float forceShake, float time, Vector3 backToCurrentPosition)
    {
        //Sequence sequence = DOTween.Sequence();
        //sequence.Append(objectForShaking.DOShakePosition(time, forceShake));
        //sequence.Append(objectForShaking.DOLocalMove(backToCurrentPosition, 0.1f));
    }

    public void PrintText(string stringForPrint, float shake, float duration, Color colorForUse, float durationColor, ref Text mediaForPrint)
    {
        //string _st = stringForPrint;
        //Sequence sequence = DOTween.Sequence();
        //sequence.Append(mediaForPrint.DOText(_st, duration, true));
        //sequence.Append(mediaForPrint.DOColor(colorForUse, durationColor));
    }
}
