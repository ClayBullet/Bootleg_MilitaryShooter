using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CoverBehaviour : StateMachine_Root
{
    #region Fields
    [HideInInspector] public bool isSearchCoverReleasedBool;
    [SerializeField] private float distanceBetweenOtherCovers;
     public float radiusDetection;
    [SerializeField] private LayerMask maskCoverLayer;
   
    private NavMeshAgent _agentNavMesh;
    #endregion

    #region Constructors
    #endregion

    #region Getters
    #endregion

    #region Setters
    #endregion

    #region Public_Methods
    public override void ChangeableState(PossibleActions actionPossible)
    {
        switch (actionPossible)
        {
            case PossibleActions.Attack:
                break;
            case PossibleActions.Patrol:
                break;
            case PossibleActions.Idle:
                break;
        }
    }

    public override void EnterState()
    {
        managerAI.actionsPossibles = PossibleActions.Cover;

        if (isSearchCoverReleasedBool) return;

        List<Vector3> coordsSearch = new List<Vector3>();

        for (int i = 0; i < 360; i++)
        {
            RaycastHit hit;

            Vector3 coord = new Vector3(Mathf.Cos(i), 0, Mathf.Sin(i)) * radiusDetection;

            if(Physics.Raycast(transform.position, coord, out hit))
            {
                Vector3 candidateToBeACover = hit.point;

                if (hit.collider.CompareTag("Cover") &&
                    isInFrontOfThePlayerBool(candidateToBeACover) &&
                    canShootThePlayerSinceThatCoverBool(candidateToBeACover) &&
                    !isNearOfOneOfThemAvoidCoordenates(candidateToBeACover))
                {
                    coordsSearch.Add(candidateToBeACover);
                }
            } 
        }

        float distance = Mathf.Infinity;

        Vector3 playerCoordinates = GameObject.FindGameObjectWithTag("Player").transform.position;

        Vector3 finalCoordinates = Vector3.zero;


        for (int i = 0; i < coordsSearch.Count; i++)
        {
            if(Vector3.Distance(playerCoordinates, coordsSearch[i]) < distance)
            {
                distance = Vector3.Distance(playerCoordinates, coordsSearch[i]);
                finalCoordinates = coordsSearch[i];
            }
        }

        if(finalCoordinates != Vector3.zero)
        {
            managerAI.agentNavMeshH.SetDestination(finalCoordinates);
            GameManager.managerGame.resolutionCombatMatrix.AvoidableCoordenates(finalCoordinates, false);
            isSearchCoverReleasedBool = true;
        }

        managerAI.actionToDoNow = StayState;
    }

    public override void ExitState()
    {
       
    }

    public override void StayState()
    {
        managerAI.actionToDoNow = ExitState;
    }
    #endregion

    #region Private_Methods

    private bool isNearOfOneOfThemAvoidCoordenates(Vector3 coords)
    {
        for (int i = 0; i < GameManager.managerGame.resolutionCombatMatrix.avoidableCoordinates.Count; i++)
        {
            if (Vector3.Distance(coords, GameManager.managerGame.resolutionCombatMatrix.avoidableCoordinates[i]) < distanceBetweenOtherCovers)
            {
                return true;
            }
        }
        return false;
    }

    private bool isInFrontOfThePlayerBool(Vector3 coordinates)
    {
        RaycastHit hit;
        Transform playerT = GameObject.FindGameObjectWithTag("Player").transform;
        Vector3 playerCoordinates = playerT.position;

        if(Physics.Linecast(coordinates, playerCoordinates, out hit, maskCoverLayer))
        {
            if (hit.collider.CompareTag("Cover")) return true;
        }

        return false;
    }
  
    private bool canShootThePlayerSinceThatCoverBool(Vector3 coordenates)
    {
        Vector3 playerCoordinates = GameObject.FindGameObjectWithTag("Player").transform.position;
        RaycastHit hit;
        bool xMinusRayBool = false;

        if (Physics.Linecast(coordenates + new Vector3(1f, 0, 0), playerCoordinates, out hit))
        {
            if (hit.collider.CompareTag("Player"))
            {
                if (!Physics.Linecast(coordenates, coordenates + new Vector3(1f, 0, 0), out hit))
                {
                    xMinusRayBool = true;
                }

            }
        }


        bool xMaxRayBool = false;

        if (Physics.Linecast(coordenates + new Vector3(-1f, 0, 0), playerCoordinates, out hit))
        {
            if (hit.collider.CompareTag("Player"))
            {
                if (!Physics.Linecast(coordenates, coordenates + new Vector3(-1f, 0, 0), out hit))
                {
                    xMaxRayBool = true;

                }

            }

        }


        bool yMaxRayBool = false;


        if (Physics.Linecast(coordenates + new Vector3(0f, 0f, 0), playerCoordinates, out hit))
        {

            if (hit.collider.CompareTag("Player"))
            {
                if (!Physics.Linecast(coordenates, coordenates + new Vector3(0f, 0.5f, 0), out hit))
                {
                    xMaxRayBool = true;
                }

            }
        }



        bool zMinusRayBool = false;

        if (Physics.Linecast(coordenates + new Vector3(0f, 0f, 1), playerCoordinates, out hit))
        {

            if (hit.collider.CompareTag("Player"))
            {
                if (!Physics.Linecast(coordenates, coordenates + new Vector3(0f, 0f, 1), out hit))
                {
                    zMinusRayBool = true;
                }

            }
        }


        bool zMaxRayBool = false;

        if (Physics.Linecast(coordenates + new Vector3(0f, 0f, -1), playerCoordinates, out hit))
        {
            if (hit.collider.CompareTag("Player"))
            {
                if (!Physics.Linecast(coordenates, coordenates + new Vector3(0f, 0f, -1), out hit))
                {
                    zMaxRayBool = true;
                }

            }
        }

        return xMinusRayBool || xMaxRayBool || yMaxRayBool || zMinusRayBool || zMaxRayBool;
    }
    #endregion

    #region Static_Methods
    #endregion

}
