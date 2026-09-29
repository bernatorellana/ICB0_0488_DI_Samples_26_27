using DiccionarisApp.model;
using System.Collections.ObjectModel;
using System.Diagnostics;
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

namespace DiccionarisApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private enum ESTAT
        {
            NOU,
            EDICIO
        }


        private ESTAT estat;

        private ESTAT Estat { get => estat; set
            {
                estat = value;
                actualitzaElsBotons();
            }
        }

        private void actualitzaElsBotons()
        {

            switch (estat)
            {
                case ESTAT.NOU:
                    btnNew.Visibility = Visibility.Hidden;
                    btnSave.Visibility = Visibility.Visible;
                    btnCancel.Visibility = Visibility.Visible;
                    break;
                case ESTAT.EDICIO:
                    btnNew.Visibility = Visibility.Visible;
                    btnSave.Visibility = Visibility.Visible;
                    btnCancel.Visibility = Visibility.Visible;
                    break;

            }
        }

        private ObservableCollection<Persona> persones = new ObservableCollection<Persona>();

   

        public MainWindow()
        {
            InitializeComponent();
            //          
            //Dictionary<String, Persona> personesPerNom =
            //    new Dictionary<String, Persona>();

            //Persona maria = new Persona("11111111H", "Maria", "Pérez Sánchez");
            //personesPerNom.Add("MARIA", maria);
            //personesPerNom["MARIA"] = maria;
            //personesPerNom["MARIA3"] = maria;

            //Persona buscada = personesPerNom["MARIA"];
            //Debug.WriteLine("Persona trobada:" + buscada);

            //if (personesPerNom.ContainsKey("MARIA3"))
            //{
            //    buscada = personesPerNom["MARIA3"];
            //}

            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cboEquips.ItemsSource = Equip.GetEquips();
            cboEquips.DisplayMemberPath = "Nom";

            lsvPersones.ItemsSource = persones;

            Estat = ESTAT.NOU;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (Estat == ESTAT.NOU)
            {
                bool isOk =     txtNIF.Text.Length == 8 && 
                                txtNom.Text.Length >= 2 && 
                                txtCognom.Text.Length >= 4;

                if (isOk)
                {
                    Persona nova = new Persona(txtNIF.Text, txtNom.Text, txtCognom.Text);
                    persones.Add(nova);
                }
            }
        }
    }
}