using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    class AdminSeatsViewModel : BaseViewModel
    {
        SeatCommand seatCommand = new SeatCommand();
        CityCommand cityCommand = new CityCommand();
        Seat_typeCommand seat_TypeCommand = new Seat_typeCommand();

        private Tickets_db context = new Tickets_db();

        private string city_of_departure;
        private string city_of_arrival;
        private string date_of_departure;
        private string time_of_departure;
        private string type;
        private string carriage_description = "";
        private string info;
        private string message;
        private string header;

        private string statusСity_of_departure;
        private string statusСity_of_arrival;
        private string statusDate_of_departure;
        private string statusTime_of_departure;
        private string statusType;

        List<string> tmpCitys = new List<string>();
        List<string> tmpSeat_types = new List<string>();

        ObservableCollection<string> tmpDates = new ObservableCollection<string>();
        ObservableCollection<string> tmpTimes = new ObservableCollection<string>();
        #region fields
        public string City_of_departure
        {
            get { return city_of_departure; }
            set { city_of_departure = value;
                if (value != null)
                    statusСity_of_departure = "";
                ClearFields(1); GetFileds(); OnPropertyChanged("City_of_departure"); }
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
                    ClearFields(1); GetFileds();
                }
                else
                if (value != City_of_departure)
                {
                    city_of_arrival = value;
                    if (value != null)
                        statusСity_of_arrival = "";
                    ClearFields(1); GetFileds();
                }
                else MessageBox.Show("не шали");
                OnPropertyChanged("City_of_arrival");
            }
        }
        public string Date_of_departure
        {
            get { return date_of_departure; }
            set
            { date_of_departure = value;
                if (value != null)
                    statusDate_of_departure = ""; 
                ClearFields(2); GetFileds(); OnPropertyChanged("Date_of_departure"); }
        }
        public string Time_of_departure
        {
            get { return time_of_departure; }
            set {
                
                    time_of_departure = value;
                if (value != null)
                    statusTime_of_departure = ""; 
                GetFileds(); OnPropertyChanged("Time_of_departure"); }
        }
        public string Type
        {
            get { return type; }
            set
            {
                
                type = value; 
                if (City_of_departure != null && City_of_departure != "Все")
                {
                    if (value != null)
                        statusType = "";
                    if (value == "Плацкарт")
                        Сarriage_description = "По выбранному маршруту будет добавлено\n20 мест типа Плацкарт";
                    else if (value == "Эконом класс")
                        Сarriage_description = "По выбранному маршруту будет добавлено\n30 мест типа Эконом класс";
                    else if(value == "Бизнес класс")
                        Сarriage_description = "По выбранному маршруту будет добавлено\n20 мест типа Бизнесс класс";
                }
                else carriage_description = "нихуя";
                OnPropertyChanged("Type");
            }
        }
        public string Сarriage_description
        {
            get { return carriage_description; }
            set { carriage_description = value; OnPropertyChanged("Сarriage_description"); }
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

        #region collections
        public List<string> Citys
        {
            get
            {
                return tmpCitys;
            }

        }
        public List<string> Seat_types
        {
            get
            {
                return tmpSeat_types;
            }
        }
        public ObservableCollection<string> Dates
        {
            get
            {
                return tmpDates;
            }
        }
        public ObservableCollection<string> Times
        {
            get
            {
                return tmpTimes;
            }
        }
        #endregion
        public AdminSeatsViewModel()
        {
            tmpCitys = cityCommand.getCitys();
            tmpSeat_types = seat_TypeCommand.getSeat_types().Distinct().ToList();
            RefreshStatusDefault();

            #region Commands

            AddSeatCommand = new RelayCommand(OnAddSeatCommandExecuted, CanAddSeatCommandExecute);
            ClearTicketFieldCommand = new RelayCommand(OnClearTicketFieldCommandExecuted, CanClearTicketFieldCommandExecute);
            #endregion
        }

        #region Commands executhion
        public ICommand AddSeatCommand { get; }
        private bool CanAddSeatCommandExecute(object p) => IsCorrected();
        private void OnAddSeatCommandExecuted(object p)
        {
            string voyage_id = seatCommand.get_voyage_id(City_of_departure, City_of_arrival, Date_of_departure, Time_of_departure).ToString();
            if (voyage_id != "0")
            {
                int voyage_id_num = Convert.ToInt32(voyage_id);
                int type_num = seat_TypeCommand.getIdSeat_type(Type);
                InfoWindowView infoWindowView = new InfoWindowView();
                infoWindowView.DataContext = this;
                Header = "Добавление новых мест";

                if (Type == "Плацкарт")
                {
                    int carrige = 0;

                    foreach (Seat seat in context.Seats)
                    {
                        //проверка если нет совпадений
                        if (seat.voyage_id == voyage_id_num && seat.type_of_seat == type_num)
                        {
                            if (seat.num_of_carriage >= carrige)
                                carrige = Convert.ToInt32(seat.num_of_carriage);
                        }
                    }
                    carrige = ++carrige;

                    //нахождение вагона
                    if (carrige != 0)
                    {

                        for (int i = 1; i <= 20; i++)
                        {
                            Seat seat = new Seat(voyage_id_num, type_num, carrige, i);

                            seatCommand.add_seat(seat);
                        }


                        Message = $"Вагон №{carrige} на 20 мест по маршруту {seatCommand.getByVoyageId(voyage_id_num).name} был успешно добавлен!";
                    }
                }
                else if (Type == "Эконом класс")
                {
                    int carrige = 0;

                    foreach (Seat seat in context.Seats)
                    {
                        //проверка если нет совпадений
                        if (seat.voyage_id == voyage_id_num && seat.type_of_seat == type_num)
                        {
                            if (seat.num_of_carriage >= carrige)
                                carrige = Convert.ToInt32(seat.num_of_carriage);
                        }
                    }
                    carrige = ++carrige;

                    //нахождение вагона
                    if (carrige != 0)
                    {

                        for (int i = 1; i <= 30; i++)
                        {
                            Seat seat = new Seat(voyage_id_num, type_num, carrige, i);

                            seatCommand.add_seat(seat);
                        }


                        Message = $"Вагон №{carrige} на 30 мест по маршруту {seatCommand.getByVoyageId(voyage_id_num).name} был успешно добавлен!";
                    }
                }
                else if (Type == "Бизнес класс")
                {
                    int carrige = 0;

                    foreach (Seat seat in context.Seats)
                    {
                        //проверка если нет совпадений
                        if (seat.voyage_id == voyage_id_num && seat.type_of_seat == type_num)
                        {
                            if (seat.num_of_carriage >= carrige)
                                carrige = Convert.ToInt32(seat.num_of_carriage);
                        }
                    }
                    carrige = ++carrige;

                    //нахождение вагона
                    if (carrige != 0)
                    {

                        for (int i = 1; i <= 20; i++)
                        {
                            Seat seat = new Seat(voyage_id_num, type_num, carrige, i);

                            seatCommand.add_seat(seat);
                        }


                        Message = $"Вагон №{carrige} на 20 мест по маршруту {seatCommand.getByVoyageId(voyage_id_num).name} был успешно добавлен!";
                    }
                }

                infoWindowView.ShowDialog();
                ClearFields(0);

                RefreshStatusDefault();
            }
            else
                MessageBox.Show("0ишбка");
        }

        public ICommand ClearTicketFieldCommand { get; }
        private bool CanClearTicketFieldCommandExecute(object p) => true;
        private void OnClearTicketFieldCommandExecuted(object p)
        {
            ClearFields(0);
            RefreshStatusDefault();
        }
        #endregion
        public void GetFileds()
        {
            #region get fields
            int city_id_out = 0, city_id_in = 0, voyage_id = 0;
            foreach (City city in context.Citys)
            {

                //проверка если нет совпадений
                if (city.city1 == City_of_departure)
                {
                    city_id_out = city.id; break;
                }
            }
            foreach (City city in context.Citys)
            {
                //проверка если нет совпадений
                if (city.city1 == City_of_arrival)
                {
                    city_id_in = city.id; break;
                }
            }
            foreach (Voyage voyage in context.Voyages)
            {
                if ((voyage.id_city___of_departure == city_id_out) && (voyage.id_city___of_arrival == city_id_in) && (voyage.date_of_departure == date_of_departure))
                {
                    voyage_id = voyage.id;
                }
            }
            #endregion

            if ((city_id_out != 0) && (city_id_in != 0))
            {
                foreach (Voyage voyage in context.Voyages)
                {
                    if (voyage.id_city___of_departure == city_id_out && voyage.id_city___of_arrival == city_id_in)
                    {
                        if (!tmpDates.Contains(voyage.date_of_departure))
                            tmpDates.Add(voyage.date_of_departure);

                    }
                }
                foreach (Voyage voyage in context.Voyages)
                {
                    if (voyage.id_city___of_departure == city_id_out && voyage.id_city___of_arrival == city_id_in && voyage.date_of_departure == date_of_departure)
                    {
                        if (!tmpTimes.Contains(voyage.time_of_departure))
                            tmpTimes.Add(voyage.time_of_departure);

                    }
                }

            }
        }
        bool IsCorrected()
        {
            if (!String.IsNullOrEmpty(statusСity_of_departure))
            {
                Info = statusСity_of_departure;
                return false;
            }
            else
            if (!String.IsNullOrEmpty(statusСity_of_arrival))
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
            if (!String.IsNullOrEmpty(statusTime_of_departure))
            {
                Info = statusTime_of_departure;
                return false;
            }
            else
            if (!String.IsNullOrEmpty(statusType))
            {
                Info = statusType;
                return false;
            }
            else
            {
                Info = "";
                return true;
            }
        }
        public void ClearFields(int level)
        {
            switch (level)
            {
                case 0:
                    City_of_departure = null;
                    City_of_arrival = null;
                    Type = null;

                    tmpDates.Clear();
                    Date_of_departure = null;
                    tmpTimes.Clear();
                    Time_of_departure = null;
                    Сarriage_description = "";
                    break;
                case 1:
                    tmpDates.Clear();
                    Date_of_departure = null;
                    tmpTimes.Clear();
                    Time_of_departure = null;
                    Type = null;
                    break;
                case 2:
                    tmpTimes.Clear();
                    Time_of_departure = null;
                    Type = null;
                    break;
                default:
                    MessageBox.Show("Ты ч0 творишь??! Успокойся");
                    break;
            }
        }
        private void RefreshStatusDefault()
        {
            statusСity_of_departure = "Не выбран город отправления";
            statusСity_of_arrival = "Не выбран город прибытия";
            statusDate_of_departure = "Не выбрана дата отъезда";
            statusTime_of_departure = "Не выбрано время отъезда";
            statusType = "Не выбран тип места";
        }
    }
}
