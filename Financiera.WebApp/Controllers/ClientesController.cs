using Financiera.BusinessLogic;
using Financiera.Model;
using Microsoft.AspNetCore.Mvc;

namespace Financiera.WebApp.Controllers
{
    public class ClientesController : Controller
    {
        private readonly ClienteBL clienteBL;

        public ClientesController(IConfiguration config)
        {
            clienteBL = new ClienteBL(config);
        }

        public IActionResult Index()
        {
            var listado = clienteBL.Listar();
            //Paginación
            return View(listado);
        }

        public IActionResult Create()
        {
            return View(new Cliente());
        }

        [HttpPost]
        public IActionResult Create(Cliente nuevoCliente)
        {
            int nuevoID = clienteBL.Registrar(nuevoCliente);
            return RedirectToAction("Details", new { id = nuevoID });
        }

        public IActionResult Details(int id)
        {
            Cliente clienteBuscado = clienteBL.ObtenerPorID(id);
            return View(clienteBuscado);
        }
    }
}
