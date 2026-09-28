using DemoClassesCollections.model;
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

namespace DemoClassesCollections
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            List<string> llistaAmbArray = new List<string>();
            llistaAmbArray.Add("Maria");
            llistaAmbArray.Add("Joan");
            llistaAmbArray.Add("Pep");
            llistaAmbArray.Add("Francisco");
            llistaAmbArray.Add("Joan");

            //llistaAmbArray.RemoveAt(2);
            llistaAmbArray.Remove("Pep");
            foreach (string persona in llistaAmbArray)
            {
                showLog(persona);
            }

            // Experiments amb vehicles
            Vehicle ibiza = new Vehicle("1111XXX", "Ibiza", "Seat");
            Vehicle leon = new Vehicle("2222XXX", "Leon", "Seat");
            Vehicle golf = new Vehicle("3333XXX", "Golf", "Volkswagen");
            Vehicle berlingo = new Vehicle("4444XXX", "Berlingo", "Citroen");
            Vehicle polo = new Vehicle("5555XXX", "Polo", "Volkswagen");

            List<Vehicle> vehicles = new List<Vehicle>();
            vehicles.Add(ibiza);
            vehicles.Add(leon);
            vehicles.Add(golf);
            vehicles.Add(berlingo);
            vehicles.Add(polo);
            vehicles.Add(ibiza);

            ibiza.Matricula = "7777TTT";


            showLog("And the winner is: " + vehicles[5].Matricula);


            showLog("L'Ibiza està a : " + vehicles.IndexOf(ibiza));

            showLog("L'Ibiza està a : " + vehicles.IndexOf(
                    new Vehicle("7777TTT", "Ibiza", "Seat")
                ));

            vehicles.Remove(new Vehicle("7777TTT", "Ibiza", "Seat"));

            //=================================================================

            cboVehicles.ItemsSource = vehicles;
            cboVehicles.DisplayMemberPath = "FullDesc";
            lsvSeleccionats.ItemsSource = vehicles;
            //lsvSeleccionats.DisplayMemberPath = "FullDesc";

        }


        private void showLog(string txt)
        {
            txtSortida.AppendText(txt + "\n");
        }
    }
}