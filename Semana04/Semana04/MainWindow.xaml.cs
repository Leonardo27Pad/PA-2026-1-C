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

namespace Semana04
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ComboBox ventana = new ComboBox();
            ventana.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Listbox ventana = new Listbox();
            ventana.ShowDialog();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            ListVew ventana = new ListVew();
            ventana.ShowDialog();
        }
    }
}
