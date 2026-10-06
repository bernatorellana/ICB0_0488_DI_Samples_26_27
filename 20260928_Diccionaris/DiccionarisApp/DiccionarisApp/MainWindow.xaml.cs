using DiccionarisApp.model;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;
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


        private ObservableCollection<Persona> persones = new ObservableCollection<Persona>();
        private Dictionary<Equip, ObservableCollection<Persona>> equips = new Dictionary<Equip, ObservableCollection<Persona>>();


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

                    txtCognom.Clear();
                    txtNom.Clear();
                    txtNIF.Clear();


                    lsvPersones.SelectedItem = null;

                    break;
                case ESTAT.EDICIO:
                    btnNew.Visibility = Visibility.Visible;
                    btnSave.Visibility = Visibility.Visible;
                    btnCancel.Visibility = Visibility.Visible;
                    break;

            }
        }



        public MainWindow()
        {
            InitializeComponent();
            //exempleDiccionaris();
            
        }

        private void exempleDiccionaris()
        {

            Dictionary<String, Persona> personesPerNom =
                new Dictionary<String, Persona>();

            Persona maria = new Persona("11111111H", "Maria", "Pérez Sánchez");
            personesPerNom.Add("MARIA", maria);
            personesPerNom["MARIA"] = maria;
            personesPerNom["MARIA3"] = maria;

            Persona buscada = personesPerNom["MARIA"];
            Debug.WriteLine("Persona trobada:" + buscada);

            if (personesPerNom.ContainsKey("MARIA3"))
            {
                buscada = personesPerNom["MARIA3"];
            }

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cboEquips.ItemsSource = Equip.GetEquips();
            cboEquips.DisplayMemberPath = "Nom";

            lsvPersones.ItemsSource = persones;


            //inicialitzar el diccionari d'equips
            foreach(Equip eq in Equip.GetEquips())
            {
                ObservableCollection<Persona> jugadorsDeLequip = new ObservableCollection<Persona>();
                equips[eq] = jugadorsDeLequip;
            }

            Estat = ESTAT.NOU;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            bool isOk = validaFormPersona();

            if (Estat == ESTAT.NOU)
            {
                if (isOk)
                {
                    Persona nova = new Persona(txtNIF.Text, txtNom.Text, txtCognom.Text);
                    persones.Add(nova);

                    Estat = ESTAT.NOU;
                }
            }
            else if (Estat == ESTAT.EDICIO)
            {
                Persona seleccionada = lsvPersones.SelectedItem as Persona;
                if (seleccionada != null && isOk)
                {
                    seleccionada.NIF = txtNIF.Text;
                    seleccionada.Nom = txtNom.Text;
                    seleccionada.Cognoms = txtCognom.Text;
                    //Estat = ESTAT.NOU;
                }
            }
        }
        private bool validaFormPersona()
        {
            return validaNIF() &&
                    validaNom() &&
                    validaCognom() &&
                    validaNIFNoRepetit();
        }

        private bool validaNIFNoRepetit()
        {
            //foreach(Persona p in persones)
            //{
            //    if (p.NIF.Equals(txtNIF.Text)) return false;
            //}
            //return true;

            Persona? p = persones.SingleOrDefault(x => x.NIF.Equals(txtNIF.Text));
            bool valid = p == null;

            if (!valid) MessageBox.Show("No s'admenten NIFs repetits", "ERROR",
                MessageBoxButton.OK, MessageBoxImage.Error);

            return valid;
            
        }

        private bool validaCognom()
        {
            return txtCognom.Text.Length >= 4;
        }

        private bool validaNom()
        {
            return txtNom.Text.Length >= 2;
        }

        private bool validaNIF()
        {
            Regex r = new Regex("[0-9]{8}[A-Z]");
            return r.Match(txtNIF.Text).Success;
        }

        private void lsvPersones_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MostrarPersonaSeleccionada();

        }

        private void MostrarPersonaSeleccionada()
        {
            Persona seleccionada = lsvPersones.SelectedItem as Persona;
            if (seleccionada != null)
            {
                txtNIF.Text = seleccionada.NIF;
                txtNom.Text = seleccionada.Nom;
                txtCognom.Text = seleccionada.Cognoms;
                Estat = ESTAT.EDICIO;
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            MostrarPersonaSeleccionada();
        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            Estat = ESTAT.NOU;
        }

        private void btnMove_Click(object sender, RoutedEventArgs e)
        {
            Persona personaSelecionada = lsvPersones.SelectedItem as Persona;
            Equip equipSeleccionat = cboEquips.SelectedItem as Equip;

            if (equipSeleccionat!=null && personaSelecionada != null)
            {

                foreach(ObservableCollection<Persona> jugs in equips.Values)
                {
                    if (jugs.Remove(personaSelecionada)) break;
                }


                ObservableCollection<Persona> jugadors = equips[equipSeleccionat];
                if (!jugadors.Contains(personaSelecionada))
                {
                    jugadors.Add(personaSelecionada);
                }
            }
        }

        private void cboEquips_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cboEquips.SelectedItem != null)
            {
                Equip equipSeleccionat = cboEquips.SelectedItem as Equip;
                ObservableCollection<Persona> jugadors = equips[equipSeleccionat];
                lsvJugadors.ItemsSource = jugadors;
            }
        }

        private void txtNom_LostFocus(object sender, RoutedEventArgs e)
        {
            if(!validaNom())
            {
                txtNom.Background = new SolidColorBrush(Color.FromRgb(255,200,200));
            } else
            {
                txtNom.Background = Brushes.White;
            }
        }

        private void txtCognom_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!validaCognom())
            {
                txtCognom.Background = new SolidColorBrush(Color.FromRgb(255, 200, 200));
            }
            else
            {
                txtCognom.Background = Brushes.White;
            }
        }

        private void txtNom_GotFocus(object sender, RoutedEventArgs e)
        {
            txtNom.Background = Brushes.White;
        }

        private void txtCognom_GotFocus(object sender, RoutedEventArgs e)
        {
            txtCognom.Background = Brushes.White;
        }



        private void txtNIF_PreviewKeyDown(object sender, KeyEventArgs e)
        {

            string nomTecla = e.Key.ToString();
            char ultimaLletra = nomTecla.Last();
            bool esDigit = Char.IsDigit(ultimaLletra);
            bool esLletra = e.Key.ToString().Length == 1 &&
                "TRWAGMYFPDXBNJZSQVHLCKE".IndexOf(e.Key.ToString()) >= 0;


            bool esCursor =        e.Key == Key.Back 
                                || e.Key == Key.Delete 
                                || e.Key == Key.Up
                                || e.Key == Key.Down
                                || e.Key == Key.Left
                                || e.Key == Key.Right
                                || e.Key == Key.Tab
                    ;

            if (esCursor) return;
            if(txtNIF.SelectionStart < 8)
            {
                e.Handled = !esDigit;
            }
            else if (txtNIF.SelectionStart == 8)
            {
                e.Handled = !esLletra;
            } else
            {
                e.Handled = true;
            }

            //if (!esCursor && txtNIF.SelectionStart < 8?!esDigit: !esLletra)
            //{
            //    e.Handled = true;
            //}
        }

 
    }
}