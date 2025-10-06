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
    public class TipoPrestamoRepositorio : IGeneric<TipoPrestamo>
    {
        private readonly string cadenaConexion;
        public TipoPrestamoRepositorio(IConfiguration config)
        {
            cadenaConexion = config["ConnectionStrings:BD"] ?? "";
        }

        public bool Actualizar(TipoPrestamo entidad)
        {
            bool exito = false;
            return exito;
        }

        public bool Eliminar(int id)
        {
            bool exito = false;
            return exito;
        }

        public List<TipoPrestamo> Listar()
        {
            List<TipoPrestamo> listado = new List<TipoPrestamo>();
            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();
                using (SqlCommand comando = new SqlCommand("ListarTipoPrestamo", conexion))
                {
                    comando.CommandType = System.Data.CommandType.StoredProcedure;
                    using (SqlDataReader lector = comando.ExecuteReader())
                    {
                        if(lector != null && lector.HasRows)
                        {
                            while (lector.Read())
                            {
                                listado.Add(new TipoPrestamo()
                                {
                                    ID = lector.GetInt32(0),
                                    Nombre = lector.GetString(1),
                                    Tasa = lector.GetDecimal(2)
                                });
                            }
                        }
                    }
                }
            }
            return listado;
        }

        public TipoPrestamo ObtenerPorID(int id)
        {
            TipoPrestamo tipoPrestamo = null;
            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();
                using (SqlCommand comando = new SqlCommand("ObtenerTipoPrestamoPorID", conexion))
                {
                    comando.CommandType = System.Data.CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@id", id);
                    using (SqlDataReader lector = comando.ExecuteReader())
                    {
                        if(lector != null && lector.HasRows)
                        {
                            lector.Read();
                            tipoPrestamo = new TipoPrestamo()
                            {
                                ID = lector.GetInt32(0),
                                Nombre = lector.GetString(1),
                                Tasa = lector.GetDecimal(2)
                            };
                        }
                    }
                }
            }
            return tipoPrestamo;
        }

        public int Registrar(TipoPrestamo entidad)
        {
            int nuevoID = 0;
            return nuevoID;
        }
    }
}
