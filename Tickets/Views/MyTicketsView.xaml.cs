using System;
using System.Windows.Controls;
using System.Windows.Data;
using Tickets.ViewModels;

namespace Tickets.Views
{
    /// <summary>
    /// Логика взаимодействия для MyTicketsView.xaml
    /// </summary>
    public partial class MyTicketsView : UserControl
    {
         MyTicketsViewModel myTicketsViewModel = new MyTicketsViewModel();
        
        public MyTicketsView()
        {
            InitializeComponent();

            DataContext = myTicketsViewModel;
        }
    }
}
