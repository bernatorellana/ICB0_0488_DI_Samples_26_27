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
    /// Lógica de interacción para CustomerEditPage.xaml
    /// </summary>
    public partial class CustomerEditPage : Page
    {
        public CustomerEditPage()
        {
            InitializeComponent();
        }


        private Client clientSeleccionat;
        private List<Client> llistaClients; 

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Dictionary<string, object> param = (Dictionary<string,object>)DataContext;

            clientSeleccionat = (Client) param["client_seleccionat"];
            llistaClients = (List<Client> ) param["llista_clients"];

            txtClient.Text = clientSeleccionat.Nom;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
           
            clientSeleccionat.Nom = txtClient.Text;
            
            this.FindAncestor<NavigationView>()?.
            Navigate(typeof(CustomersPage), llistaClients);

        }
    }
}
