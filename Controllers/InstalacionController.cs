using ApartadoAulas.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProyectoXDDD.Datos;

namespace ApartadoAulas.Controllers
{
    public class InstalacionController : Controller
    {
        EdificioModel edificio = new EdificioModel();
        InstalacionDatos _instalacionDatos = new InstalacionDatos();
        public IActionResult Listar()
        {
            var lista = _instalacionDatos.ObtenerListaDeEdificios();
            return View(lista);
        }

        //para mostrar guardar
        public IActionResult Guardar()
        {
            List<EdificioModel> lista = _instalacionDatos.ObtenerListaDeEdificios();
            List<SelectListItem> listaC = lista.ConvertAll(Item => new SelectListItem()
                { Text = Item.Nombre.ToString(),
                Value = Item.IdEdificio.ToString(),
                Selected = false }
            );

            ViewBag.Lista = listaC;
            return View();

            //ViewBag.Edificios = _instalacionDatos.ObtenerListaDeEdificios(); // Utiliza el método para obtener la lista de edificios
            //return View(new InstalacionModel()); // Pasar un nuevo objeto InstalacionModel a la vista
        }


        //para guardar
        [HttpPost]
        public IActionResult Guardar(InstalacionModel model)
        {
           /*if (!ModelState.IsValid)
            {
                return View();
            }*/

            var instalacionCreada = _instalacionDatos.GuardarInstalacion(model);
            if (instalacionCreada)
            {
                return RedirectToAction("Listar");
            }
            else
            {

                return View();
            }
        }



        //para mostrar editar
        public IActionResult Editar(int IdInstalacion)
        {
            InstalacionModel _instalacion = _instalacionDatos.ObtenerInstalacion(IdInstalacion);
            return View(_instalacion); // Pasar la instalacion obtenido a la vista
        }

        // Acción de edición POST para actualizar
        [HttpPost]
        public IActionResult Editar(InstalacionModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var respuesta = _instalacionDatos.ActualizarInstalacion(model);
            if (respuesta)
            {
                return RedirectToAction("Listar");
            }
            else
            {
                //  redirigir a una página de error en caso de fallo
                return View();
            }
        }


        //para mostrar eliminar
        public IActionResult Eliminar(int IdInstalacion)
        {
            var _instalacion = _instalacionDatos.EliminarInstalacion(IdInstalacion);
            return View(_instalacionDatos);
        }

        //para eliminar
        [HttpPost]
        public IActionResult Eliminar(InstalacionModel model)
        {
            var respuesta = _instalacionDatos.EliminarInstalacion(model.IdInstalacion);
            if (respuesta)
            {
                return RedirectToAction("Listar");
            }
            else
            {
                return View();
            }
        }
    }
}

  