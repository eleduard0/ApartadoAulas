namespace ApartadoAulas.Models
{
    public class ReservaModel
    {
     
            public int IdReserva { get; set; }
            public DateTime Fecha { get; set; } // Cambiado de DateOnly a DateTime
            public TimeSpan HoraInicio { get; set; }
            public TimeSpan HoraFin { get; set; }
            public ProfesorModel refProfesor { get; set; }
            public InstalacionModel refInstalacion { get; set; }
            public EdificioModel refEdificio { get; set; }




    }
}
