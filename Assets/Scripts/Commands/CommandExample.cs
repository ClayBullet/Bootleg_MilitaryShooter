using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Input;
using UnityEngine;

public class CommandExample : MonoBehaviour, ICommand
{
    #region Fields
    #endregion

    #region Constructors
    #endregion

    #region Getters
    #endregion

    #region Setters
    #endregion

    #region Public_Methods
    #endregion

    #region Private_Methods
    #endregion

    #region Static_Methods
    #endregion
    public event EventHandler CanExecuteChanged;

    public bool CanExecute(object parameter)
    {
        throw new NotImplementedException();
    }

    public void Execute(object parameter)
    {
        throw new NotImplementedException();
    }
}
