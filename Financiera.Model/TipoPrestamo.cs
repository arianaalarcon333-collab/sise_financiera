using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financiera.Model
{
    public class TipoPrestamo
    {
        public int ID { get; set; }
        public string Nombre { get; set; }
        public decimal Tasa { get; set; }
    }
}
