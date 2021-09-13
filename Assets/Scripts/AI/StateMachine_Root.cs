using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachine_Root : MonoBehaviour
{
	#region Fields
	public bool isTheDefaultBool;

	 protected AIManager managerAI
    {
        get
        {
			return GetComponent<AIManager>();
        }
    }
	#endregion

	#region Constructors
	#endregion

	#region Getters
	#endregion

	#region Setters
	#endregion

	#region Public_Methods

	public virtual void Start()
    {
		if (isTheDefaultBool)
        {
			managerAI.actionToDoNow = EnterState;
		}
			
    }

	
	public abstract void EnterState();
	public abstract void StayState();
	public abstract void ExitState();

	public abstract void ChangeableState(PossibleActions actionPossible);

	#endregion
	
	#region Private_Methods
	#endregion
	
	#region Static_Methods
	#endregion
}

