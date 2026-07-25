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

namespace Votantes
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        private readonly string[] partidos = { "Buhito", "Aguila", "Torito", "Lorito" };
        private readonly string[] zonas = { "A", "B", "C", "D" };

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txt00.Text = "122";
            txt01.Text = "254";
            txt02.Text = "382";
            txt03.Text = "445";

            txt10.Text = "472";
            txt11.Text = "364";
            txt12.Text = "205";
            txt13.Text = "228";

            txt20.Text = "143";
            txt21.Text = "117";
            txt22.Text = "474";
            txt23.Text = "293";

            txt30.Text = "411";
            txt31.Text = "202";
            txt32.Text = "261";
            txt33.Text = "335";
        }

        private void btnCalcular_Click(object sender, RoutedEventArgs e)
        {
            int[,] votos = new int[4, 4];

            TextBox[,] cajasTexto = { { txt00, txt01, txt02, txt03 }, { txt10, txt11, txt12, txt13 },
                { txt20, txt21, txt22, txt23 }, { txt30, txt31, txt32, txt33 }
            };

            TextBlock[] tbTotalesPartidos = { tbTotalP0, tbTotalP1, tbTotalP2, tbTotalP3 };
            TextBlock[] tbTotalesZonas = { tbTotalZ0, tbTotalZ1, tbTotalZ2, tbTotalZ3 };

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    int.TryParse(cajasTexto[i, j].Text, out votos[i, j]);
                }
            }

            int[] totalPartidos = new int[4];
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    totalPartidos[i] += votos[i, j];
                }
                tbTotalesPartidos[i].Text = totalPartidos[i].ToString();
            }

            int[] totalZonas = new int[4];
            for (int j = 0; j < 4; j++)
            {
                for (int i = 0; i < 4; i++)
                {
                    totalZonas[j] += votos[i, j];
                }
                tbTotalesZonas[j].Text = totalZonas[j].ToString();
            }

            int totalGeneral = 0;
            foreach (int total in totalPartidos)
            {
                totalGeneral += total;
            }
            tbTotalVotantes.Text = totalGeneral.ToString();

            int maxVotosPartido = totalPartidos[0];
            int indGanador = 0;
            for (int i = 1; i < 4; i++)
            {
                if (totalPartidos[i] > maxVotosPartido)
                {
                    maxVotosPartido = totalPartidos[i];
                    indGanador = i;
                }
            }
            lblCandidatoGanador.Text = partidos[indGanador];

            int maxVotosZona = totalZonas[0];
            int indZonaMax = 0;
            for (int j = 1; j < 4; j++)
            {
                if (totalZonas[j] > maxVotosZona)
                {
                    maxVotosZona = totalZonas[j];
                    indZonaMax = j;
                }
            }
            lblZonaMax.Text = zonas[indZonaMax];

            foreach (TextBox caja in cajasTexto)
            {
                caja.IsEnabled = false; 
            }

            btnCalcular.IsEnabled = false;
        }
    }
}