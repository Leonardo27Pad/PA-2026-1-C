using System;
using System.Collections.Generic;
using System.Text;

namespace EjemplMVM.Modelo
{
    public class Producto
    {
        public int Id { get; set; }
        public string nombre { get; set; } = String.Empty;
        public decimal precio { get; set; }
        public bool discontinuado { get; set; }
    }
}
