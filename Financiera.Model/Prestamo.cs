using System.ComponentModel;

namespace Financiera.Model
{
    public class Prestamo
    {
        public int ID { get; set; }
        public DateTime Fecha { get; set; }

        [DisplayName("Fecha Depósito")]
        public DateTime FechaDeposito { get; set; }

        [DisplayName("Cliente")]
        public int ClienteID { get; set; }

        [DisplayName("Tipo Préstamo")]
        public int TipoPrestamoID { get; set; }
        public string Moneda { get; set; }
        public decimal Importe { get; set; }
        public int Plazo { get; set; }
        public decimal Tasa { get; set; }
        public string Estado { get; set; }

        public Prestamo()
        {
            Fecha = DateTime.Now;
            FechaDeposito = DateTime.Now.AddDays(7);
            Estado = "P";
        }
    }
}
