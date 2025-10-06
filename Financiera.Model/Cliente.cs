namespace Financiera.Model
{
    public class Cliente
    {
        public int ID { get; set; }
        public string Apellidos { get; set; }
        public string Nombres { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public int TipoClienteID { get; set; }
        public bool Activo { get; set; }

        public string NombreCompleto
        {
            get { return $"{Apellidos}, {Nombres}"; }
        }
    }
}
