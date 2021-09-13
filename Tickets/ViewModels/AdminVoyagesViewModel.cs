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
    class AdminVoyagesViewModel : BaseViewModel
    {
        SeatCommand seatCommand = new SeatCommand();
        CityCommand cityCommand = new CityCommand();
        GetInfoCommand getInfoCommand = new GetInfoCommand();
        private Tickets_db context = new Tickets_db();

        private string city_of_daparture;
        private string city_of_arrival;
        private string date_of_daparture;
        private string date_of_arrival;
        private string time_of_daparture;
        private string time_of_arrival;
        private string voyage_name;
        private string info;
        private string message;
        private string header;
        private string cost;

        private string statusСity_of_departure;
        private string statusСity_of_arrival;
        private string statusDate_of_departure;
        private string statusDate_of_arrival;
        private string statusTime_of_departure;
        private string statusTime_of_arrival;
        private string statusVoyage_name;
        

        List<string> tmpCitys = new List<string>();

        public string City_of_departure
        {
            get { return city_of_daparture; }
            set { city_of_daparture = value; statusСity_of_departure = "";

                

                OnPropertyChanged("City_of_departure"); }
        }
        public string City_of_arrival
        {
            get { return city_of_arrival; }
            set
            {
                if (value == null)
                {
                    city_of_arrival = value;
                    if (value != null)
                        statusСity_of_arrival = "";
                }
                else
                if (value != City_of_departure)
                {
                    city_of_arrival = value;
                    if (value != null)
                        statusСity_of_arrival = "";
                }
                else MessageBox.Show("не шали");
                OnPropertyChanged("City_of_arrival");
            }
        }
        public string Time_of_departure
        {
            get { return time_of_daparture; }
            set
            {
                string correct_time = @"\d{2}:\d{2}:\d{2}";
                if (Regex.IsMatch(value, correct_time))
                {
                    time_of_daparture = value; statusTime_of_departure = "";
                    OnPropertyChanged("Time_of_departure");
                }
            }
        }
        public string Time_of_arrival
        {
            get { return time_of_arrival; }
            set
            {
                string correct_time = @"\d{2}:\d{2}:\d{2}";
                if (Regex.IsMatch(value, correct_time))
                {
                    time_of_arrival = value; statusTime_of_arrival = "";
                    OnPropertyChanged("Time_of_arrival");
                }
            }
        }
        public string Date_of_daparture
        {
            get
            { return date_of_daparture; }
            set
            {
                if (value.Length != 0)
                {
                    date_of_daparture = value.Substring(0, value.Length - 12);
                    statusDate_of_departure = "";

                    if (!String.IsNullOrEmpty(City_of_departure) && !String.IsNullOrEmpty(City_of_arrival) && value != "0.0.0            " && Date_of_arrival != "0.0.0            ")
                    {
                        string text_cost = seatCommand.getByVoyageId(seatCommand.get_any_voyage_id(City_of_departure, City_of_arrival)).cost.ToString();
                        text_cost = text_cost.Remove(2, text_cost.Length - 2);
                        Cost = "Цена: " + text_cost + "p";
                    }

                    OnPropertyChanged("Date_of_daparture");
                }
            }
        }
        public string Date_of_arrival
        {
            get
            { return date_of_arrival; }
            set
            {
                if (value.Length != 0)
                {
                    date_of_arrival = value.Substring(0, value.Length - 12);
                    statusDate_of_arrival = "";

                    OnPropertyChanged("Date_of_arrival");
                }
            }
        }
        public string Voyage_name
        {
            get { return voyage_name; }
            set { voyage_name = value; statusVoyage_name = ""; OnPropertyChanged("Voyage_name"); }
        }
        public string Cost
        {
            get { return cost; }
            set { cost = value; OnPropertyChanged("Cost"); }
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

        public List<string> Citys
        {
            get
            {
                return tmpCitys;
            }
        }

        public AdminVoyagesViewModel()
        {
            tmpCitys = cityCommand.getCitys();
            RefreshStatusDefault();
            #region Commands

            AddVoyageCommand = new RelayCommand(OnAddVoyageCommandExecuted, CanAddVoyageCommandExecute);
            ClearTicketFieldCommand = new RelayCommand(OnClearTicketFieldCommandExecuted, CanClearTicketFieldCommandExecute);
            #endregion
        }

        #region Commands executhion
        public ICommand AddVoyageCommand { get; }
        private bool CanAddVoyageCommandExecute(object p) => IsCorrected();
        private void OnAddVoyageCommandExecuted(object p)
        {
            Voyage voyage = new Voyage(Voyage_name, cityCommand.getIdCity(City_of_departure), Date_of_daparture, Time_of_departure, cityCommand.getIdCity(City_of_arrival), Date_of_arrival, Time_of_arrival, Convert.ToDecimal(seatCommand.getByVoyageId(seatCommand.get_any_voyage_id(City_of_departure, City_of_arrival)).cost));
            if (City_of_departure != "Все" && City_of_arrival != "Все")
            {
                seatCommand.add_voyage(voyage);

                InfoWindowView infoWindowView = new InfoWindowView();
                infoWindowView.DataContext = this;
                Header = "Добавление нового маршрута";
                Message = $"Маршрут успешно добавлен.\n\n{getInfoCommand.GetFullVoyageInfo(voyage)}";
                infoWindowView.ShowDialog();

                ClearFileds();
                RefreshStatusDefault();
            }
        }
        public ICommand ClearTicketFieldCommand { get; }
        private bool CanClearTicketFieldCommandExecute(object p) => true;
        private void OnClearTicketFieldCommandExecuted(object p)
        {
            ClearFileds();
            RefreshStatusDefault();
        }
        #endregion
        private void ClearFileds()
        {
            City_of_departure = null;
            City_of_arrival = null;
            Date_of_daparture = "0.0.0            ";
            Date_of_arrival = "0.0.0            ";
            Time_of_departure = "00:00:00";
            Time_of_arrival = "00:00:00";
            Voyage_name = "";
            Cost = "";
        }
        private void RefreshStatusDefault()
        {
            statusСity_of_departure = "Не выбран город отправления";
            statusСity_of_arrival = "Не выбран город прибытия";
            statusDate_of_departure = "Не выбрана дата отъезда";
            statusDate_of_arrival = "Не выбрана дата приезда";
            statusTime_of_departure = "Не выбрано время отъезда";
            statusTime_of_arrival = "Не выбрано время приезда";
            statusVoyage_name = "Не выбрано название маршрута";
        }
        bool IsCorrected()
        {
            

            if (!String.IsNullOrEmpty(statusСity_of_departure) && City_of_departure != "Все")
            {
                Info = statusСity_of_departure;
                return false;
            }
            else
            if (!String.IsNullOrEmpty(statusСity_of_arrival) && City_of_arrival != "Все")
            {
                Info = statusСity_of_arrival;
                return false;
            }
            else
            if (!String.IsNullOrEmpty(statusDate_of_departure))
            {
                Info = statusDate_of_departure;
                return false;
            }
            else
            if (!String.IsNullOrEmpty(statusDate_of_arrival))
            {
                Info = statusDate_of_arrival;
                return false;
            }
            else
            if (!String.IsNullOrEmpty(statusTime_of_departure))
            {
                Info = statusTime_of_departure;
                return false;
            }
            else
            if (!String.IsNullOrEmpty(statusTime_of_arrival))
            {
                Info = statusTime_of_arrival;
                return false;
            }
            else
            {
                Info = "";
                return true;
            }
        }

    }
}
