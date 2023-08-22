using ApartadoAulas.Models;
using System.Data.SqlClient;
using System.Data;
using ApartadoAulas.Datos;
using System;
using System.Collections.Generic;

namespace ProyectoXDDD.Datos
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
                SqlCommand cmd = new SqlCommand("SP_ListarProfesores", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(new ProfesorModel
                        {
                            IdProfesor = Convert.ToInt32(dr["IdProfesor"]),
                            Nombre = dr["Nombre"].ToString(),
                            ApePa = dr["ApePa"].ToString(),
                            ApeMa = dr["ApeMa"].ToString(),
                            Email = dr["Email"].ToString(),
                            Contrasenia = dr["Contrasenia"].ToString(), // Agregar el campo de contraseña
                            refCarrera = new CarreraModel
                            {
                                IdCarrera = Convert.ToInt32(dr["IdCarrera"]),
                                Nombre = dr["NombreCarrera"].ToString()
                            }
                        });
                    }
                }
            }

            return lista;
        }


        public ProfesorModel ObtenerProfesor(int IdProfesor)
        {
            ProfesorModel profesor = new ProfesorModel();
            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.getCadenaSql()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("SP_ObtenerProfesor", conexion);
                cmd.Parameters.AddWithValue("IdProfesor", IdProfesor);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        profesor.IdProfesor = Convert.ToInt32(dr["IdProfesor"]);
                        profesor.Nombre = dr["Nombre"].ToString();
                        profesor.ApePa = dr["ApePa"].ToString();
                        profesor.ApeMa = dr["ApeMa"].ToString();
                        profesor.Email = dr["Email"].ToString();
                        profesor.Contrasenia = dr["Contrasenia"].ToString();

                        profesor.refCarrera = new CarreraModel
                        {
                            IdCarrera = Convert.ToInt32(dr["IdCarrera1"]), // Cambiar "IdCarrera1" por "IdCarrera"
                            Nombre = dr["NombreCarrera"].ToString()
                        };
                    }
                }
            }

            return profesor;
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
                    SqlCommand cmd = new SqlCommand("SP_ActualizarProfesor", conexion);
                    cmd.Parameters.AddWithValue("IdProfesor", model.IdProfesor);
                    cmd.Parameters.AddWithValue("Nombre", model.Nombre);
                    cmd.Parameters.AddWithValue("ApePa", model.ApePa);
                    cmd.Parameters.AddWithValue("ApeMa", model.ApeMa);
                    cmd.Parameters.AddWithValue("Email", model.Email);
                    cmd.Parameters.AddWithValue("Contrasenia", model.Contrasenia); // New parameter for password
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
                    SqlCommand cmd = new SqlCommand("SP_EliminarProfesor", conexion);
                    cmd.Parameters.AddWithValue("IdProfesor", IdProfesor);
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

        public List<CarreraModel> ObtenerListaDeCarreras()
        {
            List<CarreraModel> listaCarreras = new List<CarreraModel>();
            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.getCadenaSql()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("SP_ListarCarreras", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        listaCarreras.Add(new CarreraModel
                        {
                            IdCarrera = Convert.ToInt32(dr["IdCarrera"]),
                            Nombre = dr["Nombre"].ToString()
                           
                        });
                    }
                }
            }

            return listaCarreras;
        }
    }
}
