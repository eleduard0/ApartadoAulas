using ApartadoAulas.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProyectoXDDD.Datos;
using ApartadoAulas.Datos;

namespace ApartadoAulas.Controllers
{
    public class ProfesorController : Controller
    {
        CarreraModel carrera = new CarreraModel();
        ProfesorDatos _profesorDatos = new ProfesorDatos();

        public IActionResult Listar()
        {
            var lista = _profesorDatos.ObtenerListaDeCarreras();
            return View(lista);
        }

        ProfesorModel profesor = new ProfesorModel();
        ProfesorDatos __profesorDatos = new ProfesorDatos();

        public IActionResult ListarProfesores()
        {
            var listaProfesores = __profesorDatos.Listar();
            return View(listaProfesores);
        }

        //para mostrar guardar
        public IActionResult Guardar()
        {
            List<CarreraModel> lista = _profesorDatos.ObtenerListaDeCarreras();
            List<SelectListItem> listaC = lista.ConvertAll(Item => new SelectListItem()
            {
                Text = Item.Nombre.ToString(),
                Value = Item.IdCarrera.ToString(),
                Selected = false
            }
            );

            ViewBag.ListaCarreras = listaC;
            return View();
        }

        //para guardar
        [HttpPost]
        public IActionResult Guardar(ProfesorModel model)
        {
            var profesorCreado = _profesorDatos.GuardarProfesor(model);
            if (profesorCreado)
            {
                return RedirectToAction("ListarProfesores");
            }
            else
            {
                return View();
            }
        }

        public IActionResult Editar(int IdProfesor)
        {
            ProfesorModel _profesor = _profesorDatos.ObtenerProfesor(IdProfesor);

            List<CarreraModel> lista = _profesorDatos.ObtenerListaDeCarreras();
            List<SelectListItem> listaC = lista.ConvertAll(Item => new SelectListItem()
            {
                Text = Item.Nombre.ToString(),
                Value = Item.IdCarrera.ToString(),
                Selected = (Item.IdCarrera == _profesor.refCarrera.IdCarrera)
            }
            );

            ViewBag.ListaCarreras = listaC;

            return View(_profesor);
        }

        // Acción de edición POST para actualizar
        [HttpPost]
        public IActionResult Editar(ProfesorModel model)
        {
            //if (!ModelState.IsValid)
            //{
              //  return View();
            //}

            var respuesta = _profesorDatos.ActualizarProfesor(model);
            if (respuesta)
            {
                return RedirectToAction("ListarProfesores");
            }
            else
            {
                return View();
            }
        }

        //pa mostar eliminar
        public IActionResult Eliminar(int IdProfesor)
        {
            ProfesorModel _profesor = _profesorDatos.ObtenerProfesor(IdProfesor);
            return View(_profesor);
        }

        //para eliminar
        [HttpPost]
        public IActionResult Eliminar(ProfesorModel model)
        {
            var respuesta = _profesorDatos.EliminarProfesor(model.IdProfesor);
            if (respuesta)
            {
                return RedirectToAction("ListarProfesores");
            }
            else
            {
                return View();
            }
        }
    }
}
