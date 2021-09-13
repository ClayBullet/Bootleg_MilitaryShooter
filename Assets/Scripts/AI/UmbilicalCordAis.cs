using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class UmbilicalCordAis : MonoBehaviour
{
	#region Fields
	[SerializeField] private LayerMask maskLayer;
	#endregion
	
	#region Constructors
	#endregion
	
	#region Getters
	#endregion
	
	#region Setters
	#endregion
	
	#region Public_Methods
	public bool coordsBetweenBothBool(Vector3 origin, Vector3 destination, IdentifierTeam aiOrigin, IdentifierTeam aiDestination)
    {
		RaycastHit hit;

		if(Physics.Linecast(origin, destination, out hit, maskLayer))
        {
			if(hit.collider.GetComponent<IdentifierTeam>() != aiOrigin &&
				hit.collider.GetComponent<IdentifierTeam>() != aiDestination)
            {
				return true;

			}
		}

		return false;
    }
	#endregion
	
	#region Private_Methods
	#endregion
	
	#region Static_Methods
	#endregion
}
