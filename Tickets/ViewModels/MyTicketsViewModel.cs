using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Tickets.Commands;
using Tickets.DB;
using Tickets.Views;

namespace Tickets.ViewModels
{
    public class MyTicketsViewModel : BaseViewModel
    {
        UserCommand userCommand = new UserCommand();
        TicketCommand ticketCommand = new TicketCommand();
        SeatCommand seatCommand = new SeatCommand();
        Seat_typeCommand seat_typeCommand = new Seat_typeCommand();
        GetInfoCommand getInfoCommand = new GetInfoCommand();
        Ticket ticket = new Ticket();
        ObservableCollection<Ticket> tmpTickets = new ObservableCollection<Ticket>();

        object selectedItem;
        private string message;
        private string header;
        int countActual;

        public ObservableCollection<Ticket> Tickets
        {
            get { return tmpTickets; }
        }

        public string CountActual
        {
            get { return $" Актуальные ( {countActual} )"; }
            set
            {
                countActual = Int32.Parse(value);
                OnPropertyChanged("CountActual");
            }
        }

        public object SelectedItem
        {
            get { return selectedItem; }
            set
            {
                selectedItem = value;
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
        public MyTicketsViewModel()
        {
            var some = true;
            OnUpdateTicketsCollectionCommandExecuted(some);

            #region Commands

            UpdateTicketsCollectionCommand = new RelayCommand(OnUpdateTicketsCollectionCommandExecuted, CanUpdateTicketsCollectionCommandExecute);
            TicketInformathionCommand = new RelayCommand(OnTicketInformathionCommandExecuted, CanTicketInformathionCommandExecute);
            DeleteTicketCommand = new RelayCommand(OnDeleteTicketCommandExecuted, CanDeleteTicketCommandExecute);

            #endregion
        }
        
        #region Commands executhion
        public ICommand UpdateTicketsCollectionCommand { get; }
        private bool CanUpdateTicketsCollectionCommandExecute(object p) => true;
        private void OnUpdateTicketsCollectionCommandExecuted(object p)
        {
            tmpTickets.Clear();

            foreach (Ticket ticket in ticketCommand.getByUserId(CurrentUser.User.id))
                tmpTickets.Add(ticket);

            CountActual = ticketCommand.getByUserId(CurrentUser.User.id).Count().ToString();
        }
        public ICommand TicketInformathionCommand { get; }
        private bool CanTicketInformathionCommandExecute(object p) => SelectedItem is Ticket;
        private void OnTicketInformathionCommandExecuted(object p)
        {
            if (SelectedItem != null)
            {
                Ticket ticket = (SelectedItem as Ticket);
                InfoWindowView infoWindowView = new InfoWindowView();
                infoWindowView.DataContext = this;
                Header = "Информация о билете";
                Message = getInfoCommand.GetFullTicketInfo(ticket);
                infoWindowView.ShowDialog();
            }
        }
        public ICommand DeleteTicketCommand { get; }
        private bool CanDeleteTicketCommandExecute(object p) => SelectedItem is Ticket;
        private void OnDeleteTicketCommandExecuted(object p)
        {
            DialogWindowView dialogWindow = new DialogWindowView();
            dialogWindow.DataContext = this;
            Header = "Отмена брони";
            Message = $"Уверены, что хотите отменить бронь билета {(SelectedItem as Ticket).id}?";
            dialogWindow.ShowDialog();
            int? a = (SelectedItem as Ticket).seat_id;
            if (dialogWindow.DialogResult == true)
            {

                ticketCommand.delete(SelectedItem as Ticket);
                seatCommand.update_bought(a);
            }
            var some = true;
            OnUpdateTicketsCollectionCommandExecuted(some);
        }
        #endregion

    }
}
