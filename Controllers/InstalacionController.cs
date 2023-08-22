using ApartadoAulas.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProyectoXDDD.Datos;
using ApartadoAulas.Datos;

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

        InstalacionModel instalacion = new InstalacionModel();
        InstalacionDatos __instalacionDatos = new InstalacionDatos();
        public IActionResult ListarInstalaciones()
        {
            var listaInstalaciones = __instalacionDatos.ObtenerListaDeInstalaciones();
            return View(listaInstalaciones);
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
                return RedirectToAction("ListarInstalaciones");
            }
            else
            {

                return View();
            }
        }



        public IActionResult Editar(int IdInstalacion)
        {
            InstalacionModel _instalacion = _instalacionDatos.ObtenerInstalacion(IdInstalacion);

            List<EdificioModel> lista = _instalacionDatos.ObtenerListaDeEdificios();
            List<SelectListItem> listaC = lista.ConvertAll(Item => new SelectListItem()
            {
                Text = Item.Nombre.ToString(),
                Value = Item.IdEdificio.ToString(),
                Selected = (Item.IdEdificio == _instalacion.refEdificio.IdEdificio) // Marcar el edificio actualmente seleccionado
            }
            );

            ViewBag.Lista = listaC;

            return View(_instalacion); // Pasar la instalación obtenida y la lista de edificios a la vista
        }

        // Acción de edición POST para actualizar
        [HttpPost]
        public IActionResult Editar(InstalacionModel model)
        {
            //if (!ModelState.IsValid)
            //{
            //     return View();
            //}
            var respuesta = _instalacionDatos.ActualizarInstalacion(model);
            if (respuesta)
            {
                return RedirectToAction("ListarInstalaciones");
            }
            else
            {
                //  redirigir a una página de error en caso de fallo
                return View();
            }
        }


        //pa mostar eliminar
        public IActionResult Eliminar(int IdInstalacion)
        {
            InstalacionModel _instalacion = _instalacionDatos.ObtenerInstalacion(IdInstalacion);
            return View(_instalacion);
        }


        //para eliminar
        [HttpPost]
        public IActionResult Eliminar(InstalacionModel model)
        {
            var respuesta = _instalacionDatos.EliminarInstalacion(model.IdInstalacion);
            if (respuesta)
            {
                return RedirectToAction("ListarInstalaciones");
            }
            else
            {
                return View();
            }
        }
    }
}

  