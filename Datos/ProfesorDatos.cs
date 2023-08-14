using ApartadoAulas.Models;
using System.Data.SqlClient;
using System.Data;
namespace ApartadoAulas.Datos
{
    public class ProfesorDatos
    {
        public List<ProfesorModel> Listar()
        {
            List<ProfesorModel> lista = new List<ProfesorModel>();
            var cn = new Conexion();
            using (var conexion = new SqlConnection(cn.getCadenaSql()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_ListarProfesor", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                    lista.Add(new ProfesorModel()
                    {
                        IdProfesor = Convert.ToInt32(dr["IdProfesor"]),
                        Nombre = dr["Nombre"].ToString(),
                        ApePa = dr["ApePa"].ToString(),
                        ApeMa = dr["ApeMa"].ToString(),
                        Email = dr["Email"].ToString(),
                        refCarrera = new CarreraModel()
                        {
                            IdCarrera = Convert.ToInt32(dr["IdCarrera"]),
                            Nombre = dr["Nombre"].ToString()
                        }
                    });
                            
                        
                    }
                }

            }
            return lista;
        }
        public ProfesorModel ConsultarProfesor(int IdProfesor)
        {
            ProfesorModel oProfesor = new ProfesorModel();
            var cn = new Conexion();

            using (var Conexion = new SqlConnection(cn.getCadenaSql()))
            {
                Conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_ConsultarProfesor", Conexion);

                cmd.Parameters.AddWithValue("IdProfesor", IdProfesor);
                cmd.CommandType = CommandType.StoredProcedure;
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        oProfesor.IdProfesor = Convert.ToInt32(dr["IdProfesor"]);
                        oProfesor.Nombre = dr["Nombre"].ToString();
                        oProfesor.ApePa = dr["ApePa"].ToString();
                        oProfesor.ApeMa = dr["ApeMa"].ToString();
                        oProfesor.Email = dr["Email"].ToString();
                        oProfesor.refCarrera = new CarreraModel()
                        {
                            IdCarrera = Convert.ToInt32(dr["IdCarrera"])
                        };
                    }   
                }       
            }
            return oProfesor;

        }
        public bool GuardarProfesor(ProfesorModel model)

        {
            bool respuesta;
            try
            {
                var cn = new Conexion();
                using (var conexion = new SqlConnection(cn.getCadenaSql()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_GuardarProfesor", conexion);
                    cmd.Parameters.AddWithValue("Nombre", model.Nombre);
                    cmd.Parameters.AddWithValue("ApePa", model.ApePa);
                    cmd.Parameters.AddWithValue("ApeMa", model.ApeMa);
                    cmd.Parameters.AddWithValue("Email", model.Email);
                    cmd.Parameters.AddWithValue("IdCarrera1", model.refCarrera.IdCarrera);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.ExecuteNonQuery();

                }
                respuesta = true;

            }
            catch (Exception e)
            {
                string error = e.Message;
                respuesta = false;
            }
            return respuesta;
        }
        public bool ActualizarProfesor(ProfesorModel model)

        {
            bool respuesta;
            try
            {
                var cn = new Conexion();
                using (var conexion = new SqlConnection(cn.getCadenaSql()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_ActualizarProfesor", conexion);
                    cmd.Parameters.AddWithValue("IdProfesor", model.IdProfesor);
                    cmd.Parameters.AddWithValue("Nombre", model.Nombre);
                    cmd.Parameters.AddWithValue("ApePa", model.ApePa);
                    cmd.Parameters.AddWithValue("ApeMa", model.ApeMa);
                    cmd.Parameters.AddWithValue("Email", model.Email);
                    cmd.Parameters.AddWithValue("IdCarrera1", model.refCarrera.IdCarrera);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.ExecuteNonQuery();

                }
                respuesta = true;

            }
            catch (Exception e)
            {
                string error = e.Message;
                respuesta = false;
            }
            return respuesta;
        }
        public bool EliminarProfesor(int IdProfesor)
        {
            bool respuesta;
            try
            {
                var cn = new Conexion();
                using (var conexion = new SqlConnection(cn.getCadenaSql()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_EliminarProfesor", conexion);
                    cmd.Parameters.AddWithValue("IdProfesor", IdProfesor);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }
                respuesta = true;
            }
            catch (Exception e)
            {
                string error = e.Message;
                respuesta = false;
            }
            return respuesta;
        }
    }
}


