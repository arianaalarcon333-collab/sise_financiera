using Financiera.BusinessLogic;
using Financiera.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Financiera.WebApp.Controllers
{
    public class PrestamoController : Controller
    {
        private readonly PrestamoBL prestamoBL;
        private readonly ClienteBL clienteBL;
        private readonly TipoPrestamoBL tipoPrestamoBL;

        public PrestamoController(IConfiguration config)
        {
            prestamoBL = new PrestamoBL(config);
            clienteBL = new ClienteBL(config);
            tipoPrestamoBL = new TipoPrestamoBL(config);
        }

        public IActionResult Index()
        {
            List<Prestamo> listado = prestamoBL.Listar();
            return View(listado);
        }

        public IActionResult Create()
        {
            ViewBag.clientes = new SelectList(clienteBL.Listar(), "ID", "NombreCompleto");
            ViewBag.tipos = new SelectList(tipoPrestamoBL.Listar(), "ID", "Nombre");
            return View(new Prestamo());
        }

        [HttpPost]
        public IActionResult Create(Prestamo prestamo)
        {
            int nuevoID = 0;
            //CONTROLADOR DE EXCEPCIONES
            try
            {
                // PROCESOS QUE PODRÍAN GENERAR UNA EXCEPCION
                nuevoID = prestamoBL.Registrar(prestamo);
            }
            catch (Exception ex)
            {
                ViewBag.mensajeAdicional = ex.Message;
            }
            
            if (nuevoID > 0)
                return RedirectToAction("Details", new { id = nuevoID });
            else
            {
                ViewBag.mensaje = "No se ha podido registrar el préstamo.";
                ViewBag.clientes = new SelectList(clienteBL.Listar(), "ID", "NombreCompleto");
                ViewBag.tipos = new SelectList(tipoPrestamoBL.Listar(), "ID", "Nombre");
                return View(prestamo);
            }                
        }

        public IActionResult ObtenerTipoPrestamo(int id)
        {
            TipoPrestamo tipoPrestamoBuscado = tipoPrestamoBL.ObtenerPorID(id);
            return new JsonResult(tipoPrestamoBuscado);
        }

    }
}
