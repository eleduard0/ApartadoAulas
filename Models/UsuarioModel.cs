﻿namespace ApartadoAulas.Models
{
    public class UsuarioModel
    {
        public int IdUsuario { get; set; }
        public string Nombre{ get; set;}
        public string Email { get; set;}
        public string Contrasenia { get; set;}
        public ProfesorModel refProfesor { get; set; }

    }
}
