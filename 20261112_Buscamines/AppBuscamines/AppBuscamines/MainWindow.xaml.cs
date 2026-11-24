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

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            inicialitzarTauler();
            //pintaremMinesINumeros();
            //posaremElsBotons();
        }

        private void inicialitzarTauler()
        {
            tauler = new int[MIDA_TAULER, MIDA_TAULER];

            for (int c = 0; c < MIDA_TAULER; c++)
            {
                ColumnDefinition cd = new ColumnDefinition();
                cd.Width = new GridLength(40);
                grid.ColumnDefinitions.Add(cd);

                RowDefinition rd = new RowDefinition();
                rd.Height= new GridLength(40);
                grid.RowDefinitions.Add(rd);
            }
            int minesColocades = 0;
            while (minesColocades < NUM_MINES)
            {
                int c = (int)(Random.Shared.NextDouble() * MIDA_TAULER);
                int f = (int)(Random.Shared.NextDouble() * MIDA_TAULER);
                if (tauler[f, c] != MINA)
                {
                    tauler[f, c] = MINA;

                    updateComptadorMina(f, c);

                    minesColocades++;
                }
            }
            for (int f = 0; f < MIDA_TAULER; f++)
            {
                for (int c = 0; c < MIDA_TAULER; c++)
                {
                    TextBlock t = new TextBlock();
                    if (tauler[f, c] == MINA)
                    {
                        
                        t.Text = "M";
                        
                    } else
                    {
                        t.Text = ""+tauler[f, c];
                    }
                    grid.Children.Add(t);
                    Grid.SetColumn(t, c);
                    Grid.SetRow(t, f);
                }
            }
         }

        private void updateComptadorMina(int f, int c)
        {
            int[,] mov = { 
                // T     D    L      R
                {-1,0},{1,0},{0,-1},{0,1},
                // TL    TR     BL      BR
                {-1,-1},{-1,1},{1,-1},{1,1},
            };
            for(int i = 0; i < 8; i++)
            {
                int f_v = f + mov[i, 0];
                int c_v = c + mov[i, 1];
                if(! (f_v < 0 || c_v < 0 || f_v >= MIDA_TAULER || c_v >= MIDA_TAULER) )
                {
                    if(tauler[f_v, c_v]!=MINA)  tauler[f_v, c_v]++;
                }


            }

        }
    }
}