using System;
using System.Collections.Generic;
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
using Wpf.Ui.Controls;
using WpfApp1.model;
using WpfApp1.view.utils;

namespace WpfApp1
{
    /// <summary>
    /// Lógica de interacción para CustomersPage.xaml
    /// </summary>
    public partial class CustomersPage : Page
    {
        public CustomersPage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            List<Client> clients = (List < Client >) this.DataContext;
            lsbClients.ItemsSource = clients;
        }

        private void lsbClients_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            List<Client> clients = (List<Client>)this.DataContext;
            Client clientSeleccionat = (Client)lsbClients.SelectedItem;

            Dictionary<string, object> parametres = new Dictionary<string, object>();
            parametres["llista_clients"] = clients;
            parametres["client_seleccionat"] = clientSeleccionat;

            this.FindAncestor<NavigationView>()?.
                Navigate(typeof(CustomerEditPage), parametres);
        }
    }
}
