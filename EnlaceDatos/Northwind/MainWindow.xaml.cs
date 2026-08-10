using Microsoft.Data.SqlClient;
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

namespace Northwind
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string cadenaConexion = "Server=.;Database=Northwind;Integrated Security=true;TrustServerCertificate=true;";

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string query = "select distinct Country from customers order by country";

            using (SqlConnection con = new SqlConnection(cadenaConexion))
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(query, con);
                    using (SqlDataReader reader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection))
                    {
                        cbxPais.Items.Clear();
                        while (reader.Read())
                        {
                            cbxPais.Items.Add(reader.GetString(0));
                        }
                    }

                }
                catch (SqlException ex)
                {
                    MessageBox.Show($"Error SQL: {ex.Message}");
                }
            }
        }

        private void cbxPais_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbxPais.SelectedItem == null) return;
            string paisSeleccionado = cbxPais.SelectedItem.ToString();

            string query = "select CustomerID,CompanyName,ContactName,Country from customers where Country = @Country";

            using (SqlConnection con = new SqlConnection(cadenaConexion))
            {
                con.Open();
                SqlCommand command = new SqlCommand(query, con);
                command.Parameters.AddWithValue("@Country", paisSeleccionado);

                SqlDataReader reader = command.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                List<Cliente> lstClientes = new List<Cliente>();
                while (reader.Read())
                {   
                    Cliente cliente = new Cliente();
                    cliente.CustomerID = reader.GetString(0);
                    cliente.CompanyName = reader.GetString(1);
                    cliente.ContactName = reader.GetString(2);
                    cliente.Country = reader.GetString(3);

                    lstClientes.Add(cliente);
                }

                lvClientes.ItemsSource = lstClientes;
            }
        }
    }
}