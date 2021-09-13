using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolSystem : MonoBehaviour
{

    [Header("SLOT PATROL")]
    [Space]
    [SerializeField] private float distanceForChangePatrol;
    [SerializeField] private PatrolSlot[] _slotsPatrols;
   [HideInInspector] public bool breakPatrolLoopBool;
    [HideInInspector] public bool isCurrentlyOpenThePatrolAccessCoroutineBool;


    private void Awake()
    {
        _slotsPatrols = GetComponentsInChildren<PatrolSlot>();
    }

    public void ActivateBreakLoopPatrol()
    {
        breakPatrolLoopBool = true;
    }
    public IEnumerator Patrol_Coroutine(NavMeshAgent _currentNavMeshAgent)
    {
        isCurrentlyOpenThePatrolAccessCoroutineBool = true;
        breakPatrolLoopBool = false;
        while (true)
        {
            for (int i = 0; i < _slotsPatrols.Length; i++)
            {

                if (breakPatrolLoopBool || _slotsPatrols.Length <= 0) break;

                yield return StartCoroutine(FollowPatrolMovement( _slotsPatrols[i].transform.position, _currentNavMeshAgent, _slotsPatrols[i]));

            }

            if (breakPatrolLoopBool) break;

            yield return new WaitForEndOfFrame();
        }

        isCurrentlyOpenThePatrolAccessCoroutineBool = false;
    }
  

    private IEnumerator FollowPatrolMovement(Vector3 coordinateToMove, NavMeshAgent _currentNavMeshAgent, PatrolSlot slotPatrol)
    {
        while (Vector3.Distance(_currentNavMeshAgent.transform.localPosition, coordinateToMove) > distanceForChangePatrol)
        {
            if (breakPatrolLoopBool) break;

            _currentNavMeshAgent.SetDestination(coordinateToMove);


            yield return new WaitForEndOfFrame();

        }
        float _currentTime = 0f;

        while (slotPatrol.timeForStayHereFloatPublic > _currentTime)
        {
            if (breakPatrolLoopBool) break;

            _currentTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }
    //public void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    for (int i = 0; i < slotPatrols.Count; i++)
    //    {
    //        try
    //        {
    //            Gizmos.DrawLine(slotPatrols[i].transform.position, slotPatrols[i + 1].transform.position);
    //        }
    //        catch{
    //            Gizmos.DrawLine(slotPatrols[i].transform.position, slotPatrols[0].transform.position);

    //        }


    //    }
    //}
}
