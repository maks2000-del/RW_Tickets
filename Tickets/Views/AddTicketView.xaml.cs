using System;
using System.Windows.Controls;
using Tickets.ViewModels;

namespace Tickets.Views
{
    /// <summary>
    /// Логика взаимодействия для AddTicketView.xaml
    /// </summary>
    public partial class AddTicketView : UserControl
    {
        AddTicketViewModel addTicketViewModel = new AddTicketViewModel();
    
        public AddTicketView()
        {
            InitializeComponent();
            DataContext = addTicketViewModel;
        }
    }
}
