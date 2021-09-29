using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class CommandController : MonoBehaviour
{
	#region Fields

	private readonly Queue<System.Windows.Input.ICommand> _commandsToExecute;
	private bool _runningCommandBool;
    #endregion

    #region Constructors
    #endregion

    #region Getters
    #endregion

    #region Setters
    #endregion

    #region Public_Methods
    public void AddCommand(System.Windows.Input.ICommand commandToEnqueue)
    {
        _commandsToExecute.Enqueue(commandToEnqueue);
        executeCurrentTask();
    }

    public void RunTheCommand()
    {
        
    }

    private async Task executeCurrentTask()
    {
        if (_runningCommandBool)
        {
            return;
        }

        while(_commandsToExecute.Count > 0)
        {
            var _currentCommand = _commandsToExecute.Dequeue();
            await _currentCommand.Execute();
        }
    }
    #endregion

    #region Private_Methods
    private void Awake()
    {
        GameManager.managerGame.controllerCommand = this;
    }

    #endregion

    #region Static_Methods
    #endregion
}
