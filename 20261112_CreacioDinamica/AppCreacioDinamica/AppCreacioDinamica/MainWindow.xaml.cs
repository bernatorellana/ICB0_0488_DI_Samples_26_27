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

namespace AppCreacioDinamica
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();


            //<Button Grid.Row="1" Grid.Column="0" FontSize="40" Click="Button_Click" Tag="7">[7]</Button>
            int row = 4;
            int col = 1;
            for(int i = 0; i < 10; i++, col++) { 
                Button b = new Button();
                b.FontSize = 40;
                b.Tag = i+"";
                b.Content = "["+i+"]";
                b.Click += Button_Click;
                
                Grid.SetColumn(b, col);
                Grid.SetRow(b, row);

                if (i == 0) {
                    row = 3;
                    col = -1;
                } else if (col == 2) { col = -1; row--; }

                grid.Children.Add(b);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            Button b = (Button)sender;
            String num = (String) b.Tag;
            txtPantalla.Text += num;
        }


    }
}