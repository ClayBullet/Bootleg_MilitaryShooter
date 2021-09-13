using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageExplossion : MonoBehaviour
{
	#region Fields
	public LayerMask maskGun;
	#endregion
	
	#region Constructors
	#endregion
	
	#region Getters
	#endregion
	
	#region Setters
	#endregion
	
	#region Public_Methods
	public void CurrentExplossionDamage(Vector3 origin, float radius, DamageStates stateDamage)
    {
		Collider[] col = Physics.OverlapSphere(origin, radius, maskGun);

		List<IDamage> healths = new List<IDamage>();

        foreach (Collider co in col)
        {
			if(co.GetComponent<IDamage>() != null)
            {
				healths.Add(co.GetComponent<IDamage>());
            }
        }

		foreach(IDamage health in healths)
        {
			health.DamageState(stateDamage);
        }
    }
	#endregion
	
	#region Private_Methods
	#endregion
	
	#region Static_Methods
	#endregion
}
