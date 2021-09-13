using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public static class ExtensionMethods
{
    public static float GaussianStyleForPitchSound(float min, float max)
    {
        float rand1 = Random.Range(min, max);
        float rand2 = Random.Range(min, max);

        float n = Mathf.Sqrt(-2.0f * Mathf.Log(rand1)) * Mathf.Cos((2.0f * Mathf.PI) * rand2);
        return (.5f + .25f * n);
    }

    public static bool ApproximationValues(float a, float b, float tolerance = 0.5f)
    {
        return (Mathf.Abs(a - b) < tolerance);
    }

    public static float dotRightLeft(Vector3 a, Vector3 b)
    {
        return a.x * b.x + a.y * b.y;
    }

    public static float ClampAngle(float _Angle)
    {
        float ReturnAngle = _Angle;

        if (_Angle < 0f)
            ReturnAngle = (_Angle + (360f * ((_Angle / 360f) + 1)));

        else if (_Angle > 360f)
            ReturnAngle = (_Angle - (360f * (_Angle / 360f)));

        else if (ReturnAngle == 360) //Never use 360, only go from 0 to 359
            ReturnAngle = 0;

        return ReturnAngle;
    }


}
