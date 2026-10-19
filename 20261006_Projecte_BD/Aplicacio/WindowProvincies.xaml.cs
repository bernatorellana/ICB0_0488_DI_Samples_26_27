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
            ferLlistat();
        }

        private void btnFiltrar_Click(object sender, RoutedEventArgs e)
        {
            ferLlistat();
        }

        private void btnFiltrarClean_Click(object sender, RoutedEventArgs e)
        {
            txtId.Text = "";
            txtNom.Text = "";
            ferLlistat();
        }

        private void ferLlistat()
        {
            MySQLFactory.getUOW(uow =>
            {
                IDAOProvincia dao_prov = uow.DAOProvincies;
                provincies = dao_prov.GetProvincies( txtId.Text, txtNom.Text);
                dtgProvincies.ItemsSource = provincies;

            });
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (dtgProvincies.SelectedItem != null)
            {
                Provincia seleccionada = (Provincia)dtgProvincies.SelectedItem;

                MySQLFactory.getUOW(uow =>
                {
                    IDAOProvincia dao_prov = uow.DAOProvincies;
                    IDAOClient dao_client = uow.DAOClients;

                    try
                    {
                        //hem de verificar si la província no té clients associats
                        if(dao_client.GetClients(idProvincia: seleccionada.Id).Count == 0){
                            dao_prov.DeleteProvincia(seleccionada.Id);
                            uow.Commit();
                        } 
                        else
                        {
                            MessageBox.Show("Província en us.");
                        }                        
                    }
                    catch (Exception)
                    {
                        uow.Rollback();
                        MessageBox.Show("Error esborrant província.");                      
                    }
                });
                ferLlistat();
            }

        }

        private void dtgProvincies_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Provincia actual = (Provincia) dtgProvincies.SelectedItem;

            if (actual != null)
            {
                txtNomEdicio.Text = actual.Nom;
            } else
            {
                txtNomEdicio.Text = "";
            }

        }

        private void btnDesar_Click(object sender, RoutedEventArgs e)
        {
            Provincia actual = (Provincia)dtgProvincies.SelectedItem;

            if (actual != null)
            {
                actual.Nom = txtNomEdicio.Text;///// !!!!!!!!!!

                MySQLFactory.getUOW(uow =>
                {
                    IDAOProvincia dao_prov = uow.DAOProvincies;
                    dao_prov.UpdateProvincia(actual);
                    uow.Commit();
                });
                ferLlistat();
            }
        }
    }
}
