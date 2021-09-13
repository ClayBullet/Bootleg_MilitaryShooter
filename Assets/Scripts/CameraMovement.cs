using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [Header("MOVEMENT CAMERA")]
    [Space]
    [SerializeField] private Vector3 offsetCamera;
    [SerializeField] private Vector2 clampCamera;
    [SerializeField] private Transform playerCamera;
    [SerializeField] [Range(1, 10)] public float sensibility;
    [SerializeField] private float snapiness;
    [SerializeField] private float speedCamera; 
    public bool aimCameraBool;
    float currentSensibility = 0;

    private Vector3 axisMovement;

    [HideInInspector] public bool avoidAccessToTheCameraMovemenetBool;

    private float _xPreviousSmooth, _yPreviousSmooth;
    [Header("CAMERA MOUSE")]
    [Space]
    [SerializeField] private float maxDelayCameraMouse;
    private float _delayCameraMouse;

    private float rotationX;
    private float rotationY;
    private List<float> rotArrayX = new List<float>();
    private List<float> rotArrayY = new List<float>();
    Quaternion originalRotation;
    private void Start()
    {
        GameManager.managerGame.managerInput.cameraAxis += MovementCamera;
        originalRotation = playerCamera.transform.rotation;
    }


    private void MovementCamera(float x, float y)
    {
       

            if (avoidAccessToTheCameraMovemenetBool) return;

            Vector3 currentMovement = new Vector3(x, y);
           

            if (!aimCameraBool)
                    currentSensibility = (sensibility * Time.deltaTime);
                else
                    currentSensibility = (sensibility * Time.deltaTime) / 4;




                currentMovement *= currentSensibility;
              
                axisMovement.x += Mathf.Lerp(_xPreviousSmooth, currentMovement.x, Time.deltaTime * speedCamera);
                axisMovement.y += Mathf.Lerp(_yPreviousSmooth, currentMovement.y, Time.deltaTime * speedCamera);

                axisMovement.y = Mathf.Clamp(axisMovement.y, clampCamera.x, clampCamera.y);

                playerCamera.rotation = Quaternion.Euler(new Vector3(axisMovement.y, axisMovement.x * -1, 0f));

                transform.rotation = Quaternion.Euler(0f, axisMovement.x * -1, 0f);

                _xPreviousSmooth = currentMovement.x;
                _yPreviousSmooth = currentMovement.y;



        //if (!specialCameraModeBool)
        //{
        //    _currentCameraPivotRotable = new Vector3(x, y);

        //    _xSmooth += Mathf.Lerp(_previousCameraPivotRotable.x, _currentCameraPivotRotable.x, Time.deltaTime * speedForRotateTheCamera);
        //    _ySmooth += Mathf.Lerp(_previousCameraPivotRotable.y, _currentCameraPivotRotable.y, Time.deltaTime * speedForRotateTheCamera);


        //    _xSmooth = Mathf.Clamp(_xSmooth, clampingCameraPivot.x, clampingCameraPivot.y);

        //    transform.rotation = Quaternion.Euler(_xSmooth, _ySmooth, 0f);

        //    _previousCameraPivotRotable = _currentCameraPivotRotable;
        //}






    }

    public void CameraMovement_Mouse(float x, float y, float exteriorValue)
    {
        Vector3 currentMovement = new Vector3(x, y, 0f);

       

            if (!aimCameraBool)
                currentSensibility = sensibility * exteriorValue;
            else
                currentSensibility = (sensibility * exteriorValue) / 4;


            float rotationAverageX = 0;



            rotationX += x * currentSensibility;
            rotationY += y * currentSensibility;

            rotationY = Mathf.Clamp(rotationY, clampCamera.x, clampCamera.y);

        if(currentMovement.x != 0)
            rotArrayX.Add(rotationX);

            if (_delayCameraMouse > maxDelayCameraMouse)
            {
                rotArrayX.RemoveAt(0);
            }

            for (int i = 0; i < rotArrayX.Count; i++)
            {
                rotationAverageX += rotArrayX[i];
            }

            rotationAverageX /= rotArrayX.Count;

            float rotationAverageY = 0;


        if (currentMovement.y != 0)
               rotArrayY.Add(rotationY);

            if (_delayCameraMouse > maxDelayCameraMouse)
            {
                rotArrayY.RemoveAt(0);
            }

            for (int i = 0; i < rotArrayY.Count; i++)
            {
                rotationAverageY += rotArrayY[i];
            }

            rotationAverageY /= rotArrayY.Count;

            Quaternion xQuaternion = Quaternion.AngleAxis(rotationAverageX, Vector3.up);
            Quaternion yQuaternion = Quaternion.AngleAxis(rotationAverageY, Vector3.left);

            playerCamera.localRotation = originalRotation * xQuaternion * yQuaternion;

            if (_delayCameraMouse > maxDelayCameraMouse)
            {
                _delayCameraMouse = 0f;
            }

            _delayCameraMouse += Time.deltaTime;
        


    }
}
