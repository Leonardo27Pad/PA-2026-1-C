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

namespace EncriptarClave
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

        private void btnEcriptar_Click(object sender, RoutedEventArgs e)
        {
            string clave = txtClaveOriginal.Text;

            if (string.IsNullOrEmpty(clave))
            {
                MessageBox.Show("Por favor, ingrese una clave para encriptar.", "Error", MessageBoxButton.OK);
                return;
            }
            string encriptada = " ";
            foreach (char c in clave)
            {
                encriptada += (char)(c + 5);
            }
            txtClaveEncriptada.Text = encriptada;
        }
    }
}
