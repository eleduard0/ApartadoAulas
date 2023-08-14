using System.ComponentModel.DataAnnotations;

namespace ApartadoAulas.Models
{
    public class ProfesorModel
    {
        public int IdProfesor { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string ApePa { get; set;}
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string ApeMa { get; set;}
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string Email { get; set;}
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public CarreraModel refCarrera { get; set; }
    }
}
