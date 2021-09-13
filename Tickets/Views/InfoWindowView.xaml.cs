using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Tickets.Views
{
    /// <summary>
    /// Логика взаимодействия для InfoWindowView.xaml
    /// </summary>
    public partial class InfoWindowView : Window
    {
        public InfoWindowView()
        {
            InitializeComponent();
        }
        void ok(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
