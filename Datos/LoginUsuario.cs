using ApartadoAulas.Models;
using System.Data.SqlClient;
using System.Data;
namespace ApartadoAulas.Datos
{
    public class LoginUsuario
    {

        public bool existeCorreo(string correo)
        {
            string eCorreo = "";
            var cn = new Conexion();
            using (var conexion = new SqlConnection(cn.getCadenaSql()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("SP_ValidarCorreo", conexion);
                cmd.Parameters.AddWithValue("Email", correo);
                cmd.CommandType = CommandType.StoredProcedure;
                using (var dr=cmd.ExecuteReader())
                {
                    while(dr.Read())
                    {
                        eCorreo = dr["Email"].ToString();
                    }
                }
            }
            if(eCorreo == "")
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public bool Registro(ProfesorModel model)
        {

            bool respuesta;
            
            if (existeCorreo(model.Email))
            {
                try
                {

                    var cn = new Conexion();
                    using (var conexion = new SqlConnection(cn.getCadenaSql()))
                    {
                        conexion.Open();
                        SqlCommand cmd = new SqlCommand("SP_InsertarProfesor", conexion);
                        cmd.Parameters.AddWithValue("Nombre", model.Nombre);
                        cmd.Parameters.AddWithValue("ApePa", model.ApePa);
                        cmd.Parameters.AddWithValue("ApeMa", model.ApeMa);
                        cmd.Parameters.AddWithValue("Email", model.Email);
                        cmd.Parameters.AddWithValue("Contrasenia", model.Contrasenia);
                        cmd.Parameters.AddWithValue("IdCarrera", model.refCarrera.IdCarrera);

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.ExecuteNonQuery();

                    }
                    respuesta = true;

                }
                catch (Exception ex)
                {
                    string error = ex.Message;
                    respuesta = false;
                }
            }
            else
            {
                respuesta = false;
            }

            return respuesta;
        }

        public ProfesorModel ValidarUsuario(string correo, string clave)
        {
            ProfesorModel profesor = new ProfesorModel();
            var cn = new Conexion();
            using (var conexion = new SqlConnection(cn.getCadenaSql()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("SP_ValidarUsuario", conexion);
                cmd.Parameters.AddWithValue("Email", correo);
                cmd.Parameters.AddWithValue("Contrasenia", clave);
                cmd.CommandType = CommandType.StoredProcedure;
                using(var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        profesor.IdProfesor = Convert.ToInt32(dr["IdProfesor"]);
                        profesor.Nombre = dr["Nombre"].ToString();
                        profesor.ApePa = dr["ApePa"].ToString();
                        profesor.ApeMa = dr["ApeMa"].ToString();
                        profesor.Email = dr["Email"].ToString();
                        profesor.Contrasenia = dr["Contrasenia"].ToString();
                     
                    }
                }
            }
            return profesor;
        }


        public bool CambiarClave(string correo, string clave)
        {

            bool respuesta;
            try
            {
                var cn = new Conexion();
                using (var conexion = new SqlConnection(cn.getCadenaSql()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_CambiarClave", conexion);
                    cmd.Parameters.AddWithValue("Email", correo);
                    cmd.Parameters.AddWithValue("Contrasenia", clave);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }
                respuesta = true;
            }
            catch(Exception ex)
            {
                string error = ex.Message;
                respuesta = false;
            }
            return respuesta;
        }


    }


}


