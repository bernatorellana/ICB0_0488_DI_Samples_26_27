using DiccionarisApp.model;
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
        public MainWindow()
        {
            InitializeComponent();
            //          
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
    }
}