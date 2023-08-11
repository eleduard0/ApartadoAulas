namespace ApartadoAulas.Models
{
    public class ReservaModel
    {
        public int IdReserva { get; set; }
        public DateOnly Fecha { get; set; }
        public TimeOnly HoraInicio { get; set; }
        public TimeOnly HoraFin { get; set; }
        public UsuarioModel refUsuario { get; set; }
        public InstalacionModel refInstalacion { get; set;}
    }
}
