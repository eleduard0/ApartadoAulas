using ApartadoAulas.Datos;
using ApartadoAulas.Models;
using ApartadoAulas.Recurso;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProyectoXDDD.Datos;
using System.Threading.Tasks;

namespace ApartadoAulas.Controllers
{
    public class LoginRController : Controller
    {
        LoginUsuario logR = new LoginUsuario();

        CarreraModel carrera = new CarreraModel();
        ProfesorDatos _profesorDatos = new ProfesorDatos();

        public IActionResult Listar()
        {
            var lista = _profesorDatos.ObtenerListaDeCarreras();
            return View(lista);
        }

        public IActionResult Registro()
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
        public IActionResult Registro(ProfesorModel model)
        {
           //if(!ModelState.IsValid)
           // {
           //     return View();
           // }
           model.Contrasenia=Utilidad.EncriptarClave(model.Contrasenia);
           bool crearUsuario = logR.Registro(model);
           if(!crearUsuario)
            {
                ViewData["Mensaje"] = "El correo ingresado ya se encuentra registrado";
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }


        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Login(string correo, string clave)
        {
            ProfesorModel profesor = logR.ValidarUsuario(correo, Utilidad.EncriptarClave(clave));
            if (profesor.IdProfesor == 0)
            {
                ViewData["Mensaje"] = "El correo o la clave son incorrectos";
                return View();
            }

            List<Claim> claims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, profesor.Nombre)
    };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            AuthenticationProperties properties = new AuthenticationProperties
            {
                AllowRefresh = true
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity), properties);

            return RedirectToAction("Index", "Home");
        }

        public IActionResult CambiarClave()
        {
            return View();
        }
        [HttpPost]

        public IActionResult CambiarClave(string correo, string clave)
        {
            bool respuesta = logR.CambiarClave(correo,Utilidad.EncriptarClave(clave));
            if(!respuesta)
            {
                ViewData["Mensaje"] = "El correo no existe";
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
        // ... Otros campos y métodos ...

        public IActionResult RecuperarClave()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RecuperarClave(string correo)
        {
            // Aquí deberías implementar la lógica para generar un token de recuperación y enviar un correo electrónico al usuario.
            // Puedes utilizar bibliotecas como SendGrid para enviar correos electrónicos.

            // Una vez que se ha enviado el correo, puedes redirigir a una página de confirmación.
            return RedirectToAction("RecuperacionEnviada");
        }

        public IActionResult RecuperacionEnviada()
        {
            return View();
        }

        public IActionResult RestablecerClave(string token)
        {
            // Aquí podrías mostrar un formulario donde los usuarios puedan ingresar una nueva contraseña asociada al token recibido.
            // La lógica para restablecer la contraseña podría ser similar a la que usas en la acción CambiarClave.

            return View();
        }

        [HttpPost]
        public IActionResult RestablecerClave(string token, string nuevaClave)
        {
            // Aquí deberías implementar la lógica para restablecer la contraseña asociada al token recibido.

            // Una vez que la contraseña ha sido restablecida, puedes redirigir a una página de confirmación.
            return RedirectToAction("RestablecimientoExitoso");
        }

        public IActionResult RestablecimientoExitoso()
        {
            return View();
        }

        // ... Otros métodos ...
    }
}

    

