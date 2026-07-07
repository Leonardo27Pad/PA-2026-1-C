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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Seman06
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

        }

        private void btnCrono_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPaci.Text))
            {
                MessageBox.Show("Debe ingresar un nombre de paciente", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string paciente = txtPaci.Text;

            string tratamiento = ((ComboBoxItem)CmbTrat.SelectedItem).Content.ToString();
            string piezaDental = ((ComboBoxItem)cmbPieDen.SelectedItem).Content.ToString();

            DateTime fechaCita = calCita.SelectedDate.Value;
            
            DateTime citaProxima = fechaCita.AddDays(15);

            string reporte = $"Reporte de cita \n" +
                $"Paciente: {paciente}\n" +
                $"Tratamiento: {tratamiento}\n" +
                $"Pieza dental: {piezaDental}\n" +
                $"Fecha de cita: {fechaCita.ToLongDateString()}\n" +
                $"Próxima cita: {citaProxima.ToShortDateString()}";

            txtReporte.Text = reporte;
        }
    }
}
 

