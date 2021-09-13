using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Tickets.Commands;

namespace Tickets.ViewModels
{
    public class MainViewModelRA : BaseViewModel
    {
        private BaseViewModel _selectedViewModel = new LoginViewModel();

        public BaseViewModel SelectedViewModel
        {
            get { return _selectedViewModel; }
            set
            {
                _selectedViewModel = value;
                OnPropertyChanged(nameof(SelectedViewModel));
            }
        }

        public ICommand UpdateViewCommand { get; set; }

        public MainViewModelRA()
        {
            UpdateViewCommand = new UpdateViewCommand(this);
        }
    }
}
