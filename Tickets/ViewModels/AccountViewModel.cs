using System;
using Tickets.DB;
using Tickets.Commands;
using Tickets.Views;
using System.Windows;
using System.Windows.Input;
using System.Text.RegularExpressions;
using System.Linq;

namespace Tickets.ViewModels
{
    public class AccountViewModel : BaseViewModel
    {
       
        User user = CurrentUser.User;
        UserCommand userCommand = new UserCommand();
        TicketCommand ticketCommand = new TicketCommand();
        private readonly Tickets_db context = new Tickets_db();

        private string firstName;
        private string secondName;
        private string patronymic;
        private string mail;
        private string telNumber;
        private string date_of_birth;
        private string passport_id;
        private string sex;
        private bool _isMaleSelected;
        private bool _isFemaleSelected;

        private string message;
        private string header;
        private string info;
        public AccountViewModel()
        {
            firstName = user.firstName;
            secondName = user.secondName;
            patronymic = user.patronymic;
            mail = user.mail;
            telNumber = user.telNumber;
            date_of_birth = user.date_of_birth;
            passport_id = user.passport_id;
            sex = user.sex;
            if (user.sex == "female")
                IsFemaleSelected = true;
            else
                IsMaleSelected = true;

            #region Commands

            SaveChangesCommand = new RelayCommand(OnSaveChangesCommandExecuted, CanSaveChangesCommandExecute);
            DeleteUserCommand = new RelayCommand(OnDeleteUserCommandExecuted, CanDeleteUserCommandExecute);

            #endregion
        }
        #region Fileds
        public string FirstName
        {
            get { return firstName; }
            set
            {
                if (!String.IsNullOrEmpty(value))
                    firstName = value;
                OnPropertyChanged("FirstName");
            }
        }
        public string SecondName
        {
            get { return secondName; }
            set
            {
                if (!String.IsNullOrEmpty(value))
                    secondName = value;
                OnPropertyChanged("SecondName");
            }
        }
        public string Patronymic
        {
            get { return patronymic; }
            set
            {
                if (!String.IsNullOrEmpty(value))
                    patronymic = value;
                OnPropertyChanged("Patronymic");
            }
        }

        public string TelNumber
        {
            get { return telNumber; }
            set
            {
                telNumber = value;
                OnPropertyChanged("TelNumber");
            }
        }
        public string Date_of_birth
        {
            get { return date_of_birth; }
            set
            {
                date_of_birth = value;
                OnPropertyChanged("Date_of_birth");
            }
        }
        public string Passport_id
        {
            get { return passport_id; }
            set
            {
                passport_id = value;
                OnPropertyChanged("Passport_id");
            }
        }
        public string Sex
        {
            get { return sex; }
            set
            {
                sex = value;
                OnPropertyChanged("Sex");
            }
        }
        
        public bool IsMaleSelected
        {
            get { return _isMaleSelected; }
            set
            {
                if (_isMaleSelected == value) return;

                _isMaleSelected = value;
                Sex = "female";
                OnPropertyChanged("IsMaleSelected");
            }
        }
        
        public bool IsFemaleSelected
        {
            get { return _isFemaleSelected; }
            set
            {
                if (_isFemaleSelected == value) return;

                _isFemaleSelected = value;
                Sex = "male";
                OnPropertyChanged("IsFemaleSelected");
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
        public string Info
        {
            get { return info; }
            set
            {
                info = value;
                OnPropertyChanged("Info");
            }
        }
        #endregion

        #region Commands executhion
        public ICommand SaveChangesCommand { get; }
        private bool CanSaveChangesCommandExecute(object p) => true;
        private void OnSaveChangesCommandExecuted(object p)
        {
            string pattern = @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$";

            if (firstName.Length > 2 && secondName.Length > 2 && patronymic.Length > 2)
            { 
                    if (Regex.IsMatch(mail, pattern, RegexOptions.IgnoreCase))
                    {
                            string tel = @"\d{9}";
                            if (Regex.IsMatch(telNumber, tel))
                            {
                                    string data = @"\d{2}-\d{2}-\d{4}";
                                    if (Regex.IsMatch(date_of_birth, data))
                                    {   
                            
                                        string pass = @"^AB\d{7}";
                                        if (Regex.IsMatch(passport_id, pass))
                                        {
                                            Info = ""; 
                                            User tmp = new User(FirstName, SecondName, Patronymic, mail, TelNumber, date_of_birth, Passport_id, Sex);
                                            userCommand.update(user, tmp);
                                            CurrentUser.User = userCommand.getByMail(mail);
                                            InfoWindowView infoWindowView = new InfoWindowView();
                                            infoWindowView.DataContext = this;
                                            Header = "Обновление данных об аккаунте";
                                            Message = $"Изменения сохранены\n\nВаш Mail - '{mail}'";
                                            infoWindowView.ShowDialog();

                                        }
                                        else Info = "Номер паспорта задан некорректно";

                                    } 
                                    else Info = "Дата рождения задана некорректно";
                            }
                            else Info = "Мобильный номер задан некорректно";
                    }
                    else Info = "Почта введена некорректно";
            }
            else Info = "Введите корректное ФИО";
            
        }

        public ICommand DeleteUserCommand { get; }
        private bool CanDeleteUserCommandExecute(object p) => !CurrentUser.isAdmin();
        private void OnDeleteUserCommandExecuted(object p)
        {
            DialogWindowView dialogWindowView = new DialogWindowView();
            dialogWindowView.DataContext = this;
            Message = $"Уверены, что хотите удалить пользователя {CurrentUser.User.firstName} {CurrentUser.User.secondName}?\nВсе забронированные билеты будут автоматически удалены.";
            dialogWindowView.ShowDialog();
            if (dialogWindowView.DialogResult == true)
            {
                foreach (Ticket ticket in ticketCommand.getByUserId(user.id))
                    ticketCommand.delete(ticket);

                RAWindow rAWindow = new RAWindow();
                rAWindow.Show();
                (p as Window).Close();
                userCommand.delete(CurrentUser.User);
            }
        }

        #endregion


    }

}
