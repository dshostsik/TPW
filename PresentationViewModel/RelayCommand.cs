using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ViewModelAPI
{
    public class RelayCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;
        public readonly Action execute;
        public readonly Func<bool>? canExecute;

        public RelayCommand(Action exec, Func<bool> canBeExecuted = null) {
            if(exec != null)
            {
                execute = exec;
            }
            else
            {
                throw new ArgumentNullException(nameof(exec));
            }
        }

        public bool CanExecute(object? parameter)
        {
            return canExecute?.Invoke() ?? true;
        }

        public void Execute(object? parameter)
        {
            execute();
        }
    }
}
