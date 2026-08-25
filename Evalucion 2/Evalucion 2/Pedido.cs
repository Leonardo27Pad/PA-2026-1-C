using System;
using System.Collections.Generic;
using System.Text;

namespace Evalucion_2
{
     class Pedido
    {
        public int OrderID { get; set; }
        public string Cliente { get; set; }
        public DateTime? OrderDate { get; set; }
        public decimal? Freight { get; set; }
        public string ShipCountry { get; set; }
    }
}
