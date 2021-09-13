using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Tickets.Commands;
using Tickets.DB;
using Tickets.Views;

namespace Tickets.ViewModels
{
    public class RegistrathionViewModel : BaseViewModel
    {
        #region private fields
        private readonly Tickets_db context = new Tickets_db();
        private string _registerFirstName;
        private string _registerUsername;
        private string _registerPassword;
        private string _registerPassword_repeat;
        private string info;
        private string message;
        private string header;

        #endregion

        #region public fields
        public string registerFirstName
        {
            get => _registerFirstName;
            set => Set(ref _registerFirstName, value);
        }
        public string registerUsername
        {
            get => _registerUsername;
            set => Set(ref _registerUsername, value);
        }
        public string registerPassword
        {
            get => _registerPassword;
            set => Set(ref _registerPassword, value);
        }
        public string registerPassword_repeat
        {
            get => _registerPassword_repeat;
            set => Set(ref _registerPassword_repeat, value);
        }
        public string Info
        {
            get { return info; }
            set
            {
                info = value;
                OnPropertyChanged("Info");
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
        public string Header
        {
            get { return header; }
            set
            {
                header = value;
                OnPropertyChanged("Header");
            }
        }
        #endregion

        private void ClearFileds()
        {
            registerFirstName = "";
            registerUsername = "";
            registerPassword = "";
            registerPassword_repeat = "";
            Info = "";
        }
        public RegistrathionViewModel()
        {
            #region Commands

            RegisterCommand = new RelayCommand(OnRegisterCommandExecuted, CanRegisterCommandExecute);

            #endregion
        }

        #region Commands executhion
        public ICommand RegisterCommand { get; }
        private bool CanRegisterCommandExecute(object p) => true;
        private void OnRegisterCommandExecuted(object p)
        {
            string pattern = @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$";

            if (!String.IsNullOrEmpty(registerFirstName) && !String.IsNullOrEmpty(registerUsername) && !String.IsNullOrEmpty(registerPassword) && !String.IsNullOrEmpty(registerPassword_repeat))
            {
                if (registerFirstName.Length > 2)
                {
                    if (Regex.IsMatch(registerUsername, pattern, RegexOptions.IgnoreCase))
                    {
                        if (context.Users.FirstOrDefault(u => u.mail == registerUsername) == null)
                        {
                            if (registerPassword.Length > 5)
                            {
                                if (registerPassword == registerPassword_repeat)
                                {
                                    User user = new User(registerFirstName, registerUsername, registerPassword);
                                    context.Users.Add(user);
                                    context.SaveChanges();

                                    InfoWindowView infoWindowView = new InfoWindowView();
                                    infoWindowView.DataContext = this;
                                    Header = "Регистрация нового пользователя";
                                    Message = $"Пользователь {registerFirstName} успешно зарегистрирован.\nДля входа используйте ваш email: {registerUsername}";
                                    infoWindowView.ShowDialog();
                                    ClearFileds();

                                }
                                else Info = "Пароли не совпадают :(";
                            }
                            else Info = "пароль короче 6 символов";
                        }
                        else Info = "Вы уже зарегистрированы";
                    }
                    else Info = "Почта введена некорректно";
                }
                else Info = "Имя короче трёх символов";
            }
            else Info = "Заполните все поля";

        }
        #endregion
    }
}
