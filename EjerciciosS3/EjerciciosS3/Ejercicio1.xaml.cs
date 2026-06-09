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

namespace EjerciciosS3
{
    /// <summary>
    /// Lógica de interacción para Ejercicio1.xaml
    /// </summary>
    public partial class Ejercicio1 : Window
    {
        public Ejercicio1()
        {
            InitializeComponent();
        }

        private void btnCalcular_Click(object sender, RoutedEventArgs e)
        {
            double ingreso = 0;

            double.TryParse(txtIngreso.Text, out ingreso);

            double fonavi = 0;
            double impRenta = 0;
            double afp = 0;
            if (cbF.IsChecked == true)
            {
                fonavi = ingreso * 0.05;
            }
            if(cbIR.IsChecked == true)
            {
                impRenta = ingreso * 0.10;
            }
            if (cbAFP.IsChecked == true)
            {
                afp = ingreso * 0.06;
            }
            double totalPagar = fonavi + impRenta + afp;

            lbF.Content = fonavi;
            lbIR.Content = impRenta;
            lbAFP.Content = afp;
            lbTP.Content = totalPagar;
        }
    }
}
