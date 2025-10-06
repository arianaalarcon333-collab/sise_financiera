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
    public class PrestamoRepositorio
    {
        private string cadenaConexion;
        public PrestamoRepositorio(IConfiguration config)
        {
            cadenaConexion = config["ConnectionStrings:BD"] ?? "";
        }

        public List<Prestamo> Listar()
        {
            List<Prestamo> listado = new List<Prestamo>();
            return listado;
        }

        public Prestamo ObtenerPorID(int id)
        {
            Prestamo prestamo = null;
            return prestamo;
        }

        public int Registrar(Prestamo entidad)
        {
            int nuevoID = 0;
            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();
                using (SqlCommand comando = new SqlCommand("RegistrarPrestamo", conexion))
                {
                    comando.CommandType = System.Data.CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@fechaDeposito", entidad.FechaDeposito);
                    comando.Parameters.AddWithValue("@cliente", entidad.ClienteID);
                    comando.Parameters.AddWithValue("@tipo", entidad.TipoPrestamoID);
                    comando.Parameters.AddWithValue("@moneda", entidad.Moneda);
                    comando.Parameters.AddWithValue("@importe", entidad.Importe);
                    comando.Parameters.AddWithValue("@plazo", entidad.Plazo);
                    comando.Parameters.AddWithValue("@tasa", entidad.Tasa);
                    nuevoID = Convert.ToInt32(comando.ExecuteScalar());
                }
            }
            return nuevoID;
        }
    }
}
