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
    /// Lógica de interacción para Listbox.xaml
    /// </summary>
    public partial class Listbox : Window
    {
        public Listbox()
        {
            InitializeComponent();
        }

        private void btMostrar_Click(object sender, RoutedEventArgs e)
        {
            if (lbFrutas.SelectedItem == null)
            {
                MessageBox.Show("Por favor, seleccione una fruta.");
                return;
            }
            ListBoxItem seleccionado = lbFrutas.SelectedItem as ListBoxItem;
            string valorSeleccionado = seleccionado.Content.ToString();
            MessageBox.Show($"Fruta Seleccionada: {valorSeleccionado}");
        }

        private void btAgregar_Click(object sender, RoutedEventArgs e)
        {
            ListBoxItem nuevo = new ListBoxItem();
            nuevo.Content = txtNuevo.Text.ToUpper();

            lbFrutas.Items.Add(nuevo);

            txtNuevo.Clear();
        }
    }
}
