using ApartadoAulas.Models;
using System.Data.SqlClient;
using System.Data;
namespace ApartadoAulas.Datos
{
    public class EdificioDatos
    {
         public List<EdificioModel> Listar()
        {
            List<EdificioModel> oLista= new List<EdificioModel>();
            var cn=new Conexion();
            using(var conexion=new SqlConnection(cn.getCadenaSql()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_ListarEdificio", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                using(var dr=cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        oLista.Add(new EdificioModel()
                        {
                            IdEdificio = Convert.ToInt32(dr["IdEdificio"]),
                            Nombre = dr["Nombre"].ToString(),
                            Descripcion = dr["Descripcion"].ToString()
                        });
                    }
                }
            }
            return oLista;  
        }
        
        public EdificioModel ConsultarEdificio(int IdEdificio)
        {
            var oEdificio = new EdificioModel();
            var cn = new Conexion();

            using (var Conexion = new SqlConnection(cn.getCadenaSql()))
            {
                Conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_ConsultarEdificio", Conexion);

                cmd.Parameters.AddWithValue("IdEdificio", IdEdificio);
                cmd.CommandType = CommandType.StoredProcedure;
                using(var dr=cmd.ExecuteReader())
                {
                    while(dr.Read())
                    {
                        oEdificio.IdEdificio = Convert.ToInt32(dr["IdEdificio"]);
                        oEdificio.Nombre = dr["Nombre"].ToString();
                        oEdificio.Descripcion = dr["Descripcion"].ToString();

                    }
                }
            }
            return oEdificio;

        }

        public bool GuardarEdificio (EdificioModel model)
            
        {
            bool respuesta;
            try
            {
                var cn=new Conexion();
                using(var conexion=new SqlConnection(cn.getCadenaSql()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_CrearEdificio", conexion);
                    cmd.Parameters.AddWithValue("Nombre", model.Nombre);
                    cmd.Parameters.AddWithValue("Descripcion", model.Descripcion);
                    cmd.CommandType= CommandType.StoredProcedure;

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
        public bool ActualizarEdificio (EdificioModel model)
        {
            bool respuesta;
            try
            {
                var cn = new Conexion();
                using (var conexion = new SqlConnection(cn.getCadenaSql())) 
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_ActualizarEdificio", conexion);
                    cmd.Parameters.AddWithValue("IdEdificio", model.IdEdificio);
                    cmd.Parameters.AddWithValue("Nombre", model.Nombre);
                    cmd.Parameters.AddWithValue("Descripcion", model.Descripcion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.ExecuteNonQuery();
                }
                respuesta = true;
            }       
            catch (Exception e)
            {
                string error = e.Message;
                respuesta= false;
            }
            return respuesta;
        }
        public bool EliminarEdificio(int IdEdificio)
        {
            bool respuesta;
            try
            {
              var cn= new Conexion();
              using(var conexion=new SqlConnection(cn.getCadenaSql()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_EliminarEdificio", conexion);
                    cmd.Parameters.AddWithValue("IdEdificio", IdEdificio);
                    cmd.CommandType=CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }
              respuesta = true;
            }
            catch(Exception e)
            {
                string error = e.Message;
                respuesta= false;
            }
            return respuesta;
        }

    }
}
