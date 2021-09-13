using System;
using System.Collections.Generic;
using System.Linq;
using Tickets.ViewModels;
using System.Windows.Controls;

namespace Tickets.Views
{
    /// <summary>
    /// Логика взаимодействия для AccountView.xaml
    /// </summary>
    public partial class AccountView : UserControl
    {
        AccountViewModel accountViewModel = new AccountViewModel();
        public AccountView()
        {
            InitializeComponent();
            DataContext = accountViewModel;
        }
    }
}
