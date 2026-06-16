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
    /// Lógica de interacción para Ejercicio2.xaml
    /// </summary>
    public partial class Ejercicio2 : Window
    {
        public Ejercicio2()
        {
            InitializeComponent();
        }

        private void btnCalcular_Click(object sender, RoutedEventArgs e)
        {
            double.TryParse(txtDeuda.Text, out double deuda);
            double desc = 0;
            double puntos = 0;

            if (cbDescuento.IsChecked == true)
            {
                if (double.TryParse(txtDes.Text, out double porcDesc))
                {
                    desc = deuda * (porcDesc / 100.0);
                }
                else
                {
                    MessageBox.Show("Ingrese un porcentaje de descuento válido.", "Dato Inválido");
                    return;
                }
            }

            if (cbBonus.IsChecked == true)
            {
                if (double.TryParse(txtBonus.Text, out double puntosIngresados))
                {
                    puntos = (puntosIngresados / 10.0) * 3.0;
                }
                else
                {
                    MessageBox.Show("Ingrese una cantidad de puntos válida.", "Dato Inválido");
                    return;
                }
            }

            double totalAPagar = deuda - desc - puntos;

            if(totalAPagar < 0)
            {
                totalAPagar = 0;
            }

            lbTotalPagar.Content = totalAPagar;

        }

       
    }
}
