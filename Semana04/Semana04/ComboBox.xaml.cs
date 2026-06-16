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

namespace Semana04
{
    /// <summary>
    /// Lógica de interacción para ComboBox.xaml
    /// </summary>
    public partial class ComboBox : Window
    {
        public ComboBox()
        {
            InitializeComponent();
        }

        private void btMostrar_Click(object sender, RoutedEventArgs e)
        {
            if (cmbFrutas.SelectedItem == null)
            {
                MessageBox.Show("Por favor, seleccione una fruta.");
                return;
            }
            ComboBoxItem seleccionado = (ComboBoxItem)cmbFrutas.SelectedItem;
            string valorSeleccionado = seleccionado.Content.ToString();

            MessageBox.Show($"Fruta Seleccionada: {valorSeleccionado}");
        }

        private void btAgregar_Click(object sender, RoutedEventArgs e)
        {
            ComboBoxItem nuevoItem = new ComboBoxItem();
            nuevoItem.Content = txtNuevo.Text.ToUpper();
            cmbFrutas.Items.Add(nuevoItem);
            txtNuevo.Clear();

            MessageBox.Show("Fruta agregada correctamente.");
        }
    }
}
