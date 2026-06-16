using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Semana04
{
     class Alumno
    {
        public Alumno(string nombres, string apellidos, int edad)
        {
            this.nombres = nombres;
            this.apellidos = apellidos;
            this.edad = edad;
        }

        public string nombres { get; set; }
        public string apellidos { get; set; }
        public int edad { get; set; }
    }
}
