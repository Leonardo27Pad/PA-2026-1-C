using Microsoft.Data.SqlClient;
using System.Data;
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
using System.Windows.Threading;

namespace Evalucion_2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string cadenaConexion = "Server=.;Database=Northwind;Integrated Security=True;TrustServerCertificate=True";
        private DispatcherTimer tempo;

        public MainWindow()
        {
            InitializeComponent();
            ConfigurarTemporizador();
        }
        private void ConfigurarTemporizador()
        {
            tempo = new DispatcherTimer();
            tempo.Interval = TimeSpan.FromSeconds(5);
            tempo.Tick += Temporizador_Tick;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CargarPaises();
        }

        private void CargarPaises()
        {
            try
            {
                using (SqlConnection conexion = new SqlConnection(cadenaConexion))
                {
                    string consulta = "SELECT DISTINCT ShipCountry FROM Orders WHERE ShipCountry IS NOT NULL ORDER BY ShipCountry";
                    SqlCommand comando = new SqlCommand(consulta, conexion);
                    conexion.Open();
                    using (SqlDataReader reader = comando.ExecuteReader())
                    {
                        cmbPaises.Items.Clear();
                        while (reader.Read())
                        {
                            if (!reader.IsDBNull(0))
                                cmbPaises.Items.Add(reader.GetString(0));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los países: " + ex.Message);
            }
        }
        private void btnIniciar_Click(object sender, RoutedEventArgs e)
        {
            if (cmbPaises.SelectedItem == null)
            {
                MessageBox.Show("Por favor, selecciona un país primero.", "Aviso");
                return;
            }

            btnIniciar.IsEnabled = false;
            cmbPaises.IsEnabled = false;
            btnDetener.IsEnabled = true;

            txtEstado.Text = "Monitoreando...";
            txtEstado.Foreground = System.Windows.Media.Brushes.Green;

            ConsultarPedidosPendientes();
            tempo.Start();
        }

        private void btnDetener_Click(object sender, RoutedEventArgs e)
        {
            tempo.Stop();

            btnIniciar.IsEnabled = true;
            cmbPaises.IsEnabled = true;
            btnDetener.IsEnabled = false;

            txtEstado.Text = "Detenido";
            txtEstado.Foreground = System.Windows.Media.Brushes.Red;
        }

        private void Temporizador_Tick(object sender, EventArgs e)
        {
            ConsultarPedidosPendientes();
        }

        private void ConsultarPedidosPendientes()
        {
            try
            {
                if (cmbPaises.SelectedItem == null)
                {
                    btnDetener_Click(null, null);
                    return;
                }

                string paisSeleccionado = cmbPaises.SelectedItem.ToString();

                using (SqlConnection conexion = new SqlConnection(cadenaConexion))
                {
                    string consulta = @"
                    SELECT OrderID AS OrderID,
                           CustomerID AS Cliente,
                           OrderDate AS OrderDate,
                           Freight AS Freight,
                           ShipCountry AS ShipCountry
                    FROM Orders
                    WHERE ShipCountry = @Country
                      AND ShippedDate IS NULL";

                    SqlCommand comando = new SqlCommand(consulta, conexion);
                    comando.Parameters.Add("@Country", System.Data.SqlDbType.NVarChar, 50).Value = paisSeleccionado;

                    SqlDataAdapter adaptador = new SqlDataAdapter(comando);
                    DataTable dtPedidos = new DataTable();

                    adaptador.Fill(dtPedidos);

                    dgPedidos.ItemsSource = dtPedidos.DefaultView;
                    txtTotalOrdenes.Text = dtPedidos.Rows.Count.ToString();
                    txtUltimoRefresco.Text = DateTime.Now.ToString("HH:mm:ss");
                }
            }
            catch (Exception ex)
            {
                btnDetener_Click(null, null);
                MessageBox.Show("Error al consultar los datos: " + ex.Message);
            }
        }
    }
}