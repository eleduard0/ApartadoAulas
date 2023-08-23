using ApartadoAulas.Models;
using System.Data.SqlClient;
using System.Data;
using ApartadoAulas.Datos;

namespace ApartadoAulas.Datos
{
    public class InstalacionDatos
    {
        public List<InstalacionModel> Listar()
        {
            List<InstalacionModel> lista = new List<InstalacionModel>();
            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.getCadenaSql()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("SP_ListarInstalaciones", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(new InstalacionModel
                        {
                            IdInstalacion = Convert.ToInt32(dr["IdInstalacion"]),
                            Nombre = dr["Nombre"].ToString(),
                            Descripcion = dr["Descripcion"].ToString(),
                            refEdificio = new EdificioModel
                            {
                                IdEdificio = Convert.ToInt32(dr["IdEdificio"]),
                                Nombre = dr["Nombre"].ToString(),
                                Descripcion = dr["Descripcion"].ToString()
                            }
                        });
                    }
                }
            }

            return lista;
        }

        public InstalacionModel ObtenerInstalacion(int IdInstalacion)
        {
            InstalacionModel _instalacion = new InstalacionModel();
            var cn = new Conexion();
            using (var conexion = new SqlConnection(cn.getCadenaSql()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_ObtenerInstalacion", conexion);
                cmd.Parameters.AddWithValue("IdInstalacion", IdInstalacion);
                cmd.CommandType = CommandType.StoredProcedure;
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        _instalacion.IdInstalacion = Convert.ToInt32(dr["IdInstalacion"]);
                        _instalacion.Nombre = dr["Nombre"].ToString();
                        _instalacion.Descripcion = dr["Descripcion"].ToString();
                        _instalacion.refEdificio = new EdificioModel
                        {
                            IdEdificio = Convert.ToInt32(dr["IdEdificio"]), // Asigna el IdEdificio
                            Nombre = dr["Nombre"].ToString(), // Asigna el Nombre del Edificio si está en el resultado
                            Descripcion = dr["Descripcion"].ToString() // Asigna la Descripción del Edificio si está en el resultado
                        };
                    }
                }
            }
            return _instalacion;
        }

        public bool GuardarInstalacion(InstalacionModel model)
        {
            bool respuesta;
            try
            {
                var cn = new Conexion();
                using (var conexion = new SqlConnection(cn.getCadenaSql()))
                {
                    conexion.Open();

                    SqlCommand cmd = new SqlCommand("SP_InsertarInstalacion", conexion);
                    cmd.Parameters.AddWithValue("Nombre", model.Nombre);
                    cmd.Parameters.AddWithValue("Descripcion", model.Descripcion);
                    cmd.Parameters.AddWithValue("IdEdificio", model.refEdificio.IdEdificio);

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
            return respuesta;
        }


        public bool ActualizarInstalacion(InstalacionModel model)
        {
            bool respuesta;
            try
            {
                var cn = new Conexion();
                using (var conexion = new SqlConnection(cn.getCadenaSql()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_ActualizarInstalacion", conexion);
                    cmd.Parameters.AddWithValue("IdInstalacion", model.IdInstalacion);
                    cmd.Parameters.AddWithValue("Nombre", model.Nombre);
                    cmd.Parameters.AddWithValue("Descripcion", model.Descripcion);
                    cmd.Parameters.AddWithValue("IdEdificio", model.refEdificio.IdEdificio);

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
            return respuesta;
        }

        public bool EliminarInstalacion(int IdInstalacion)
        {
            bool respuesta;
            try
            {
                var cn = new Conexion();
                using (var conexion = new SqlConnection(cn.getCadenaSql()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_EliminarInstalacion", conexion);
                    cmd.Parameters.AddWithValue("IdInstalacion", IdInstalacion);
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
            return respuesta;
        }

        public List<EdificioModel> ObtenerListaDeEdificios()
        {
            List<EdificioModel> listaEdificios = new List<EdificioModel>();
            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.getCadenaSql()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("SP_ListarEdificios", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        listaEdificios.Add(new EdificioModel
                        {
                            IdEdificio = Convert.ToInt32(dr["IdEdificio"]),
                            Nombre = dr["Nombre"].ToString(),
                            Descripcion = dr["Descripcion"].ToString()
                        });
                    }
                }
            }

            return listaEdificios;
        }

        public List<InstalacionModel> ObtenerListaDeInstalaciones()
        {
            List<InstalacionModel> listaInstalaciones = new List<InstalacionModel>();
            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.getCadenaSql()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("SP_ListarInstalacion", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        listaInstalaciones.Add(new InstalacionModel
                        {
                            IdInstalacion = Convert.ToInt32(dr["IdInstalacion"]),
                            Nombre = dr["Nombre"].ToString(),
                            Descripcion = dr["Descripcion"].ToString(),
                            refEdificio = new EdificioModel
                            {
                                IdEdificio = Convert.ToInt32(dr["IdEdificio"]),
                                Nombre = dr["NombreEdificio"].ToString(),
                                Descripcion = dr["DescripcionEdificio"].ToString()
                            }
                        });
                    }
                }
            }

            return listaInstalaciones;
        }
    }
}
