using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickUp : MonoBehaviour
{
	#region Fields
	public WeaponScriptable gun;
	public bool isPickableGunBool;
	#endregion
	
	#region Constructors
	#endregion
	
	#region Getters
	#endregion
	
	#region Setters
	#endregion
	
	#region Public_Methods
	public void TakeGun()
    {
		GameManager.managerGame.managerInput.DownPress_ReloadButton -= TakeGun;
		GameManager.managerGame.managerPlayerWeapon.LeftGunHere(transform, this.gameObject);
		GameManager.managerGame.managerPlayerWeapon.WeaponGrabble(gun);
    }

	public void EnterGun_Take(ColliderBridge brCol)
    {
		if (!isPickableGunBool) return;

		GameManager.managerGame.managerInput.DownPress_ReloadButton += TakeGun;
		GameManager.managerGame.managerPlayerWeapon.AvailableGun(gun, true);
    }

	public void ExitGun_Take(ColliderBridge brCol)
    {
		if (!isPickableGunBool) return;


		GameManager.managerGame.managerInput.DownPress_ReloadButton -= TakeGun;
		GameManager.managerGame.managerPlayerWeapon.AvailableGun(gun, false);

	}
	#endregion

	#region Private_Methods
	#endregion

	#region Static_Methods
	#endregion
}
