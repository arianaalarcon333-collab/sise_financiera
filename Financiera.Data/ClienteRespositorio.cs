using Financiera.Model;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Financiera.Data
{
    public class ClienteRespositorio : IGeneric<Cliente>
    {
        private readonly string cadenaConexion;
        public ClienteRespositorio(IConfiguration config)
        {
            cadenaConexion = config["ConnectionStrings:BD"] ?? "";
        }

        public bool Actualizar(Cliente entidad)
        {
            bool exito = false;

            return exito;
        }

        public bool Eliminar(int id)
        {
            bool exito = false;
            return exito;
        }

        public List<Cliente> Listar()
        {
            List<Cliente> listado = new List<Cliente>();
            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();
                using (SqlCommand comando = new SqlCommand("ListarClientes", conexion))
                {
                    comando.CommandType = System.Data.CommandType.StoredProcedure;
                    using (SqlDataReader lector = comando.ExecuteReader())
                    {
                        if (lector != null && lector.HasRows)
                        {
                            while (lector.Read())
                            {
                                listado.Add(new Cliente()
                                {
                                     ID = lector.GetInt32(0),
                                     Apellidos = lector.GetString(1),
                                     Nombres = lector.GetString(2),
                                     Direccion = lector.GetString(3),
                                     Telefono = lector.GetString(4),
                                     Email = lector.GetString(5),
                                     TipoClienteID = lector.GetInt32(6),
                                     Activo = lector.GetBoolean(7)
                                });
                            }
                        }
                    }
                }
            }
            return listado;
        }

        public Cliente ObtenerPorID(int id)
        {
            Cliente cliente = null;
            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();
                using (SqlCommand comando = new SqlCommand("ObtenerCliente", conexion))
                {
                    comando.CommandType = System.Data.CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@Id", id);
                    using (SqlDataReader lector = comando.ExecuteReader())
                    {
                        if(lector != null && lector.HasRows)
                        {
                            lector.Read();
                            cliente = new Cliente()
                            {
                                ID = lector.GetInt32(0),
                                Apellidos = lector.GetString(1),
                                Nombres = lector.GetString(2),
                                Direccion = lector.GetString(3),
                                Telefono = lector.GetString(4),
                                Email = lector.GetString(5),
                                TipoClienteID = lector.GetInt32(6),
                                Activo = lector.GetBoolean(7)
                            };
                        }
                    }
                }
            }
            return cliente;
        }

        public int Registrar(Cliente entidad)
        {
            int nuevoID = 0;
            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();
                using (SqlCommand comando = new SqlCommand("RegistrarCliente", conexion))
                {
                    comando.CommandType = System.Data.CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@Apellidos", entidad.Apellidos);
                    comando.Parameters.AddWithValue("@Nombres", entidad.Nombres);
                    comando.Parameters.AddWithValue("@Direccion", entidad.Direccion);
                    comando.Parameters.AddWithValue("@Telefono", entidad.Telefono);
                    comando.Parameters.AddWithValue("@Email", entidad.Email);
                    comando.Parameters.AddWithValue("@Tipo", entidad.TipoClienteID);

                    nuevoID = Convert.ToInt32(comando.ExecuteScalar());
                    
                    /*
                     * 
                        comando.ExecuteReader(); => Lectura de datos tipo tabla
                        comando.ExecuteNonQuery(); => Cantidad filas afectadas(Insert, Update, Delete)
                        comando.ExecuteScalar(); => Devuelve un solo Valor
                    */
                }

            }
            return nuevoID;
        }
    }
}
