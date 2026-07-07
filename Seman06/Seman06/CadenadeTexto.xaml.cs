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

namespace Seman06
{
    /// <summary>
    /// Lógica de interacción para CadenadeTexto.xaml
    /// </summary>
    public partial class CadenadeTexto : Window
    {
        string[] arrayNombres;
        public CadenadeTexto()
        {
            InitializeComponent();
        }

        private void btnListar_Click(object sender, RoutedEventArgs e)
        {
            string textOrigen = txtCadenaInicial.Text.Trim();
            lbNombres.Items.Clear();
            if(string.IsNullOrWhiteSpace(textOrigen))
            {
                MessageBox.Show("Debe ingresar un texto", "Validar", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            arrayNombres =textOrigen.Split(' ');

            foreach (string nombre in arrayNombres)
            {
                lbNombres.Items.Add(nombre);
            }
            txtCantidadNombres.Text = lbNombres.Items.Count.ToString();
        }

        private void btnFiltrar_Click(object sender, RoutedEventArgs e)
        {
            string filtro = txtLetra.Text;
            lbFiltrado.Items.Clear();
            foreach (string nombre in arrayNombres)
            {
                if (nombre.StartsWith(filtro, StringComparison.OrdinalIgnoreCase))
                {
                    lbFiltrado.Items.Add(nombre);
                }
            }
            txtCantidadFiltrado.Text = lbFiltrado.Items.Count.ToString();
        }
    }
}
