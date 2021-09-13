using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Tickets.Commands;
using Tickets.DB;

namespace Tickets.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        #region private fields
        private readonly Tickets_db context = new Tickets_db();
        UserCommand userCommand = new UserCommand();
        private string _loginUsername;
        private string _loginPassword;
        #endregion

        #region public fields
        public string loginUsername
        {
            get => _loginUsername;
            set => Set(ref _loginUsername, value);
        }
        public string loginPassword
        {
            get => _loginPassword;
            set => Set(ref _loginPassword, value);
        }
        #endregion

        #region Commands executhion
        public ICommand LoginCommand { get; }
        private bool CanLoginCommandExecute(object p) => true;
        private void OnLoginCommandExecuted(object p)
        {
            if (!String.IsNullOrEmpty(loginUsername) && !String.IsNullOrEmpty(loginPassword))
            {
                User user = userCommand.getByMail(loginUsername);
                if (user != null)
                {
                    if (User.getHash(loginPassword).Equals(user.password))
                    {
                        CurrentUser.User = user;
                        MainViewModelMV mvvm = new MainViewModelMV(user);
                        MainWindow mv = new MainWindow
                        {
                            DataContext = mvvm
                        };
                        mv.Show();
                        (p as Window).Close();
                        //return true;
                    }
                    else
                    {
                        MessageBox.Show("Неверный пароль");
                    }

                }
                else
                {
                    MessageBox.Show("Пользователя с таким email не существует");
                }
            }
            else
            {
                MessageBox.Show("Не всё ввели");
            }
        }
        #endregion
        public LoginViewModel()
        {
            #region Commands

            LoginCommand = new RelayCommand(OnLoginCommandExecuted, CanLoginCommandExecute);

            #endregion
        }
    }
}
