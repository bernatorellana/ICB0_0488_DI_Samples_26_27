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

namespace AppBuscamines
{




    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        int[,] tauler;

        const int MINA = -1;

        
        const int MIDA_TAULER = 10;
        const int NUM_MINES = MIDA_TAULER;

        public MainWindow()
        {
            InitializeComponent();
            inicialitzarTauler();
            pintaremMinesINumeros();
            posaremElsBotons();
        }
    }
}