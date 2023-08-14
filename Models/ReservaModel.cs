using System.ComponentModel.DataAnnotations;

namespace ApartadoAulas.Models
{
    public class ReservaModel
    {
        public int IdReserva { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public DateOnly Fecha { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public TimeOnly HoraInicio { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public TimeOnly HoraFin { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public UsuarioModel refUsuario { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public InstalacionModel refInstalacion { get; set;}
    }
}
