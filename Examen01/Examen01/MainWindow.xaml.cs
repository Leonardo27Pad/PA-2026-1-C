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

namespace Examen01
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int contAutos = 0;
        int contCamioneta = 0;
        int contSUV = 0;
        int conLimpieza = 0;
        double totalRecaudado = 0.0;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnRegistrar_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPlaca.Text) )
            {
                MessageBox.Show("Ingrese una placa");
                return;
            }
            if (cboTipoVehiculo.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione un tipo de vehículo.");
                return;
            }

            string placa = txtPlaca.Text.Trim().ToUpper();
            string tipoVehiculo = ((ComboBoxItem)cboTipoVehiculo.SelectedItem).Content.ToString();

            double tarifaBase = 0;
            int serAdiConta = 0;
            double subTotalSer = 0;

            switch (tipoVehiculo)
            {
                case "Auto": tarifaBase = 20.00; 
                break;
                case "Camioneta": tarifaBase = 30.00;
                break;
                case "SUV": tarifaBase = 35.00;
                break;
            }

            if (chkEncerado.IsChecked == true) { subTotalSer += 15.00; serAdiConta++; }
            if (chkLavadoMotor.IsChecked == true) { subTotalSer += 20.00; serAdiConta++; }
            if (chkLimpiezaSalon.IsChecked == true) { subTotalSer += 25.00; serAdiConta++; }

            double montoFinal = tarifaBase + subTotalSer;

            if (serAdiConta == 3)
            {
                montoFinal -= 10.00;
            }

            string registro = $"Placa: {placa}        Tipo: {tipoVehiculo}         Servicios Extras: {serAdiConta}         Total a Pagar: S/  {montoFinal}";
            lstHistorial.Items.Add(registro);

            if (tipoVehiculo == "Auto") contAutos++;
            else if (tipoVehiculo == "Camioneta") contCamioneta++;
            else if (tipoVehiculo == "SUV") contSUV++;

            if (chkLimpiezaSalon.IsChecked == true) conLimpieza++;

            totalRecaudado += montoFinal;

            ActualizarEstadisticas();

            LimpiarInterfaz();
        }

        private void ActualizarEstadisticas()
        {
            lblAutos.Text = $"Autos: {contAutos}";
            lblCamionetas.Text = $"Camionetas: {contCamioneta}";
            lblSUV.Text = $"SUVs: {contSUV}";
            lblLimpiezaSalon.Text = $"Limpieza Salón: {conLimpieza}";
            lblTotalRecaudado.Text = $"Total Recaudado: S/ {totalRecaudado}";
        }

        private void LimpiarInterfaz()
        {
            txtPlaca.Clear();
            cboTipoVehiculo.SelectedIndex = -1;
            chkEncerado.IsChecked = false;
            chkLavadoMotor.IsChecked = false;
            chkLimpiezaSalon.IsChecked = false;
        }
    }
}
