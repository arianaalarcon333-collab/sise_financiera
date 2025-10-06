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
    public class PrestamoBL
    {
        private readonly PrestamoRepositorio prestamoDB;
        private readonly CuotasPrestamoRepositorio cuotaDB;
        private readonly ClienteRespositorio clienteDB;
        private readonly TipoClienteRepositorio tipoClienteDB;

        public PrestamoBL(IConfiguration config)
        {
            prestamoDB = new PrestamoRepositorio(config);
            cuotaDB = new CuotasPrestamoRepositorio(config);
            clienteDB = new ClienteRespositorio(config);
            tipoClienteDB = new TipoClienteRepositorio(config);

        }

        public List<Prestamo> Listar()
        {
            return prestamoDB.Listar();
        }

        public Prestamo ObtenerPorID(int id)
        {
            return prestamoDB.ObtenerPorID(id);
        }

        public int Registrar(Prestamo prestamo)
        {
            // CREAR EXCEPCIONES PERSONALIZADAS
            // El plazo mínimo debe ser de 6 meses.
            if (prestamo.Plazo < 6)
            {
                // Propagar / Lanzar una excepción
                throw new Exception("El plazo mínimo es de 6 meses.");
            }

            //BUSCAR A CLIENTE
            Cliente cliente = clienteDB.ObtenerPorID(prestamo.ClienteID);
            if(cliente == null)
            {
                throw new Exception("El cliente no existe.");
            }

            //BUSCAR EL TIPO DE CLIENTE
            TipoCliente tipoCliente = tipoClienteDB.ObtenerPorID(cliente.TipoClienteID);
            if(tipoCliente == null)
            {
                throw new Exception("El tipo de cliente no existe.");
            }  

            //Si el cliente es INDIVIDUAL, el plazo mínimo para un préstamo es de 24 meses.
            if(tipoCliente.Nombre.Contains("INDIVIDUAL") && prestamo.Plazo < 24)
            {
                throw new Exception("El plazo mínimo para el tipo de cliente asociado es de 24 meses.");
            }


            //TODO:
            //Si el cliente es corporativo y el prestamo es de tipo Mi negocio, entonces se le asigna un 3% menos adicional


            // REGISTRO DEL PRÉSTAMO
            int nuevoID = prestamoDB.Registrar(prestamo);

            // REGISTRO DE LAS CUOTAS
            // Cuota fija mensual
            decimal cuotaMensual = prestamo.Importe / prestamo.Plazo;
            decimal porcentajeInteres = prestamo.Tasa / 100;
            decimal importeInteres = cuotaMensual * porcentajeInteres;
            CuotaPrestamo cuota;
            for (int idx = 1; idx <= prestamo.Plazo; idx++)
            {
                cuota = new CuotaPrestamo()
                {
                    PrestamoID = nuevoID,
                    NumeroCuota = idx,
                    Importe = cuotaMensual,
                    ImporteInteres = importeInteres,
                    Estado = "P",
                    FechaPago = prestamo.FechaDeposito.AddMonths(idx)
                };
                cuotaDB.Registrar(cuota);
            }
            return nuevoID;
        }
    }
}
