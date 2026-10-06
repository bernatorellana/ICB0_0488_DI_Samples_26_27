using _20251001_Controls_Senders.model;
using DAO;
using IDAO;
using System.Collections.ObjectModel;
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

namespace Aplicacio
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }



        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ObservableCollection<Client> clientsActuals = null;
            int numeroClients = 0;

            MySQLFactory.getUOW(uow =>
            {
                IDAOClient dao = uow.DAOClients;
                clientsActuals = dao.GetClients("",""); 
                numeroClients = dao.GetNumeroClients("","");
            });

            dtgClients.ItemsSource = clientsActuals;
        }


    }
}