using System.Globalization;
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
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ConversionsFromString
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

        private void txtNumero_TextChanged(object sender, TextChangedEventArgs e)
        {
            //lblNumero.Content = txtNumero.Text;
            /*try
            {
                int numero = Int32.Parse(txtNumero.Text);
                lblNumero.Content = "El número és :" + numero;
            }
            catch (Exception)
            {
                lblNumero.Content = "ERROR";
            }*/
            int numeroResultant;
            bool esNumero = Int32.TryParse(txtNumero.Text, out numeroResultant);


            mostraError(!esNumero, "Numero incorrecte", lblNumero, txtNumero);


        }

        private void txtPes_TextChanged(object sender, TextChangedEventArgs e)
        {
            double numeroResultant;
            NumberStyles style = NumberStyles.AllowDecimalPoint;
  
            CultureInfo culture = CultureInfo.CurrentCulture;

            bool esNumero = Double.TryParse(txtPes.Text, style, culture, out numeroResultant);

            mostraError(!esNumero, "pes incorrecte", lblPes, txtPes);
        }

        private void mostraError(bool hiHaError, string missatgeError, Label lblSortida , TextBox txt)
        {
            if(hiHaError)
            {
                lblSortida.Content = missatgeError;
                txt.Background = Brushes.Red;
            } else
            {
                lblSortida.Content = "";
                txt.Background = Brushes.White;
            }
        }


        private void txtData_TextChanged(object sender, TextChangedEventArgs e)
        {
            bool dataCorrecta = false;

            string dataS = txtData.Text;
            dataS= dataS.Replace("-", "/");

            try {
                DateTime data = DateTime.ParseExact(dataS, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                dataCorrecta = true;
            }
            catch (Exception)
            {
                dataCorrecta = false;
            }
          

            mostraError(!dataCorrecta, "Data incorrecta, format dd/mm/yyyy", lblData, txtData);

        }

        private void txtEmail_TextChanged(object sender, TextChangedEventArgs e)
        {
           bool correuCorrecte = Regex.IsMatch(txtEmail.Text,
                                    "^[a-z0-9\\.\\-_]+@([a-z]+\\.)+[a-z]{2,4}$");

            mostraError(!correuCorrecte, "Email incorrecte", lblEmail, txtEmail);

        }

        private void txtNIF_TextChanged(object sender, TextChangedEventArgs e)
        {
            bool NIFCorrecte = Regex.IsMatch(txtNIF.Text,
                          "^[0-9]{8}[A-Z]$");
            string error = "format incorrecte";
            if (NIFCorrecte)
            {
                char lletra = txtNIF.Text[txtNIF.Text.Length - 1];
                string numero = txtNIF.Text.Substring(0, txtNIF.Text.Length - 1);
                char lletraCalculada = CalcularNif(numero);
                if (lletra != lletraCalculada)
                {
                    NIFCorrecte = false;
                    error = "lletra incorrecta";
                }
            }
            mostraError(!NIFCorrecte, error, lblNif, txtNIF);

        }


        public static char CalcularNif(string numero)
        {
            const string lletres = "TRWAGMYFPDXBNJZSQVHLCKE";

            if (numero == null || !numero.All(char.IsDigit) || numero.Length != 8)
            {
                throw new ArgumentException(
                    "El número ha de contenir exactament 8 dígits");
            }

            int dni = int.Parse(numero);

            return lletres[dni % 23];
        }


    }
}