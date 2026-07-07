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

namespace JuegoEdad
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int edadMinima;
        private int edadMaxima;
        private int edadAleatoria;
        private int contadorIntentos;

        Random random = new Random();
        public MainWindow()
        {
            
            InitializeComponent();
        }

        private void btnPrimerInt_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(txtMinimo.Text, out edadMinima))
            {
                MessageBox.Show("Por favor, ingrese un número válido para la edad mínima.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                txtMinimo.Focus();
                return;
            }

            if (!int.TryParse(txtMaximo.Text, out edadMaxima))
            {
                MessageBox.Show("Por favor, ingrese un número válido para la edad máxima.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                txtMaximo.Focus();
                return;
            }
            if (edadMaxima < 0)
            {
                MessageBox.Show("Por favor, ingrese un número válido para la edad máxima.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                
                return;
            }
            if (edadMaxima < edadMinima)
            {
                MessageBox.Show("La edad máxima no puede ser menor que la edad mínima.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            edadAleatoria = random.Next(edadMinima, edadMaxima+1);
            contadorIntentos++;

            txtNumeroPensado.Text = edadAleatoria.ToString();
        }

        private void btnCorrecto_Click(object sender, RoutedEventArgs e)
        {
            if (contadorIntentos == 0)
            {
                MessageBox.Show("Primero dede hacer click en el boton Primer Intento", "Intentos", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            MessageBox.Show($"Numero de intentos: {contadorIntentos}", "Intentos", MessageBoxButton.OK, MessageBoxImage.Information);
            reiniciar();
        }

        private void btnIncorrecto_Click(object sender, RoutedEventArgs e)
        {
            if(contadorIntentos == 0)
            {
                MessageBox.Show("Primero dede hacer click en el boton Primer Intento", "Intentos",MessageBoxButton.OK, MessageBoxImage.Error);
            }

            contadorIntentos++;
            edadAleatoria = random.Next(edadMinima, edadMaxima + 1);
            txtNumeroPensado.Text = edadAleatoria.ToString();
        }

        private void reiniciar()
        {
            contadorIntentos = 0;
            txtMaximo.Clear();
            txtMinimo.Clear();
            txtNumeroPensado.Clear();
        }
    }
}