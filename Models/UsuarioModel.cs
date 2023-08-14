using System.ComponentModel.DataAnnotations;

namespace ApartadoAulas.Models
{
    public class UsuarioModel
    {
        public int IdUsuario { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string Nombre{ get; set;}
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string Email { get; set;}
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string Contrasenia { get; set;}
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public ProfesorModel refProfesor { get; set; }

    }
}
