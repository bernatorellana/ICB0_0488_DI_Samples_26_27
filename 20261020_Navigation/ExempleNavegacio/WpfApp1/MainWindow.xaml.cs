using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp1.model;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void NavigationViewItem_Click(object sender, RoutedEventArgs e)
        {
        }

        private void navCustomers_Click(object sender, RoutedEventArgs e)
        {
            List<Client> clients = new List<Client>()
            {
                new Client(1, "Maria Sánchez"), 
                new Client(2, "Joan Pérez"), 
                new Client(3, "Pere Gimbernat"), 
                new Client(4, "Paco Mir"), 
                new Client(5, "Teresa Sans")
            };
            nav.Navigate(typeof(CustomersPage), clients);
        }
    }
}