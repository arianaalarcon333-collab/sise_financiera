using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Biblioteca.Models
{
    public class Libro
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "El título es obligatorio.")]
        [StringLength(150, ErrorMessage = "El título no debe superar los 150 caracteres.")]
        public string Titulo { get; set; }


        [Required(ErrorMessage = "El año de publicación es obligatorio.")]
        [DisplayName("Publicación")]

        public int Publicacion { get; set; }


        [Required(ErrorMessage = "Debe seleccionar un autor.")]
        [DisplayName("Autor")]
        public int IdAutor { get; set; }

        [DisplayName("Categoria")]
        public int IdCategoria { get; set; }

        public Autor Autor { get; set; }
        public Categoria Categoria { get; set; }
    }
}