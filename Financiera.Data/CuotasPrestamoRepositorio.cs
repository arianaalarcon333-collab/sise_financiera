using Financiera.Model;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financiera.Data
{
    public class CuotasPrestamoRepositorio
    {
        private readonly string cadenaConexion;

        public CuotasPrestamoRepositorio(IConfiguration config)
        {
            cadenaConexion = config["ConnectionStrings:BD"] ?? "";
        }

        public bool Registrar(CuotaPrestamo entidad)
        {
            bool exito = false;
            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();
                using (SqlCommand comando = new SqlCommand("RegistrarCuotas", conexion))
                {
                    comando.CommandType = System.Data.CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@prestamo", entidad.PrestamoID);
                    comando.Parameters.AddWithValue("@numero", entidad.NumeroCuota);
                    comando.Parameters.AddWithValue("@importe", entidad.Importe);
                    comando.Parameters.AddWithValue("@importeInteres", entidad.ImporteInteres);
                    comando.Parameters.AddWithValue("@fechaPago", entidad.FechaPago);
                    exito = comando.ExecuteNonQuery() > 0;
                }
            }
            return exito;
        }
    }
}
