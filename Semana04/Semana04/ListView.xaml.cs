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
    /// Lógica de interacción para ListVew.xaml
    /// </summary>
    public partial class ListVew : Window
    {
        List<Alumno> alumnos = new List<Alumno>();
        public ListVew()
        {
            InitializeComponent();

            alumnos.Add(new Alumno("Juan", "Perez", 30));
            alumnos.Add(new Alumno("Maria", "Gomez", 25));
        
            lvAlumnos.ItemsSource = alumnos;
        }
        
        private void btAgregar_Click(object sender, RoutedEventArgs e)
        {
       
            Alumno nuevoAlumno = new Alumno(
                txtNombre.Text,
                txtApellido.Text,
                Int32.Parse(txtEdad.Text));
            alumnos.Add(nuevoAlumno);
            lvAlumnos.ItemsSource = null;
            lvAlumnos.ItemsSource = alumnos;
            txtNombre.Clear();
            txtApellido.Clear();
            txtEdad.Clear();
        }

        private void btMostrar_Click(object sender, RoutedEventArgs e)
        {
            Alumno alumno = (Alumno)lvAlumnos.SelectedItem;

            MessageBox.Show($"Alumno Seleccionado: {alumno.nombres} {alumno.apellidos}, Edad: {alumno.edad}");

        }
    }
}
