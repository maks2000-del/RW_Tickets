using System.Windows;
using Tickets.ViewModels;

namespace Tickets.Views
{
    /// <summary>
    /// Логика взаимодействия для DialogWindowView.xaml
    /// </summary>
    public partial class DialogWindowView : Window
    {
        public DialogWindowView()
        {
            InitializeComponent();
        }

        private void AcceptButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
