using ApartadoAulas.Models;
using System.Data.SqlClient;
using System.Data;
namespace ApartadoAulas.Datos
{
    public class CarreraDatos
    {
        public List<CarreraModel> Listar()
        {
            List<CarreraModel> oLista = new List<CarreraModel>();
            var cn = new Conexion();
            using (var conexion = new SqlConnection(cn.getCadenaSql()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_ListarCarrera", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        oLista.Add(new CarreraModel()
                        {
                            IdCarrera = Convert.ToInt32(dr["IdCarrera"]),
                            Nombre = dr["Nombre"].ToString()
                        });
                    }
                }
            }
            return oLista;
        }

        public CarreraModel ConsultarCarrera(int IdCarrera)
        {
            var oCarrera = new CarreraModel();
            var cn = new Conexion();

            using (var Conexion = new SqlConnection(cn.getCadenaSql()))
            {
                Conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_ConsultarCarrera", Conexion);

                cmd.Parameters.AddWithValue("IdCarrera", IdCarrera);
                cmd.CommandType = CommandType.StoredProcedure;
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        oCarrera.IdCarrera = Convert.ToInt32(dr["IdCarrera"]);
                        oCarrera.Nombre = dr["Nombre"].ToString();
                    }
                }
            }
            return oCarrera;

        }

        public bool GuardarCarrera(CarreraModel model)

        {
            bool respuesta;
            try
            {
                var cn = new Conexion();
                using (var conexion = new SqlConnection(cn.getCadenaSql()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_GuardarCarrera", conexion);
                    cmd.Parameters.AddWithValue("Nombre", model.Nombre);
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
        public bool ActualizarCarrera(CarreraModel model)
        {
            bool respuesta;
            try
            {
                var cn = new Conexion();
                using (var conexion = new SqlConnection(cn.getCadenaSql()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_EditarCarrera", conexion);
                    cmd.Parameters.AddWithValue("IdEdificio", model.IdCarrera);
                    cmd.Parameters.AddWithValue("Nombre", model.Nombre);
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
        public bool EliminarCarrera(int IdCarrera)
        {
            bool respuesta;
            try
            {
                var cn = new Conexion();
                using (var conexion = new SqlConnection(cn.getCadenaSql()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_EliminarCarrera", conexion);
                    cmd.Parameters.AddWithValue("IdCarrera", IdCarrera);
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

