namespace ApartadoAulas.Models
{
    public class ProfesorModel
    {
        public int IdProfesor { get; set; }
        public string Nombre { get; set; }
        public string ApePa { get; set;}
        public string ApeMa { get; set;}
        public string Email { get; set;}
        public CarreraModel refCarrera { get; set; }
    }
}
