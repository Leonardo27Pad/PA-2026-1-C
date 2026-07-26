using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace TareaBingo
{
    class Casilla
    {
        public int Numero { get; set; }
        public bool EsEspacioLibre { get; set; }
        public bool Marcado { get; set; }
        public bool EsGanadora { get; set; }

        public Casilla(int numero, bool esEspacioLibre = false)
        {
            Numero = numero;
            EsEspacioLibre = esEspacioLibre;
            Marcado = esEspacioLibre; 
            EsGanadora = false;
        }
    }
}
