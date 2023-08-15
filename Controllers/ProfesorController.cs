using Microsoft.AspNetCore.Mvc;
using ApartadoAulas.Models;
using ApartadoAulas.Datos;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ApartadoAulas.Controllers
{
    public class ProfesorController : Controller
    {
        CarreraModel carrera = new CarreraModel();
        ProfesorDatos _profeDatos = new ProfesorDatos();
        public IActionResult Listar()
        {
            var lista = _profeDatos.ListarCarrera;
            return View(lista);
        }

        ProfesorModel instalacion = new ProfesorModel();
        ProfesorDatos __profeDatos = new ProfesorDatos();
        public IActionResult ListarProfesor()
        {
            var listaProfesor = _profeDatos.Listar();
            return View(listaProfesor);
        }

        //ProfesorDatos profesor = new ProfesorDatos();
        //CarreraDatos carrera = new CarreraDatos();
        public IActionResult Guardar()
        {
            List<CarreraModel> lista = _profeDatos.ListarCarrera();
            List<SelectListItem> listaC = lista.ConvertAll(
                item => new SelectListItem()
                {
                    Text = item.Nombre.ToString(),
                    Value = item.IdCarrera.ToString(),
                    Selected = false
                });
            ViewBag.Lista = listaC;
            return View();
        }
        [HttpPost]
        public IActionResult Guardar(ProfesorModel model)
        {
            /*if (!ModelState.IsValid)
             {
                 return View();
             }*/

            var profesorCreado = _profeDatos.GuardarProfesor(model);
            if (profesorCreado)
            {
                return RedirectToAction("ListarProfesor");
            }
            else
            {

                return View();
            }
        }
        public IActionResult Editar(int IdProfesor)
        {
            ProfesorModel _profesor = _profeDatos.ConsultarProfesor(IdProfesor);

            List<CarreraModel> lista = _profeDatos.ListarCarrera();
            List<SelectListItem> listaC = lista.ConvertAll(Item => new SelectListItem()
            {
                Text = Item.Nombre.ToString(),
                Value = Item.IdCarrera.ToString(),
                Selected = (Item.IdCarrera == _profesor.refCarrera.IdCarrera) // Marcar La carrera actualmente seleccionado
            }
            );

            ViewBag.Lista = listaC;

            return View(_profesor); // Pasar la instalación obtenida y la lista de edificios a la vista
        }

        // Acción de edición POST para actualizar
        [HttpPost]
        public IActionResult Editar(ProfesorModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var respuesta = _profeDatos.ActualizarProfesor(model);
            if (respuesta)
            {
                return RedirectToAction("ListarProfesor");
            }
            else
            {
                //  redirigir a una página de error en caso de fallo
                return View();
            }
        }
    }
}
