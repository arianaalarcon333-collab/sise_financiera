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
            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();
                using (SqlCommand comando = new SqlCommand("ListarPrestamos", conexion))
                {
                    comando.CommandType = System.Data.CommandType.StoredProcedure;
                    using (SqlDataReader lector = comando.ExecuteReader())
                    {
                        if (lector != null && lector.HasRows)
                        {
                            while(lector.Read())
                            {
                                listado.Add(new Prestamo
                                {
                                    ID = lector.GetInt32(0),
                                    Fecha = lector.GetDateTime(1),
                                    FechaDeposito = lector.GetDateTime(2),
                                    ClienteID = lector.GetInt32(3),
                                    TipoPrestamoID = lector.GetInt32(4),
                                    Moneda = lector.GetString(5),
                                    Importe = lector.GetDecimal(6),
                                    Plazo = lector.GetInt32(7),
                                    Tasa = lector.GetDecimal(8),
                                    Estado = lector.GetString(9),
                                });
                            }
                        }
                    }
                }
            }
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
