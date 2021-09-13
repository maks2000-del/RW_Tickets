using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Tickets.ViewModels;
using Tickets.Commands;
using Tickets.DB;
using System.Windows;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows.Input;
using Tickets.Views;
using Tickets.DB;

namespace Tickets.ViewModels
{
    public class AddTicketViewModel : BaseViewModel
    {
        
        TicketCommand ticketCommand = new TicketCommand();
        CityCommand cityCommand = new CityCommand();
        GetInfoCommand getInfoCommand = new GetInfoCommand();
        Seat_typeCommand seat_typeCommand = new Seat_typeCommand();
        SeatCommand seatCommand = new SeatCommand();
        private Tickets_db context = new Tickets_db();

        private string city_of_departure;
        private string city_of_arrival;
        private string date_of_departure;
        private string time_of_departure;
        private string type;
        private int carrige;
        private int seat;
        private string cost = "Цена: ";
        private string info;
        private string message;
        private string header;

        private string statusСity_of_departure;
        private string statusСity_of_arrival;
        private string statusDate_of_departure;
        private string statusTime_of_departure;
        private string statustype;
        private string statusCarrige;
        private string statusSeat;

        List<string> tmpSeat_types = new List<string>();
        List<string> tmpCitys = new List<string>();

        //ObservableCollection<string> tmpCitys_in = new ObservableCollection<string>();
        ObservableCollection<string> tmpDates = new ObservableCollection<string>();
        ObservableCollection<string> tmpTimes = new ObservableCollection<string>();
        ObservableCollection<string> tmpCarriages = new ObservableCollection<string>();
        ObservableCollection<string> tmpSeats = new ObservableCollection<string>();

        #region fileds
        public string City_of_departure
        {
            get { return city_of_departure; }
            set 
            {
                
                city_of_departure = value;
                if(value != null)
                statusСity_of_departure = "";
                ClearFields(1); GetFileds();
                OnPropertyChanged("City_of_departure");
                
            }
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
            set { date_of_departure = value;
                if (value != null)
                    statusDate_of_departure = "";
                ClearFields(2); GetFileds();
                
                OnPropertyChanged("Date_of_departure"); }
        }
        public string Time_of_departure
        {
            get { return time_of_departure; }
            set { time_of_departure = value;
                if (value != null)
                    statusTime_of_departure = "";
                ClearFields(3); GetFileds(); OnPropertyChanged("Time_of_departure"); }
        }
        public string Type
        {
            get { return type; }
            set { type = value;
                if (value != null)
                    statustype = ""; 
                ClearFields(4); GetFileds();

                if (City_of_departure != null && City_of_departure != "Все" && City_of_arrival != null && City_of_arrival != "Все" )
                {
                    string text_cost = seatCommand.getByVoyageId(seatCommand.get_any_voyage_id(City_of_departure, City_of_arrival)).cost.ToString();
                    text_cost = text_cost.Remove(2, text_cost.Length - 2);
                    Cost = "Цена: " + text_cost + "p";
                }
                else Cost = "Цена:";

                OnPropertyChanged("Type"); }
        }
        public string Carrige
        {
            get { return carrige.ToString(); }
            set {
                if (value != "")
                {
                    carrige = Convert.ToInt32(value);
                    if (value != null)
                        statusCarrige = "";

                    ClearFields(5);
                    GetFileds(); OnPropertyChanged("Carrige");
                }
            }
        }
        public string Seat
        {
            get { return seat.ToString(); }
            set {
                if (value != "")
                {
                    seat = Convert.ToInt32(value);
                    if (value != null)
                        statusSeat = "";
                    GetFileds(); OnPropertyChanged("Seat");
                }
            }
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
        #endregion

        #region collections
        public List<string> Citys
        {
            get
            {
                return tmpCitys; 
            }
        }
        public ObservableCollection<string> Dates
        {
            get
            {
                return tmpDates;
            }
        }
        public List<string> Seat_types
        {
            get 
            {
                return tmpSeat_types; 
            }
        }
        //public ObservableCollection<string> Citys_in
        //{
        //    get 
        //    {
        //        return tmpCitys_in; 
        //    }
        //}
        public ObservableCollection<string> Times
        {
            get 
            {
                return tmpTimes;
            }
        }
        public ObservableCollection<string> Carriages
        {
            get 
            {
                return tmpCarriages;
            }
        }
        public ObservableCollection<string> Seats
        {
            get 
            {
                return tmpSeats; 
            }
        }
        #endregion

        public AddTicketViewModel()
        {
            tmpSeat_types = seat_typeCommand.getSeat_types().Distinct().ToList();
            tmpCitys = cityCommand.getCitys();

            RefreshStatusDefault();
            #region Commands

            AddTicketCommand = new RelayCommand(OnAddTicketCommandExecuted, CanAddTicketCommandExecute);
            ClearTicketFieldCommand = new RelayCommand(OnClearTicketFieldCommandExecuted, CanClearTicketFieldCommandExecute);

            #endregion
        }
        public void GetFileds()
        {
            #region get fields
            int city_id_out = 0, city_id_in = 0, voyage_id = 0, seat_type_id = 0;
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
            foreach (Seat_types seat_type in context.Seat_types)
            {
                if (seat_type.seat_type == Type)
                {
                    seat_type_id = seat_type.id;
                }
            }
            #endregion

            //foreach (City city in context.Citys)
            //{
            //    if (city.city1 != City_of_departure && !Citys_in.Contains(city.city1))
            //    Citys_in.Add(city.city1);
            //}

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
                foreach (Seat seat in context.Seats)
                {
                    if (seat.type_of_seat == seat_type_id && seat.voyage_id == voyage_id)
                    {
                        if (!tmpCarriages.Contains(seat.num_of_carriage.ToString()))
                            tmpCarriages.Add(seat.num_of_carriage.ToString());
                    }

                }
                foreach (Seat seat in context.Seats)
                {
                    if (seat.type_of_seat == seat_type_id && seat.voyage_id == voyage_id && seat.num_of_carriage == carrige)
                    {
                        if ((seat.is_free == true) && (!tmpSeats.Contains(seat.num_of_seat.ToString())))
                            tmpSeats.Add(seat.num_of_seat.ToString());
                    }

                }
            }
        }
        
        #region Commands executhion
        public ICommand AddTicketCommand { get; }
        private bool CanAddTicketCommandExecute(object p) => IsCorrected();
        private void OnAddTicketCommandExecuted(object p)
        {
            
            Ticket ticket = new Ticket(seatCommand.get_voyage_id(City_of_departure, City_of_arrival, Date_of_departure, Time_of_departure), seatCommand.get_seat_id(City_of_departure, City_of_arrival, Type, Seat, Carrige, Date_of_departure, Time_of_departure));
            //if (City_of_departure != "Все" && City_of_arrival != "Все" && Type != "Все") { 
                ticketCommand.add(ticket);

                //сделать if free = false и feee = true когда удалил билет
                seatCommand.update(seatCommand.getById(seatCommand.get_seat_id(City_of_departure, City_of_arrival, Type, Seat, Carrige, Date_of_departure, Time_of_departure)));

                InfoWindowView infoWindowView = new InfoWindowView();
                infoWindowView.DataContext = this;
                Header = "Бронирование нового билета";
                Message = $"билет успешно добавлен.\n\n{getInfoCommand.GetFullTicketInfo(ticket)}";
                infoWindowView.ShowDialog();
                ClearFields(0);

                RefreshStatusDefault();
            //}
        }

        public ICommand ClearTicketFieldCommand { get; }
        private bool CanClearTicketFieldCommandExecute(object p) => true;
        private void OnClearTicketFieldCommandExecuted(object p)
        {
            ClearFields(0);
            RefreshStatusDefault();
        }
        #endregion


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
                    tmpCarriages.Clear();
                    Carrige = null;
                    tmpSeats.Clear();
                    Seat = null;
                    break;
                case 1:
                    //tmpCitys_in.Clear();
                    tmpCarriages.Clear();
                    Carrige = null;
                    tmpSeats.Clear();
                    Seat = null;
                    tmpDates.Clear();
                    Date_of_departure = null;
                    tmpTimes.Clear();
                    Time_of_departure = null;
                    Type = null;
                    break;
                case 2:
                    tmpCarriages.Clear();
                    Carrige = null;
                    tmpSeats.Clear();
                    Seat = null;
                    tmpTimes.Clear();
                    Time_of_departure = null;
                    Type = null;
                    break;
                case 3:
                    tmpCarriages.Clear();
                    Carrige = null;
                    tmpSeats.Clear();
                    Seat = null;
                    Type = null;
                    break;
                case 4:
                    tmpCarriages.Clear();
                    Carrige = null;
                    tmpSeats.Clear();
                    Seat = null;
                    break;
                case 5:
                    tmpSeats.Clear();
                    Seat = null;
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
            statustype = "Не выбран тип места";
            statusCarrige = "Не выбран номер вагона";
            statusSeat = "Не выбран номер места";
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
            if (!String.IsNullOrEmpty(statusDate_of_departure) && Date_of_departure != "")
            {
                Info = statusDate_of_departure;
                return false;
            }
            else
            if (!String.IsNullOrEmpty(statusTime_of_departure) && Time_of_departure != "")
            {
                Info = statusTime_of_departure;
                return false;
            }
            else
            if (!String.IsNullOrEmpty(statustype) && Type != "Все")
            {
                Info = statustype;
                return false;
            }
            else
            if (!String.IsNullOrEmpty(statusCarrige) && Carrige != "")
            {
                Info = statusCarrige;
                return false;
            }
            else
            if (!String.IsNullOrEmpty(statusSeat) && Seat != "")
            {
                Info = statusSeat;
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
