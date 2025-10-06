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
    public class TipoPrestamoBL
    {
        private readonly TipoPrestamoRepositorio tipoPrestamoDB;

        public TipoPrestamoBL(IConfiguration config)
        {
            tipoPrestamoDB = new TipoPrestamoRepositorio(config);
        }

        public List<TipoPrestamo> Listar()
        {
            return tipoPrestamoDB.Listar();
        }

        public TipoPrestamo ObtenerPorID(int id)
        {
            return tipoPrestamoDB.ObtenerPorID(id);
        }
    }
}
