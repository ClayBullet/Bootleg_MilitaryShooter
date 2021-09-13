using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[DisallowMultipleComponent]
public class WeaponInterface : MonoBehaviour
{
	#region Fields
	public Transform placeHolderInCanvas;
	public Image imageBullet;
	[SerializeField] private Text gunText;
	[SerializeField] private Text ammoText;
	#endregion
	
	#region Constructors
	#endregion
	
	#region Getters
	#endregion
	
	#region Setters
	#endregion
	
	#region Public_Methods
	public void MagazineRepresentation(WeaponScriptable gun)
    {
        for (int i = 0; i < gun.limitMagazine; i++)
        {
			GameObject go = Instantiate(imageBullet.gameObject);
			go.transform.SetParent(placeHolderInCanvas);
		}
    }

	public void CurrentGunInMyHands(WeaponScriptable gun)
    {
		gunText.text = gun.name;
    }

	public void UpdateMagazineState(WeaponScriptable gun)
    {
        switch (gun.kindWeapon)
        {
			case AmmoKind.pistolAmmo:
				ammoText.text = GameManager.managerGame.playerInventory.ammoPistol.ToString();
				break;
			case AmmoKind.rifleAmmo:
				ammoText.text = GameManager.managerGame.playerInventory.ammoRifle.ToString();
				break;
			case AmmoKind.shotgunAmmo:
				ammoText.text = GameManager.managerGame.playerInventory.ammoShotgun.ToString();
				break;
			
			case AmmoKind.submachineAmmo:
				ammoText.text = GameManager.managerGame.playerInventory.ammoSubmachine.ToString();
				break;
        }
    }
	#endregion
	
	#region Private_Methods
	#endregion
	
	#region Static_Methods
	#endregion
}
