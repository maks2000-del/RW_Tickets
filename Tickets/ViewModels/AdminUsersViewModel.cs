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
    class AdminUsersViewModel : BaseViewModel
    {
        UserCommand userCommand = new UserCommand();
        TicketCommand ticketCommand = new TicketCommand();
        GetInfoCommand getInfoCommand = new GetInfoCommand();

        ObservableCollection<User> tmpUsers = new ObservableCollection<User>();

        object selectedItem;
        string message;
        string searchString;

        public object SelectedItem
        {
            get { return selectedItem; }
            set
            {
                if (value != null)
                    selectedItem = value;
                OnPropertyChanged("SelectedItem");
            }
        }
        public string Message
        {
            get { return message; }
            set
            {
                message = value;
                OnPropertyChanged("Message");
            }
        }
        public string SearchString
        {
            get { return searchString; }
            set
            {
                searchString = value;
                OnPropertyChanged("SearchString");
                update();
                OnPropertyChanged("Users");
            }
        }

        public ObservableCollection<User> Users
        {
            get
            {
                selectedItem = null;
                return tmpUsers;
            }
        }

        public AdminUsersViewModel()
        {

            update();
            #region Commands

            DeleteUserCommand = new RelayCommand(OnDeleteUserCommandExecuted, CanDeleteUserCommandExecute);
            UserInfoCommand = new RelayCommand(OnUserInfoCommandExecuted, CanUserInfoCommandExecute);

            #endregion
        }
        #region Commands executhion
        public ICommand DeleteUserCommand { get; }
        private bool CanDeleteUserCommandExecute(object p) => CurrentUser.isAdmin();
        private void OnDeleteUserCommandExecuted(object p)
        {
            if (!(SelectedItem as User).privilege.Equals("admin"))
            {
                if (SelectedItem is User)
                {

                    DialogWindowView dialogWindow = new DialogWindowView();
                    dialogWindow.DataContext = this;
                    Message = $"Уверены, что хотите удалить пользователя {(SelectedItem as User).firstName} {(SelectedItem as User).secondName}?";
                    dialogWindow.ShowDialog();
                    if (dialogWindow.DialogResult == true)
                    {
                        foreach (Ticket ticket in ticketCommand.getByUserId((SelectedItem as User).id))
                            ticketCommand.delete(ticket);

                        userCommand.delete(SelectedItem as User);
                    }


                }
                else
                {
                    MessageBox.Show($"Выберите объект");
                }
            }
            else
            {
                MessageBox.Show($"Невозможно");
            }
            update();
        }

        public ICommand UserInfoCommand { get; }
        private bool CanUserInfoCommandExecute(object p) => SelectedItem != null;
        private void OnUserInfoCommandExecuted(object p)
        {

            if (SelectedItem is User)
            {
                User user = (SelectedItem as User);
                string tickets = "";
                foreach (Ticket ticket in ticketCommand.getByUserId((SelectedItem as User).id))
                {
                    tickets += $"{getInfoCommand.GetShortTicketInfo(ticket)}";
                }

                InfoWindowView infoWindowView = new InfoWindowView();
                infoWindowView.DataContext = this;

                Message = $"{getInfoCommand.GetUserInfo(user)}" +
                    $"История бронирований пользователя:\n" +
                    $"{tickets}";

                infoWindowView.ShowDialog();
            }
            else
            {
                InfoWindowView infoWindowView = new InfoWindowView();
                infoWindowView.ShowDialog();
            }

        }
        #endregion

        public void update()
        {
            tmpUsers.Clear();
            if (SearchString == null)
            {
                foreach (User user in userCommand.getAll())
                {
                    if (user.id != CurrentUser.User.id)
                        tmpUsers.Add(user);
                }
            }
            else
            {
                foreach (User user in userCommand.getAll())
                {
                    if (user.id != CurrentUser.User.id && user.mail.ToUpper().Contains(SearchString.ToUpper()))
                        tmpUsers.Add(user);
                }
            }
        }
    }
}

