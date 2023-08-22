using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ApartadoAulas.Models;
using ProyectoXDDD.Datos;
using ApartadoAulas.Datos;

namespace ApartadoAulas.Controllers
{
    public class ReservaController : Controller
    {
        private ReservaDatos _reservaDatos = new ReservaDatos();

        public IActionResult Listar()
        {
            var lista = _reservaDatos.Listar();
            return View(lista);
        }

        public IActionResult Guardar()
        {
            // Obtener la lista de edificios y cargarla en el selector
            var listaEdificios = _reservaDatos.ObtenerListaDeEdificios();
            ViewBag.ListaEdificios = new SelectList(listaEdificios, "IdEdificio", "Nombre");

            // Crear una lista vacía de instalaciones (se llenará en la vista a través de JavaScript)
            var listaInstalaciones = new List<InstalacionModel>();
            ViewBag.ListaInstalaciones = new SelectList(listaInstalaciones, "IdInstalacion", "Nombre");

            return View();
        }

        [HttpPost]
        public IActionResult Guardar(ReservaModel model)
        {
            if (ModelState.IsValid)
            {
                var reservaCreada = _reservaDatos.GuardarReserva(model);
                if (reservaCreada)
                {
                    return RedirectToAction("Listar");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Error al crear la reserva.");
                }
            }

            // Si hay errores, recargar las listas de edificios e instalaciones
            var listaEdificios = _reservaDatos.ObtenerListaDeEdificios();
            ViewBag.ListaEdificios = new SelectList(listaEdificios, "IdEdificio", "Nombre");
            var listaInstalaciones = _reservaDatos.ObtenerListaDeInstalacionesPorEdificio(model.refInstalacion.refEdificio.IdEdificio);

            ViewBag.ListaInstalaciones = new SelectList(listaInstalaciones, "IdInstalacion", "Nombre");

            return View(model);
        }

        // Este método se llama mediante una solicitud AJAX para obtener las instalaciones según el edificio seleccionado
        [HttpPost]
        public JsonResult ObtenerInstalacionesPorEdificio(int idEdificio)
        {
            var listaInstalaciones = _reservaDatos.ObtenerListaDeInstalacionesPorEdificio(idEdificio);
            var instalacionesSelectList = new SelectList(listaInstalaciones, "IdInstalacion", "Nombre");
            return Json(instalacionesSelectList);
        }

        public IActionResult Editar(int IdReserva)
        {
            var reserva = _reservaDatos.ObtenerReserva(IdReserva);

            // Obtener la lista de edificios y cargarla en el selector
            var listaEdificios = _reservaDatos.ObtenerListaDeEdificios();
            ViewBag.ListaEdificios = new SelectList(listaEdificios, "IdEdificio", "Nombre");

            // Obtener la lista de instalaciones según el edificio de la reserva y cargarla en el selector
            var listaInstalaciones = _reservaDatos.ObtenerListaDeInstalacionesPorEdificio(reserva.refInstalacion.refEdificio.IdEdificio);
            ViewBag.ListaInstalaciones = new SelectList(listaInstalaciones, "IdInstalacion", "Nombre");

            return View(reserva);
        }

        [HttpPost]
        public IActionResult Editar(ReservaModel model)
        {
            if (ModelState.IsValid)
            {
                var respuesta = _reservaDatos.ActualizarReserva(model);
                if (respuesta)
                {
                    return RedirectToAction("Listar");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Error al actualizar la reserva.");
                }
            }

            // Si hay errores, recargar las listas de edificios e instalaciones
            var listaEdificios = _reservaDatos.ObtenerListaDeEdificios();
            ViewBag.ListaEdificios = new SelectList(listaEdificios, "IdEdificio", "Nombre");

            var idEdificio = model.refEdificio.IdEdificio;
            var listaInstalaciones = _reservaDatos.ObtenerListaDeInstalacionesPorEdificio(idEdificio);
            ViewBag.ListaInstalaciones = new SelectList(listaInstalaciones, "IdInstalacion", "Nombre");

            return View(model);
        }

        public IActionResult Eliminar(int IdReserva)
        {
            var reserva = _reservaDatos.ObtenerReserva(IdReserva);
            return View(reserva);
        }

        [HttpPost]
        public IActionResult Eliminar(ReservaModel model)
        {
            var respuesta = _reservaDatos.EliminarReserva(model.IdReserva);
            if (respuesta)
            {
                return RedirectToAction("Listar");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Error al eliminar la reserva.");
                return View(model);
            }
        }
    }
}
