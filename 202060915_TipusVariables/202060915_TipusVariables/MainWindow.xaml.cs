using System.Globalization;
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

namespace _202060915_TipusVariables
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
            int i = 0;
            float pes = 80.6f;
            decimal totalFactura = 12.23m;

            bool socMajorEdat = true;

            string nom = "Cristina Sanchez";
            nom += " Pérez";

            nom = nom.ToUpper();

            txtSortida.Text = nom;

            string[] fragments = nom.Split(" ");

            txtSortida.Text += "\n";

            for (int n = 0; n < fragments.Length; n++)
            {
                txtSortida.Text += fragments[n] + "\n";
            }
            foreach (String frag in fragments)
            {
                txtSortida.Text += frag + "\n";
            }

            // Conversions de tipus
            // el que sigui a string
            int posicio = 123;
            string posicioS = posicio.ToString();

            float PI = 3.14159f;
            String PIs = PI.ToString();
            txtSortida.Text += PIs + "\n";
            CultureInfo angles = new CultureInfo("en-US");
            PIs = PI.ToString("########.000", angles);
            txtSortida.Text += PIs;
            mostrar("");
            // Dates
            DateTime ara = DateTime.Now;
            DateTime avui = DateTime.Today;

            DateTime cumple = new DateTime(2000, 10, 21);
            String cumpleS = cumple.ToString();
            mostrar(cumpleS);

            cumpleS = cumple.ToString("dd/MMMM/yyy", angles);
            mostrar(cumpleS);

            CultureInfo rus = new CultureInfo("ru");
            List<string> mesos = new List<string>();

            // Mostrem tots els mesos de l'any en diversos idiomes
            for (int m = 1; m <= 12; m++)
            {
                DateTime data = new DateTime(2000, m, 21);
                string mes = data.ToString("MMMM", rus);
                mostrar(mes);
                mesos.Add(mes);
            }

            cboMesos.ItemsSource = mesos;
            cboMesos.SelectedIndex = 0;
            lsbMesos.ItemsSource = mesos;


            // Omplir el combobox d'idiomes

            List<CultureInfo> idiomes = new List<CultureInfo>();
            idiomes.Add(new CultureInfo("ru"));
            idiomes.Add(new CultureInfo("ja"));
            idiomes.Add(new CultureInfo("en"));
            cboIdioma.ItemsSource = idiomes;

        }

        private void mostrar(string cumpleS)
        {
            txtSortida.Text += cumpleS + "\n";
        }



        private void cboIdioma_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CultureInfo idiomaSeleccionat = (CultureInfo)cboIdioma.SelectedItem;

            List<string> mesos = new List<string>();

            // Mostrem tots els mesos de l'any en diversos idiomes
            for (int m = 1; m <= 12; m++)
            {
                DateTime data = new DateTime(2000, m, 21);
                string mes = data.ToString("MMMM", idiomaSeleccionat);
                mesos.Add(mes);
            }

            cboMesos.ItemsSource = mesos;

        }
    }
}