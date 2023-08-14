using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace ApartadoAulas.Models
{
    public class InstalacionModel
    {
        public int IdInstalacion { get; set; }
        public string Nombre { get; set;}
        public string Descripcion { get; set;}
        [Display(Name = "Edificio")]
        [BindNever]
        public EdificioModel refEdificio { get; set; }


    }
}
