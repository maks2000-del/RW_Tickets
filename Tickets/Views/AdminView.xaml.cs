using System.Windows.Controls;
using System.Windows.Data;
using Tickets.DB;
using Tickets.ViewModels;

namespace Tickets.Views
{
    /// <summary>
    /// Логика взаимодействия для AdminView.xaml
    /// </summary>
    public partial class AdminView : UserControl
    {
        User User = CurrentUser.User;
        AdminViewModel adminViewModel = new AdminViewModel();
        public AdminView()
        {
            InitializeComponent();

            DataContext = adminViewModel;

        }

    }
}
