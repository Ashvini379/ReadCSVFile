using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ReadCSVFile.Commands
{
    public class DelegateCommand : ICommand
    {
        private Action _Execute;
        public DelegateCommand(Action _execute)
        {
            _Execute = _execute;
        }
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _Execute();
        }
    }
}
