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
    public class TipoClienteRepositorio : IGeneric<TipoCliente>
    {
        private readonly string cadenaConexion;
        public TipoClienteRepositorio(IConfiguration config)
        {
            cadenaConexion = config["ConnectionStrings:BD"] ?? "";
        }
        public bool Actualizar(TipoCliente entidad)
        {
            throw new NotImplementedException();
        }

        public bool Eliminar(int id)
        {
            throw new NotImplementedException();
        }

        public List<TipoCliente> Listar()
        {
            throw new NotImplementedException();
        }

        public TipoCliente ObtenerPorID(int id)
        {
            TipoCliente tipo = null;
            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();
                using (SqlCommand comando = new SqlCommand("ObtenerTipoClientePorID", conexion))
                {
                    comando.CommandType = System.Data.CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@id", id);
                    using (SqlDataReader lector = comando.ExecuteReader())
                    {
                        if(lector != null && lector.HasRows)
                        {
                            lector.Read();
                            tipo = new TipoCliente { 
                                ID = lector.GetInt32(0),
                                Nombre = lector.GetString(1),
                                Activo = lector.GetBoolean(2)
                            };
                        }
                    }
                }
            }
            return tipo;
        }

        public int Registrar(TipoCliente entidad)
        {
            throw new NotImplementedException();
        }
    }
}
