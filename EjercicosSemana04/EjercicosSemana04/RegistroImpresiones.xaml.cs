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
    /// Lógica de interacción para RegistroImpresiones.xaml
    /// </summary>
    public partial class RegistroImpresiones : Window
    {
        public RegistroImpresiones()
        {
            InitializeComponent();
        }
        private void btnRegistrar_Click(object sender, RoutedEventArgs e)
        {
            lbCliente.Items.Add(txtCliente.Text); lbCelular.Items.Add(txtNCelular.Text); lbCantidad.Items.Add(txtCantidad.Text); 
            double tarifa = 0;
            if (rbEscolar.IsChecked == true) tarifa = 0.5;
            else if (rbUniversitario.IsChecked == true) tarifa = 0.8;
            else if (rbOrganizacion.IsChecked == true) tarifa = 1.2;

            lbTarifa.Items.Add(tarifa);
            double importe = Double.Parse(txtCantidad.Text) * tarifa; 
            lbImporte.Items.Add(importe); txtCliente.Clear(); txtNCelular.Clear(); txtCantidad.Clear(); rbEscolar.IsChecked = false; 
            rbUniversitario.IsChecked = false; rbOrganizacion.IsChecked = false;
        }
        private void btnEstadistica_Click(object sender, RoutedEventArgs e)
        {
            int c_escolar = 0, c_universitario = 0, c_organizacion = 0; 
            for (int i = 0; i < lbTarifa.Items.Count; i++)
            {
                if (lbTarifa.Items[i].ToString() == "0.5") c_escolar++;
                else if (lbTarifa.Items[i].ToString() == "0.8") c_universitario++;
                else if (lbTarifa.Items[i].ToString() == "1.2") c_organizacion++;
            }
            txtImpreEcolares.Text = c_escolar.ToString(); txtImpreUniversitarias.Text = c_universitario.ToString(); txtImpreOrganizacionales.Text = c_organizacion.ToString();
        }
        private void lbCliente_SelectionChanged(object sender,SelectionChangedEventArgs e)
        {
            int indice = lbCliente.SelectedIndex;
            txtCliente.Text = lbCliente.Items[indice].ToString(); txtNCelular.Text = lbCelular.Items[indice].ToString(); txtCantidad.Text = lbCantidad.Items[indice].ToString();
            if (lbTarifa.Items[indice].ToString() == "0.5") rbEscolar.IsChecked = true;
            else if (lbTarifa.Items[indice].ToString() == "0.8") rbUniversitario.IsChecked = true;
            else if (lbTarifa.Items[indice].ToString() == "1.2") rbOrganizacion.IsChecked = true;

        }
    }
}

