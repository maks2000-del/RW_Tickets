using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Tickets.ViewModels;

namespace Tickets.Commands
{
    public class UpdateViewCommand_main : ICommand
    {
        public MainViewModelMV viewModel;

        public UpdateViewCommand_main(MainViewModelMV viewModel)
        {
            this.viewModel = viewModel;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (parameter.ToString() == "My Tickets")
            {
                viewModel.SelectedViewModel_main = new MyTicketsViewModel();
            }
            else
            if (parameter.ToString() == "New Ticket")
            {
                if(CurrentUser.isFullRegistrathion())
                viewModel.SelectedViewModel_main = new AddTicketViewModel();
                else
                MessageBox.Show("Для оформления бронирования билета пожалуста завершите регистрацию в разделе 'Аккаунт'");
            }
            else 
            if (parameter.ToString() == "Account")
            {
                viewModel.SelectedViewModel_main = new AccountViewModel();
            }
            else
            if (parameter.ToString() == "Admin")
            {
                if(CurrentUser.isAdmin())
                viewModel.SelectedViewModel_main = new AdminViewModel();
                else { MessageBox.Show("У вас недостаточно прав"); }
            }
            

        }
    }
    public class UpdateViewCommand : ICommand
    {
        public MainViewModelRA viewModel;

        public UpdateViewCommand(MainViewModelRA viewModel)
        {
            this.viewModel = viewModel;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (parameter.ToString() == "Log In")
            {
                viewModel.SelectedViewModel = new LoginViewModel();
            }
            else if (parameter.ToString() == "Sig Up")
            {
                viewModel.SelectedViewModel = new RegistrathionViewModel();
            }

        }

    }

    public class UpdateViewCommand_admin : ICommand
    {
        public AdminViewModel viewModel;

        public UpdateViewCommand_admin(AdminViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) => true;

        public void Execute(object parameter)
        {
            if (parameter.ToString() == "Users")
            {
                viewModel.SelectedViewModel_admin = new AdminUsersViewModel();
            }
            else
            if (parameter.ToString() == "Voyages")
            {
                viewModel.SelectedViewModel_admin = new AdminVoyagesViewModel();
            }
            else
            if (parameter.ToString() == "Seats")
            {
                viewModel.SelectedViewModel_admin = new AdminSeatsViewModel();
            }


        }
    }

}
