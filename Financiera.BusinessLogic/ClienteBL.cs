using Financiera.Data;
using Financiera.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financiera.BusinessLogic
{
    public class ClienteBL
    {
        private readonly ClienteRespositorio clienteBD;

        public ClienteBL(IConfiguration config)
        {
            clienteBD = new ClienteRespositorio(config);
        }

        public List<Cliente> Listar()
        {
            return clienteBD.Listar();
        }

        public int Registrar(Cliente cliente)
        {
            return clienteBD.Registrar(cliente);
        }

        public Cliente ObtenerPorID(int id)
        {
            return clienteBD.ObtenerPorID(id);
        }
    }
}
