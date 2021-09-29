using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New AMMO", menuName = "New Ammo")]
public class AmmoScriptable : ScriptableObject
{
	#region Fields
	public string nameAmmo;
	[TextArea] public string ammoDescription;
	/// <summary>
	/// Las armas asociadas a este tipo de munición.
	/// </summary>
	public List<WeaponScriptable> gunScriptableList;
	/// <summary>
	/// Los objetos arrojadizos asociados a este tipo de munición
	/// </summary>
	public List<ThrowScriptable> throwScriptableList;
	#endregion
	
	#region Constructors
	#endregion
	
	#region Getters
	#endregion
	
	#region Setters
	#endregion
	
	#region Public_Methods
	public void GetDescription()
    {

    }

	public bool isThisGunAssignedBool(WeaponScriptable currentGun)
    {
		return gunScriptableList.Contains(currentGun);
    }
	#endregion
	
	#region Private_Methods
	#endregion
	
	#region Static_Methods
	#endregion
}
