namespace Financiera.Model
{
    public class CuotaPrestamo
    {
        public int PrestamoID { get; set; }
        public int NumeroCuota { get; set; }
        public decimal Importe { get; set; }
        public decimal ImporteInteres { get; set; }
        public DateTime FechaPago { get; set; }
        public string Estado { get; set; }
    }
}
