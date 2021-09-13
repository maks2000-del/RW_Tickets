using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tickets.DB;
using Tickets.Commands;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Tickets.Views;
using System.Windows;
using System.Windows.Input;

namespace Tickets.ViewModels
{
    public class AdminViewModel : BaseViewModel
    {

        

        private BaseViewModel _selectedViewModel = new AdminUsersViewModel();

        public BaseViewModel SelectedViewModel_admin
        {
            get { return _selectedViewModel; }
            set
            {
                _selectedViewModel = value;
                OnPropertyChanged(nameof(SelectedViewModel_admin));
            }
        }

        public ICommand UpdateViewCommand_admin { get; set; }
        public AdminViewModel()
        {
            UpdateViewCommand_admin = new UpdateViewCommand_admin(this);

            
        }
        
    }
}
