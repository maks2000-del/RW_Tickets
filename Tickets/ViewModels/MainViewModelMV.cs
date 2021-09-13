using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Tickets.Commands;
using Tickets.DB;
using Tickets.Views;

namespace Tickets.ViewModels
{
    public class MainViewModelMV : BaseViewModel
    {
        private BaseViewModel _selectedViewModel = new MyTicketsViewModel();
        private User user;

        public BaseViewModel SelectedViewModel_main
        {
            get { return _selectedViewModel; }
            set
            {
                    _selectedViewModel = value;
                    OnPropertyChanged(nameof(SelectedViewModel_main));
            }
        }

        public ICommand UpdateViewCommand_main { get; set; }

        public MainViewModelMV(User user)
        {
            this.user = user;
            UpdateViewCommand_main = new UpdateViewCommand_main(this);

        }
    }
}