using Microsoft.AspNetCore.Mvc;
using ApartadoAulas.Models;
using ApartadoAulas.Datos;

namespace ApartadoAulas.Controllers
{
    public class EdificioController : Controller
    {
        EdificioDatos edificioDatos = new EdificioDatos();
        public IActionResult Listar()
        {
            var lista = edificioDatos.Listar();
            return View(lista);

        }

        [HttpGet]
        public IActionResult Guardar()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Guardar(EdificioModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var respuesta = edificioDatos.GuardarEdificio(model);
            if (respuesta)
            {
                return RedirectToAction("Listar");
            }
            else
            {
                return View();
            }
        }


        public IActionResult Editar(int IdEdificio)
        {
            EdificioModel edificio = edificioDatos.ConsultarEdificio(IdEdificio);

            return View(edificio);
        }
        [HttpPost]
        public IActionResult Editar(EdificioModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var respuesta = edificioDatos.ActualizarEdificio(model);
            if (respuesta)
            {
                return RedirectToAction("Listar");
            }
            else
            {
                return View();
            }
        }

        public IActionResult Eliminar(int IdEdificio)
        {
            var edificio = edificioDatos.ConsultarEdificio(IdEdificio);
            return View(edificio);
        }
        [HttpPost]
        public IActionResult Eliminar(EdificioModel model)
        {
            var respuesta = edificioDatos.EliminarEdificio(model.IdEdificio);
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