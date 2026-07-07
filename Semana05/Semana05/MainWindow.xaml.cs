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

namespace Semana05
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

        private void btnCalcular_Click(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrEmpty(txtCliente.Text) )
            {
                MessageBox.Show("Ingrese un cliente valido");
                return;
            }
            if (!double.TryParse(txtMonto.Text, out double deuda) )
            {
                MessageBox.Show("Ingrese un monto valido");
                return;
            }
            if (dpVenc.SelectedDate == null)
            {
                MessageBox.Show("Ingrese una fecha de vencimiento valida");
                return;
            }
            if (dpPago.SelectedDate == null) { 
                MessageBox.Show("Ingrese una fecha de pago valida");
            }

            DateTime feechaVenc = dpVenc.SelectedDate.Value;
            DateTime fechaPago = dpPago.SelectedDate.Value;

            int diasMora = 0;
            if (fechaPago > feechaVenc)
            {
                TimeSpan diferencia = fechaPago.Subtract(feechaVenc);
                diasMora = (int)diferencia.TotalDays;
            }

            txtDiasMora.Text = diasMora.ToString();

            double porcMora = diasMora * 0.5;

            txtMoraPorc.Text = porcMora.ToString();

            double monto = double.Parse(txtMonto.Text);
            double moraSoles = monto * (porcMora / 100);

            txtMoraSoles.Text = moraSoles.ToString();

            double totalPagar = monto + moraSoles;

            txtMontoPagar.Text = totalPagar.ToString();
        }
        private void btnFinalizar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            txtCliente.Clear();
            txtMonto.Clear();
            txtMontoPagar.Clear();
            txtDiasMora.Clear();
            txtMoraSoles.Clear;
            txtMoraPorc.Clear();
            
            dpPago.SelectedDate = null;
            dpVenc.SelectedDate = null;
        }
    }
}
