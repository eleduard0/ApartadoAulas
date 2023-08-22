using System.ComponentModel.DataAnnotations;

namespace ApartadoAulas.Models
{
    public class CarreraModel
    {
        public int IdCarrera { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string Nombre { get; set; }
    }
}