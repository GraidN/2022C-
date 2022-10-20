﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Reflex
{
    public class MyICommand<T> : ICommand
    {

        Action<T> _TargetExecuteMethod;
        Func<T, bool> _TargetCanExecuteMethod;

        public MyICommand(Action<T> executeMethod)
        {
            _TargetExecuteMethod = executeMethod;
        }

        public MyICommand(Action<T> executeMethod, Func<T, bool> canExecuteMethod)
        {
            _TargetExecuteMethod = executeMethod;
            _TargetCanExecuteMethod = canExecuteMethod;
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged(this, EventArgs.Empty);
        }
        bool ICommand.CanExecute(object parameter)
        {

            if (_TargetCanExecuteMethod != null && parameter != null)
            {
                T tparm = (T)parameter;
                return _TargetCanExecuteMethod(tparm);
            }

            if (_TargetExecuteMethod != null)
            {
                return true;
            }

            return false;
        }
        public event EventHandler CanExecuteChanged = delegate { };

        void ICommand.Execute(object parameter)
        {
            if (_TargetExecuteMethod != null && parameter != null)
            {
                _TargetExecuteMethod((T)parameter);
            }
        }
    }
}