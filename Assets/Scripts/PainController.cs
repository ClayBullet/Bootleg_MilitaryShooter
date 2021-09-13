using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PainController : MonoBehaviour
{
	#region Fields
	[SerializeField] private CorpseParts[] partsCorpses;
	[SerializeField] private float delayForDamageReceived;
	private CorpseParts _currentPartOfTheLimb;
	[SerializeField]private Animator animator;
	private bool _isDamageRealizedBool;
	#endregion
	
	#region Constructors
	#endregion
	
	#region Getters
	#endregion
	
	#region Setters
	#endregion
	
	#region Public_Methods
	public void MoreCloseBodyPart(Vector3 hitCoords)
    {
		if (_isDamageRealizedBool) return;
		_isDamageRealizedBool = true;
		float minDistance = Mathf.Infinity;
        for (int i = 0; i < partsCorpses.Length; i++)
        {
			if(Vector3.Distance(partsCorpses[i].partOfTheLimb.position, hitCoords) < minDistance)
            {
				_currentPartOfTheLimb = partsCorpses[i];
				minDistance = Vector3.Distance(partsCorpses[i].partOfTheLimb.position, hitCoords);
			}
        }

		
		GameManager.managerGame.managerAnimation.Animation_Trigger(animator, AnimationManager.isDamageable);

		switch (_currentPartOfTheLimb.limbed)
        {
			case Limbs.Head:
				GameManager.managerGame.managerAnimation.Animation_Bool(animator, AnimationManager.headTouched, true);
                break;
            case Limbs.Body:
				GameManager.managerGame.managerAnimation.Animation_Bool(animator, AnimationManager.bodyTouched, true);
				break;

        }

		StartCoroutine(DelayForDamageReceived());
    }
    #endregion

    #region Private_Methods

   

	private IEnumerator DelayForDamageReceived()
    {
		yield return new WaitForSeconds(delayForDamageReceived);
		GameManager.managerGame.managerAnimation.Animation_Bool(animator, AnimationManager.headTouched, false);
		GameManager.managerGame.managerAnimation.Animation_Bool(animator, AnimationManager.bodyTouched, false);
		_isDamageRealizedBool = false;


	}
	#endregion

	#region Static_Methods
	#endregion
}
[System.Serializable]
public class CorpseParts
{
	public string nameCorpse;
	public Transform partOfTheLimb;
	public Limbs limbed;

	public CorpseParts(string _nameCorpse, Transform _partOfTheLimb, Limbs _limbed)
    {
		_nameCorpse = nameCorpse;
		_partOfTheLimb = partOfTheLimb;
		_limbed = limbed;
    }
}
public enum Limbs
{
	Head,
	Body
}