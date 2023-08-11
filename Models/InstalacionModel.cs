namespace ApartadoAulas.Models
{
    public class InstalacionModel
    {
        public int IdInstalacion { get; set; }
        public string Nombre { get; set;}
        public string Descripcion { get; set;}
        public EdificioModel refEdificio { get; set; }
    }
}
