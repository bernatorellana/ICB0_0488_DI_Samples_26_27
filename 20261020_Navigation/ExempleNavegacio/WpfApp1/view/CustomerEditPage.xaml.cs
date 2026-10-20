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
using WpfApp1.model;

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

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Client c = (Client)DataContext;
            txtClient.Text = c.Nom;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Client c = (Client)DataContext;
            c.Nom = txtClient.Text;
        }
    }
}
