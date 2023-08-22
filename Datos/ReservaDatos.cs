using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using ApartadoAulas.Datos;
using ApartadoAulas.Models;
using ProyectoXDDD.Datos;

namespace ProyectoXDDD.Datos
{
    public class ReservaDatos
    {
        public List<ReservaModel> Listar()
        {
            List<ReservaModel> lista = new List<ReservaModel>();
            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.getCadenaSql()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("SP_ListarReservas", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(new ReservaModel
                        {
                            IdReserva = Convert.ToInt32(dr["IdReserva"]),
                            Fecha = Convert.ToDateTime(dr["Fecha"]),
                            HoraInicio = TimeSpan.Parse(dr["HoraInicio"].ToString()),
                            HoraFin = TimeSpan.Parse(dr["HoraFin"].ToString()),
                            refProfesor = new ProfesorModel
                            {
                                IdProfesor = Convert.ToInt32(dr["IdProfesor"]),
                                Nombre = dr["Nombre"].ToString(),
                                ApePa = dr["ApePa"].ToString(),
                                ApeMa = dr["ApeMa"].ToString(),
                                Email = dr["Email"].ToString()
                            },
                            refInstalacion = new InstalacionModel
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
                            }
                        });
                    }
                }
            }

            return lista;
        }

        public ReservaModel ObtenerReserva(int IdReserva)
        {
            ReservaModel _reserva = new ReservaModel();
            var cn = new Conexion();
            using (var conexion = new SqlConnection(cn.getCadenaSql()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_ObtenerReserva", conexion);
                cmd.Parameters.AddWithValue("IdReserva", IdReserva);
                cmd.CommandType = CommandType.StoredProcedure;
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        _reserva.IdReserva = Convert.ToInt32(dr["IdReserva"]);
                        _reserva.Fecha = Convert.ToDateTime(dr["Fecha"]);
                        _reserva.HoraInicio = TimeSpan.Parse(dr["HoraInicio"].ToString());
                        _reserva.HoraFin = TimeSpan.Parse(dr["HoraFin"].ToString());
                        _reserva.refProfesor = new ProfesorModel
                        {
                            IdProfesor = Convert.ToInt32(dr["IdProfesor"]),
                            Nombre = dr["ProfesorNombre"].ToString(),
                            ApePa = dr["ProfesorApePa"].ToString(),
                            ApeMa = dr["ProfesorApeMa"].ToString(),
                            Email = dr["ProfesorEmail"].ToString()
                        };
                        _reserva.refInstalacion = new InstalacionModel
                        {
                            IdInstalacion = Convert.ToInt32(dr["IdInstalacion"]),
                            Nombre = dr["InstalacionNombre"].ToString(),
                            Descripcion = dr["InstalacionDescripcion"].ToString(),
                            refEdificio = new EdificioModel
                            {
                                IdEdificio = Convert.ToInt32(dr["IdEdificio"]),
                                Nombre = dr["EdificioNombre"].ToString(),
                                Descripcion = dr["EdificioDescripcion"].ToString()
                            }
                        };
                    }
                }
            }
            return _reserva;
        }

        public bool GuardarReserva(ReservaModel model)
        {
            bool respuesta;
            try
            {
                var cn = new Conexion();
                using (var conexion = new SqlConnection(cn.getCadenaSql()))
                {
                    conexion.Open();

                    SqlCommand cmd = new SqlCommand("SP_InsertarReserva", conexion);
                    cmd.Parameters.AddWithValue("Fecha", model.Fecha);
                    cmd.Parameters.AddWithValue("HoraInicio", model.HoraInicio);
                    cmd.Parameters.AddWithValue("HoraFin", model.HoraFin);
                    cmd.Parameters.AddWithValue("IdProfesor", model.refProfesor.IdProfesor);
                    cmd.Parameters.AddWithValue("IdInstalacion", model.refInstalacion.IdInstalacion);

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

        public bool ActualizarReserva(ReservaModel model)
        {
            bool respuesta;
            try
            {
                var cn = new Conexion();
                using (var conexion = new SqlConnection(cn.getCadenaSql()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_ActualizarReserva", conexion);
                    cmd.Parameters.AddWithValue("IdReserva", model.IdReserva);
                    cmd.Parameters.AddWithValue("Fecha", model.Fecha);
                    cmd.Parameters.AddWithValue("HoraInicio", model.HoraInicio);
                    cmd.Parameters.AddWithValue("HoraFin", model.HoraFin);
                    cmd.Parameters.AddWithValue("IdProfesor", model.refProfesor.IdProfesor);
                    cmd.Parameters.AddWithValue("IdInstalacion", model.refInstalacion.IdInstalacion);

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

        public bool EliminarReserva(int IdReserva)
        {
            bool respuesta;
            try
            {
                var cn = new Conexion();
                using (var conexion = new SqlConnection(cn.getCadenaSql()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_EliminarReserva", conexion);
                    cmd.Parameters.AddWithValue("IdReserva", IdReserva);
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
                                Nombre = dr["Nombre"].ToString(),
                                Descripcion = dr["Descripcion"].ToString()
                            }
                        });
                    }
                }
            }

            return listaInstalaciones;
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

        public List<InstalacionModel> ObtenerListaDeInstalacionesPorEdificio(int idEdificio)
        {
            List<InstalacionModel> listaInstalaciones = new List<InstalacionModel>();
            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.getCadenaSql()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("SP_ListarInstalacionesPorEdificio", conexion);
                cmd.Parameters.AddWithValue("@IdEdificio", idEdificio);
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
                                Nombre = dr["EdificioNombre"].ToString(),
                                Descripcion = dr["EdificioDescripcion"].ToString()
                            }
                        });
                    }
                }
            }

            return listaInstalaciones;
        }

    }
}
