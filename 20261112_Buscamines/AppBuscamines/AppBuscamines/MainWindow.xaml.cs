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
        Image[,] tapes;

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
            tapes = new Image[MIDA_TAULER, MIDA_TAULER];

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


                    /*
                    <Border BorderThickness="1" BorderBrush="Black" >
                        <TextBlock HorizontalAlignment="Center"
                                   VerticalAlignment="Center"
                                   Text="9" FontFamily="./fonts/#Minesweeper" ></TextBlock>
                    </Border>

                    */
                    Border b = new Border();
                    b.BorderThickness = new Thickness(0.5);
                    b.BorderBrush = new SolidColorBrush(Colors.Black);
                    b.Background = new SolidColorBrush(Colors.LightGray);
                    TextBlock t = new TextBlock();
                    t.HorizontalAlignment = HorizontalAlignment.Center;
                    t.VerticalAlignment = VerticalAlignment.Center;
                    b.Child = t;

                    // Creem la imatge que tapa la mina
                    Image tapa = new Image();
                    tapa.Source = new BitmapImage(new Uri("pack://application:,,,/AppBuscamines;component/images/button.png"));

                    tapa.Tag = new Point(f, c);

                    tapes[f, c] = tapa; // Permet accedir a una tapa concreta a partir de les seves coordenades


                    tapa.MouseLeftButtonDown += Tapa_MouseLeftButtonDown;

                    // The font resource reference includes the base URI reference (application directory level),
                    // and a relative URI reference.

                    t.FontFamily = new FontFamily(new Uri("pack://application:,,,/"), "./fonts/#Minesweeper");

                    Color[] colors = { Colors.Blue, 
                                        Colors.Green, 
                                        Colors.Orange, 
                                        Colors.OrangeRed,
                                        Colors.DarkOrange,
                                        Colors.Black, Colors.Black,Colors.Black
                    };

                    if (tauler[f, c] == MINA)
                    {

                        //t.Text = "M";
                        Image mine = new Image();
                       
                        mine.Source = new BitmapImage(new Uri("pack://application:,,,/AppBuscamines;component/images/mine.png"));
                        b.Child = mine;
                        b.Background = new SolidColorBrush(Colors.Red);
                        
                    } else
                    {
                        int numero = tauler[f, c];
                        if (numero > 0) { 
                            t.Text = ""+numero;

                            t.Foreground = new SolidColorBrush(colors[numero-1]);

                            t.FontSize = 30;
                        } 
                    }

                    // Afegim el text o la mina al grid
                    grid.Children.Add(b);
                    Grid.SetColumn(b, c);
                    Grid.SetRow(b, f);

                    // Afegir la tapa
                    grid.Children.Add(tapa);
                    Grid.SetColumn(tapa, c);
                    Grid.SetRow(tapa, f);

                }
            }

         }

        private void Tapa_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            
            Image tapa = (Image)sender;
            
            Point p = (Point) tapa.Tag;
            //MessageBox.Show("He clicat a la fila " + p.X + " columna " + p.Y);
            
            if (tauler[(int)p.X,(int)p.Y]==MINA)
            {
                MessageBox.Show("You loose");
            } else if (tauler[(int)p.X, (int)p.Y] == 0) // Casella buida, he de propagar
            {
                propaga((int)p.X, (int)p.Y);
            }
        }

        private void propaga(int f, int c)
        {
            // Verificar si la cel·la ja està destapada, si és així no cal fer res
            if (tapes[f, c].Visibility == Visibility.Collapsed) return;

            // Si la cel·la està tapada, la destapem
            tapes[f, c].Visibility = Visibility.Collapsed;


            // Si la cel·la conté un número >0 ja hem acabat
            if (tauler[f, c] > 0) return;

            // Si la cel·la conté un zero, llavors propaguem a totes les seves 
            // veines
            int[,] mov = { 
                // T     D    L      R
                {-1,0},{1,0},{0,-1},{0,1},
                // TL    TR     BL      BR
                {-1,-1},{-1,1},{1,-1},{1,1},
            };
            for (int i = 0; i < 8; i++)
            {
                int fn = f + mov[i,0];
                int cn = c + mov[i,1];
                if(fn>=0 && cn>=0 && fn<MIDA_TAULER && cn<MIDA_TAULER)
                    propaga(fn, cn);
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