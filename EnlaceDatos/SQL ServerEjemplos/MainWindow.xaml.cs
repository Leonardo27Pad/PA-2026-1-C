using Microsoft.Data.SqlClient;
using System.Data;
using System.Data.SqlClient;
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

namespace SQL_ServerEjemplos
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
        string cadenaConexion = "Server=.;Database=Northwind;Integrated Security=true;TrustServerCertificate=true;";
        private void btnVerificarConexion_Click(object sender, RoutedEventArgs e)
        {


            using (SqlConnection con = new SqlConnection(cadenaConexion))
            {
                try
                {
                    con.Open();
                    MessageBox.Show("Conexión exitosa: Base de datos = " + con.Database);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al conectar: " + ex.Message);
                }
            }
        }

        private void btnCargarCategorias_Click(object sender, RoutedEventArgs e)
        {
            string query = "SELECT CategoryID,CategoryName FROM Categories";
            using (SqlConnection con = new SqlConnection(cadenaConexion))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand(query, con);
                    con.Open();

                    using (SqlDataReader dataReader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection))
                    {
                        cbxCategorias.Items.Clear();
                        while (dataReader.Read())
                        {
                            cbxCategorias.Items.Add(
                                new
                                {
                                    Id = dataReader.GetInt32(0),
                                    Nombre = dataReader.GetString(1)
                                }
                            );
                        }
                    }

                }
                catch (SqlException ex)
                {
                    MessageBox.Show($"Error en sql: {ex.Message}");
                }
            }
        }

        private void btnMostrarSeleccionado_Click(object sender, RoutedEventArgs e)
        {
            if (cbxCategorias.SelectedItem != null)
            {
                dynamic categoriaSeleccionada = cbxCategorias.SelectedItem;
                int id = categoriaSeleccionada.Id;
                string nombre = categoriaSeleccionada.Nombre;

                MessageBox.Show($"Categoría seleccionada: Id={id}, Nombre={nombre}");


                MessageBox.Show($"Categoría seleccionada: Id={cbxCategorias.SelectedValue}");
            }
            else
            {
                MessageBox.Show("No hay categoría seleccionada");
            }
        }

        private void btnCargarProductos_Click(object sender, RoutedEventArgs e)
        {
            string query = "SELECT ProductID, ProductName, UnitPrice, UnitsInStock FROM Products WHERE Discontinued = 0";
            using (SqlConnection con = new SqlConnection(cadenaConexion))
            {
                try
                {
                    SqlDataAdapter sqlData = new SqlDataAdapter(query, con);
                    DataSet dataSet = new DataSet();
                    sqlData.Fill(dataSet, "Producto");
                    dgProductos.ItemsSource = dataSet.Tables["Producto"].DefaultView;
                }
                catch (SqlException ex)
                {
                    MessageBox.Show($"Error al ejecutar sql: {ex.Message}");
                }



            }
        }


    }
}