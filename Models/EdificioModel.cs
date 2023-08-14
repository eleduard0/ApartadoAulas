using System.ComponentModel.DataAnnotations;

namespace ApartadoAulas.Models
{
    public class EdificioModel
    {
        public int IdEdificio { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string Descripcion { get; set;}

    }
}
