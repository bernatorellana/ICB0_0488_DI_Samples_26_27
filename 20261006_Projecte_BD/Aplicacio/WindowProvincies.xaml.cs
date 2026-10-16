using DAO;
using IDAO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using _20251001_Controls_Senders.model;

namespace Aplicacio
{
    /// <summary>
    /// Lógica de interacción para WindowProvincies.xaml
    /// </summary>
    public partial class WindowProvincies : Window
    {
        private ObservableCollection<Provincia> provincies; 
        public WindowProvincies()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {      
            MySQLFactory.getUOW(uow =>
            {
                IDAOProvincia dao_prov = uow.DAOProvincies;
                provincies =  dao_prov.GetProvincies("", "");
                dtgProvincies.ItemsSource = provincies;

            });
        }
    }
}
