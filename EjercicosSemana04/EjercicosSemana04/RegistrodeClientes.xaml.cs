using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace EjercicosSemana04
{
    /// <summary>
    /// Lógica de interacción para RegistrodeClientes.xaml
    /// </summary>
    public partial class RegistrodeClientes : Window
    {
        List<Cliente> clientes = new List<Cliente>();
        public RegistrodeClientes()
        {
            InitializeComponent();
            lvClientes.ItemsSource = clientes;
        }

        private void btnGrabar_Click(object sender, RoutedEventArgs e)
        {
            string nombres = txtNombres.Text;
            string apellidos = txtApellidos.Text;
            string dni = txtDNI.Text;
            string direccion = txtDireccion.Text;
            ComboBoxItem estadoCivilItem = (ComboBoxItem)cmbEstadoCivil.SelectedItem;
            string estadoCivil = estadoCivilItem.Content.ToString();

            Cliente nuevoCliente = new Cliente();
            nuevoCliente.Nombres = nombres;
            nuevoCliente.Apellidos = apellidos;
            nuevoCliente.Dni = dni;
            nuevoCliente.Direccion = direccion;
            nuevoCliente.EstadoCivil = estadoCivil;

            clientes.Add(nuevoCliente);

            lvClientes.ItemsSource = null;
            lvClientes.ItemsSource = clientes;

            Limpiar();

        }

        private void Limpiar()
        {
            txtApellidos.Clear();
            txtNombres.Clear();
            txtDireccion.Clear();
            txtDNI.Clear();
            cmbEstadoCivil.SelectedIndex = -1;
        }

        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }

        private void btnEstadistica_Click(object sender, RoutedEventArgs e)
        {
            int solteros = 0;
            int casados = 0;

            /*for(int i = 0; i < clientes.Count; i++)
            {
                if (clientes[i].EstadoCivil == "Soltero(a)")
                {
                    solteros++;
                }
                if (clientes[i].EstadoCivil == "Casado(a)")
                {
                    casados++;
                }
            }*/

            solteros = clientes.Count(c => c.EstadoCivil == "Soltero(a)");
            casados = clientes.Count(c => c.EstadoCivil == "Casado(a)");

            txtCasados.Text = casados.ToString();
            txtSolteros.Text = solteros.ToString();
        }

        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
