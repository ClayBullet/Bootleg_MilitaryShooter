using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLineTrace : MonoBehaviour
{
    private LineRenderer _rendererLine;
    private bool _closeUpdateTheInitialLineBool;
    private Vector3 _lastCoordinates;
  
    private void Awake()
    {
        _rendererLine = GetComponent<LineRenderer>();
    }

    public void StartCoordinates(Vector3 coordinates)
    {
        _lastCoordinates = coordinates;
        _rendererLine.SetPosition(0, coordinates);
    }

    public void LateUpdate()
    {
        _rendererLine.SetPosition(1, transform.position);
        if (this.gameObject.activeSelf)
        {
          if(Vector3.Distance(_lastCoordinates, transform.position) > 1)
            {
                _lastCoordinates = Vector3.Lerp(_lastCoordinates, transform.position, Time.deltaTime);
                _rendererLine.SetPosition(0, _lastCoordinates);
            }

            
        }
    }

   

 
}
