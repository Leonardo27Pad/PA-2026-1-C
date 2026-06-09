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

namespace EjerciciosS3
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

        
        private void b_aplicar1_Click(object sender, RoutedEventArgs e)
        {

            lb_radiobutton.FontFamily = new FontFamily("Segoe UI");
            lb_radiobutton.Foreground = Brushes.Black;
            lb_radiobutton.Background = Brushes.White;

            if (rb_tipodeletra.IsChecked == true)
            {
                lb_radiobutton.FontFamily = new FontFamily("Consolas");
            }
            else if (rb_colordetexto.IsChecked == true)
            {
                lb_radiobutton.Foreground = Brushes.Red;
            }
            else if (rb_colodefondo.IsChecked == true)
            {
                lb_radiobutton.Background = Brushes.Yellow;
            }

        }

        private void b_aplicar_Click(object sender, RoutedEventArgs e)
        {
            lb_checkbox.FontFamily = new FontFamily("Segoe UI");
            lb_checkbox.Foreground = Brushes.Black;
            lb_checkbox.Background = Brushes.White;

            if (cb_tipodeletra.IsChecked == true)
            {
                lb_checkbox.FontFamily = new FontFamily("Consolas");
            }
            if (cb_colordetexto.IsChecked == true)
            {
                lb_checkbox.Foreground = Brushes.Red;
            }
            if (cb_colordefondo.IsChecked == true)
            {
                lb_checkbox.Background = Brushes.Yellow;
            }
        }
    }
    }
