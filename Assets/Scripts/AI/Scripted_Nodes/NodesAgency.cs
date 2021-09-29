using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodesAgency : MonoBehaviour
{
	#region Fields
	[SerializeField] private List<NodeAgent> allTheNodes = new List<NodeAgent>();
	private int _agentCurrentIndex;

	#endregion
	
	#region Constructors
	#endregion
	
	#region Getters
	#endregion
	
	#region Setters
	#endregion
	
	#region Public_Methods

	public NodeAgent updateCurrentNode()
    {
		if(_agentCurrentIndex < allTheNodes.Count)
        {
			NodeAgent node = allTheNodes[_agentCurrentIndex];
			_agentCurrentIndex += 1;
			return node;
        }

		return null;
    }
    #endregion

    #region Private_Methods
    private void Start()
    {
		_agentCurrentIndex = 0;
    }
    #endregion

    #region Static_Methods
    #endregion
}
