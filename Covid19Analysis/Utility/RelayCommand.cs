﻿using System;
using System.Windows.Input;

namespace Covid19Analysis.Utility
{

    /// <summary>This class keeps track of a Command for interacting with the view</summary>
    public class RelayCommand : ICommand
    {
        #region Properties

        /// <summary>Occurs when changes occur that affect whether or not the command should execute.</summary>
        /// <returns>An EventHandler</returns>
        public event EventHandler CanExecuteChanged;

        #endregion

        #region Private Members
        private readonly Action<object> execute;
        private readonly Predicate<object> canExecute;

        #endregion

        #region Constructors

        /// <summary>Initializes a new instance of the <see cref="RelayCommand" /> class.</summary>
        /// <param name="execute">The execute.</param>
        /// <param name="canExecute">The can execute.</param>
        public RelayCommand(Action<object> execute, Predicate<object> canExecute)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        #endregion

        #region Methods

        /// <summary>Defines the method that determines whether the command can execute in its current state.</summary>
        /// <param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to null.</param>
        /// <returns>true if this command can be executed; otherwise, false.</returns>
        public bool CanExecute(object parameter)
        {
            var result = this.canExecute?.Invoke(parameter) ?? true;
            return result;
        }


        /// <summary>Defines the method to be called when the command is invoked.</summary>
        /// <param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to null.</param>
        public void Execute(object parameter)
        {
            if (this.CanExecute(parameter))
            {
                this.execute(parameter);
            }
        }


        /// <summary>Called when [can execute changed].</summary>
        public virtual void OnCanExecuteChanged()
        {
            this.CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        #endregion
    }
}