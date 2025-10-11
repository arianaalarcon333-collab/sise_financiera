using Biblioteca.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;

namespace Biblioteca.Controllers
{
    public class LibroController : Controller
    {
        private readonly string cadenaConexion;
        public LibroController(IConfiguration config)
        {
            cadenaConexion = config["ConnectionStrings:BD"] ?? "";
        }

        private List<Libro> listarLibros()
        {
            var listado = new List<Libro>();
            using (var conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();
                string query = @"SELECT l.ID, l.Titulo, l.Publicacion, 
                                l.IdAutor, a.Nombre AS AutorNombre,
                                l.IdCategoria, c.Nombre AS CategoriaNombre
                         FROM Libros l
                         INNER JOIN Autores a ON l.IdAutor = a.ID
                         INNER JOIN Categorias c ON l.IdCategoria = c.ID";

                using (var command = new SqlCommand(query, conexion))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                listado.Add(new Libro()
                                {
                                    ID = reader.GetInt32(0),
                                    Titulo = reader.GetString(1),
                                    Publicacion = reader.GetInt32(2),
                                    IdAutor = reader.GetInt32(3),
                                    Autor = new Autor { Nombre = reader.GetString(4) },
                                    IdCategoria = reader.GetInt32(5),
                                    Categoria = new Categoria { Nombre = reader.GetString(6) }
                                });
                            }
                        }
                    }
                }
            }
            return listado;
        }

        private Libro obtenerLibroID(int idLibro)
        {
            Libro libro = null;

            using (var conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();
                using (var comando = new SqlCommand(
                    "SELECT * FROM Libros WHERE ID = " + idLibro, conexion))
                {
                    using (var lector = comando.ExecuteReader())
                    {
                        lector.Read();
                        libro = new Libro()
                        {
                            ID = lector.GetInt32(0),
                            Titulo = lector.GetString(1),
                            Publicacion = lector.GetInt32(2),
                            IdAutor = lector.GetInt32(3),
                            IdCategoria = lector.GetInt32(4),

                        };
                    }

                }
            }
            return libro;

        }

        public bool actualizarLibro(Libro libro)
        {
            var exito = false;
            using (var conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();
                using (var comando = new SqlCommand(@"UPDATE libros SET titulo=@ti,publicacion=@publi,
                   idautor=@idA,idcategoria=@idC WHERE id=@id", conexion))
                {
                    comando.Parameters.AddWithValue("@ti", libro.Titulo);
                    comando.Parameters.AddWithValue("@publi", libro.Publicacion);
                    comando.Parameters.AddWithValue("@idA", libro.IdAutor);
                    comando.Parameters.AddWithValue("@idC", libro.IdCategoria);
                    comando.Parameters.AddWithValue("@id", libro.ID);
                    exito = comando.ExecuteNonQuery() > 0;

                }

            }
            return exito;

        }

        private bool registrarLibro(Libro libro)
        {
            var exito = false;
            using (var conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();
                using (var comando = new SqlCommand(
                @"INSERT INTO libros(Titulo,Publicacion,IdAutor,IdCategoria)
                    VALUES(@ti,@publi,@auto,@cate)", conexion))
                {
                    comando.Parameters.AddWithValue("@ti", libro.Titulo);
                    comando.Parameters.AddWithValue("@publi", libro.Publicacion);
                    comando.Parameters.AddWithValue("@auto", libro.IdAutor);
                    comando.Parameters.AddWithValue("@cate", libro.IdCategoria);
                    exito = comando.ExecuteNonQuery() > 0;
                }
            }

            return exito;


        }



        private bool eliminar(Libro libro)
        {
            var exito = false;

            using (var conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();
                using (var comando = new SqlCommand(@"DELETE FROM Libros WHERE id=@id", conexion))
                {
                    comando.Parameters.AddWithValue("@id", libro.ID);
                    exito = comando.ExecuteNonQuery() > 0;

                }
            }
            return exito;

        }



        private List<Autor> listarAutores()
        {
            var listado = new List<Autor>();
            using (var conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();
                using (var comando = new SqlCommand("SELECT * FROM Autores", conexion))
                {
                    using (var reader = comando.ExecuteReader())
                    {
                        if (reader != null && reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                listado.Add(new Autor()
                                {
                                    ID = reader.GetInt32(0),
                                    Nombre = reader.GetString(1),
                                    Nacionalidad = reader.GetString(2),
                                    FechaNacimiento = reader.GetDateTime(3),



                                });
                            }
                        }
                    }
                }

            }
            return listado;
        }

        private List<Categoria> listarCategorias()
        {
            var listado = new List<Categoria>();
            using (var conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();
                using (var comando = new SqlCommand("SELECT * FROM Categorias", conexion))
                {
                    using (var reader = comando.ExecuteReader())
                    {
                        if (reader != null && reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                listado.Add(new Categoria()
                                {
                                    ID = reader.GetInt32(0),
                                    Nombre = reader.GetString(1)
                                });
                            }
                        }
                    }
                }
            }
            return listado;
        }





        public IActionResult Index(int page, int tipo = 0, int tipo1 = 0)
        {
            var listado = listarLibros();

            if (tipo > 0)
                listado = listado.Where(l => l.ID == tipo).ToList();


            if (tipo1 > 0)
                listado = listado.Where(l => l.ID == tipo1).ToList();



            int totalRegistros = listado.Count();
            int registrosPorPagina = 10;
            int cantidadDePaginas = (Int32)Math.Ceiling((double)totalRegistros / registrosPorPagina);


            ViewBag.TiposCategorias = new SelectList(listarCategorias(), "ID", "Nombre", tipo);
            ViewBag.TiposAutores = new SelectList(listarAutores(), "ID", "Nombre", tipo1);

            ViewBag.cantidadDePaginas = cantidadDePaginas;
            int omitir = registrosPorPagina * (page - 1);
            return View(listado.Skip(omitir).Take(registrosPorPagina));


        }


        public IActionResult Detalle(int id)
        {
            Libro libro = obtenerLibroID(id);
            return View(libro);
        }

        public IActionResult Create()
        {
            return View(new Libro());
        }

        [HttpPost]
        public IActionResult Create(Libro libro)
        {
            if (!ModelState.IsValid)
                return View(libro);
            var exito = registrarLibro(libro);
            if (!exito)
            {
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        public IActionResult edit(int id)
        {
            var libro = obtenerLibroID(id);
            return View(libro);

        }

        [HttpPost]
        public IActionResult Edit(Libro libro)

        {
            var exito = actualizarLibro(libro);
            if (!exito)
                return View(libro);
            return RedirectToAction("Detalle", new { id = libro.ID });

        }

        public IActionResult Delete(int id)

        {
            var libro = obtenerLibroID(id);
            return View(libro);
        }

        [HttpPost]
        public IActionResult Delete(Libro libro)
        {

            var exito = eliminar(libro);
            return RedirectToAction("Index");
        }

    }
}
