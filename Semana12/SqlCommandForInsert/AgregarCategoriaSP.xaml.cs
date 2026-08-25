using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Windows;

namespace SqlCommandForInsert
{
    public partial class AgregarCategoriaSP : Window
    {
        // Conexión a la base de datos
        string cn = ConfigurationManager.ConnectionStrings["SqlCommandForInsert.Properties.Settings.Northwind"].ConnectionString;

        public AgregarCategoriaSP()
        {
            InitializeComponent();
        }

        // Cargar los datos al iniciar la ventana
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.CargarListaCategorias();
        }

        // Botón para limpiar el formulario
        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            this.Nuevo();
        }

        // Método que limpia las cajas de texto
        private void Nuevo()
        {
            txtNombre.Clear();
            txtDescripcion.Clear();
            txtMensaje.Text = "";
            txtNombre.Focus();
        }

        // Botón principal para agregar o buscar
        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                txtMensaje.Text = "Por favor, ingresa un nombre para la categoría.";
                txtMensaje.Foreground = System.Windows.Media.Brushes.Red;
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(cn))
                {
                    conn.Open();
                    SqlCommand cmd = conn.CreateCommand();

                    cmd.CommandText = "sp_ObtenerOInsertarCategoria";
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.Add("@Nombre", System.Data.SqlDbType.NVarChar, 15).Value = txtNombre.Text;
                    cmd.Parameters.Add("@Descripcion", System.Data.SqlDbType.NVarChar, 100).Value = txtDescripcion.Text;

                    // Ejecutamos el SP y capturamos el ID devuelto
                    int idGenerado = Convert.ToInt32(cmd.ExecuteScalar());

                    // Mensaje de éxito
                    txtMensaje.Text = $"Operación exitosa. El ID de la categoría es {idGenerado}";
                    txtMensaje.Foreground = System.Windows.Media.Brushes.Green;

                    this.CargarListaCategorias();
                }
            }
            catch (SqlException ex)
            {
                txtMensaje.Text = $"Error en la base de datos: {ex.Message}";
                txtMensaje.Foreground = System.Windows.Media.Brushes.Red;
            }
            catch (Exception ex)
            {
                txtMensaje.Text = $"Error general: {ex.Message}";
                txtMensaje.Foreground = System.Windows.Media.Brushes.Red;
            }
        }

        // Método para cargar y ordenar la tabla de categorías
        private void CargarListaCategorias()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cn))
                {
                    // Consulta ordenada de menor a mayor (sin DESC)
                    string query = "SELECT CategoryID, CategoryName, Description FROM Categories ORDER BY CategoryID";
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Categoria> lista = new List<Categoria>();

                    while (reader.Read())
                    {
                        lista.Add(new Categoria
                        {
                            Id = reader.GetInt32(0),
                            Nombre = reader.GetString(1),
                            Descripcion = reader.IsDBNull(2) ? "" : reader.GetString(2)
                        });
                    }
                    dgCategorias.ItemsSource = lista;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar la tabla: {ex.Message}");
            }
        }
    }

}