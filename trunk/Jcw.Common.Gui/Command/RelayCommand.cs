using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace Jcw.Common.Gui.Command
{
    public class RelayCommand : ICommand
    {
        #region Fields

        private readonly Action execute;
        private readonly Func<bool> canExecute;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new command.
        /// </summary>
        /// <param name="execute">The execution logic.</param>
        /// <param name="canExecute">The execution status logic.</param>
        public RelayCommand(Action execute)
            : this (execute, null)
        {
        }

        /// <summary>
        /// Creates a new command.
        /// </summary>
        /// <param name="execute">The execution logic.</param>
        /// <param name="canExecute">The execution status logic.</param>
        public RelayCommand(Action execute, Func<bool> canExecute)
        {
            if (execute == null)
            {
                throw new ArgumentNullException ("execute");
            }

            this.execute = execute;
            this.canExecute = canExecute;
        }

        #endregion

        #region ICommand Implementation

        [DebuggerStepThrough]
        public bool CanExecute(object parameter)
        {
            return (canExecute != null) ?
                canExecute () :
                true;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object parameter)
        {
            execute ();
        }

        #endregion
    }

    public class RelayCommand<T> : ICommand
    {
        #region Fields

        private readonly Action<T> execute;
        private readonly Predicate<T> canExecute;

        #endregion

        #region Constructors

        public RelayCommand(Action<T> execute)
            : this (execute, null)
        {
        }

        /// <summary>
        /// Creates a new command.
        /// </summary>
        /// <param name="execute">The execution logic.</param>
        /// <param name="canExecute">The execution status logic.</param>
        public RelayCommand(Action<T> execute, Predicate<T> canExecute)
        {
            if (execute == null)
            {
                throw new ArgumentNullException ("execute");
            }

            this.execute = execute;
            this.canExecute = canExecute;
        }

        #endregion

        #region ICommand Implementation

        [DebuggerStepThrough]
        public bool CanExecute(object parameter)
        {
            return (canExecute != null) ?
                canExecute ((T)parameter) :
                true;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object parameter)
        {
            execute ((T)parameter);
        }

        #endregion
    }
}
