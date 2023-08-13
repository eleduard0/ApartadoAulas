using Microsoft.AspNetCore.Mvc;
using ApartadoAulas.Models;
using ApartadoAulas.Datos;

namespace ApartadoAulas.Controllers
{
    public class CarreraController : Controller
    {
        CarreraDatos carreraDatos = new CarreraDatos();
        public IActionResult Listar()
        {
            var lista = carreraDatos.Listar();
            return View(lista);

        }

        [HttpGet]
        public IActionResult Guardar()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Guardar(CarreraModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var respuesta = carreraDatos.GuardarCarrera(model);
            if (respuesta)
            {
                return RedirectToAction("Listar");
            }
            else
            {
                return View();
            }
        }


        public IActionResult Editar(int IdCarrera)
        {
            CarreraModel edificio = carreraDatos.ConsultarCarrera(IdCarrera);

            return View(edificio);
        }
        [HttpPost]
        public IActionResult Editar(CarreraModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var respuesta = carreraDatos.ActualizarCarrera(model);
            if (respuesta)
            {
                return RedirectToAction("Listar");
            }
            else
            {
                return View();
            }
        }

        public IActionResult Eliminar(int IdCarrera)
        {
            var carrera = carreraDatos.ConsultarCarrera(IdCarrera);
            return View(carrera);
        }
        [HttpPost]
        public IActionResult Eliminar(CarreraModel model)
        {
            var respuesta = carreraDatos.EliminarCarrera(model.IdCarrera);
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
