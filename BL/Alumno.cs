using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Alumno
    {
        public static ML.Result Add(ML.Alumno alumno)
        {
            ML.Result resultado = new ML.Result();

            try
            {
                using (SqlConnection conexion = new SqlConnection(DL.Conexion.GetConnectionString()))
                {
                    string query = "AlumnoAdd";

                    SqlCommand command = new SqlCommand(query, conexion);
                    command.CommandType = CommandType.StoredProcedure;

                    SqlParameter[] collection = new SqlParameter[3];
                    collection[0] = new SqlParameter("@Nombre", System.Data.SqlDbType.VarChar);
                    collection[0].Value = alumno.Nombre;
                    collection[1] = new SqlParameter("@ApellidoPaterno", System.Data.SqlDbType.VarChar);
                    collection[1].Value = alumno.ApellidoPaterno;
                    collection[2] = new SqlParameter("@ApellidoMaterno", System.Data.SqlDbType.VarChar);
                    collection[2].Value = alumno.ApellidoMaterno;

                    command.Parameters.AddRange(collection);
                    command.Connection.Open();

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        resultado.Correct = true;
                    } else
                    {
                        resultado.Correct = false;
                        resultado.ErrorMessage = "No se pudo registrar al alumno: " + alumno.Nombre;
                    }
                }
            } catch (Exception ex)
            {
                resultado.Correct = false;
                resultado.ErrorMessage = ex.InnerException.Message;
                resultado.Ex = ex;
            }

            return resultado;
        }

        public static ML.Result Update(ML.Alumno alumno)
        {
            ML.Result resultado = new ML.Result();

            try
            {
                using (SqlConnection conexion = new SqlConnection(DL.Conexion.GetConnectionString()))
                {
                    string query = "AlumnoUpdate";

                    SqlCommand command = new SqlCommand(query, conexion);
                    command.CommandType = CommandType.StoredProcedure;

                    SqlParameter[] collection = new SqlParameter[4];
                    collection[0] = new SqlParameter("@Id", System.Data.SqlDbType.Int);
                    collection[0].Value = alumno.IdAlumno;
                    collection[1] = new SqlParameter("@Nombre", System.Data.SqlDbType.VarChar);
                    collection[1].Value = alumno.Nombre;
                    collection[2] = new SqlParameter("@ApellidoPaterno", System.Data.SqlDbType.VarChar);
                    collection[2].Value = alumno.ApellidoPaterno;
                    collection[3] = new SqlParameter("@ApellidoMaterno", System.Data.SqlDbType.VarChar);
                    collection[3].Value = alumno.ApellidoMaterno;

                    command.Parameters.AddRange(collection);
                    command.Connection.Open();

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        resultado.Correct = true;
                    }
                    else
                    {
                        resultado.Correct = false;
                        resultado.ErrorMessage = "No se pudo actualizar al alumno: " + alumno.Nombre;
                    }
                }
            }
            catch (Exception ex)
            {
                resultado.Correct = false;
                resultado.ErrorMessage = ex.InnerException.Message;
                resultado.Ex = ex;
            }

            return resultado;
        }

        public static ML.Result Delete(int idAlumno)
        {
            ML.Result resultado = new ML.Result();

            try
            {
                using (SqlConnection conexion = new SqlConnection(DL.Conexion.GetConnectionString()))
                {
                    string query = "AlumnoDelete";

                    SqlCommand command = new SqlCommand(query, conexion);
                    command.CommandType = CommandType.StoredProcedure;

                    SqlParameter[] collection = new SqlParameter[1];
                    collection[0] = new SqlParameter("@Id", System.Data.SqlDbType.Int);
                    collection[0].Value = idAlumno;

                    command.Parameters.AddRange(collection);
                    command.Connection.Open();

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        resultado.Correct = true;
                    }
                    else
                    {
                        resultado.Correct = false;
                        resultado.ErrorMessage = "No se pudo eliminar al alumno con id: " + idAlumno;
                    }
                }
            }
            catch (Exception ex)
            {
                resultado.Correct = false;
                resultado.ErrorMessage = ex.InnerException.Message;
                resultado.Ex = ex;
            }

            return resultado;
        }

        public static ML.Result GetAll()
        {
            ML.Result resultado = new ML.Result();

            try
            {
                using (SqlConnection conexion = new SqlConnection(DL.Conexion.GetConnectionString()))
                {
                    string query = "AlumnoGetAll";

                    using(SqlCommand command = new SqlCommand(query, conexion))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        SqlDataAdapter adapter = new SqlDataAdapter(command);

                        DataTable tablaAlumno = new DataTable();

                        adapter.Fill(tablaAlumno);

                        if (tablaAlumno.Rows.Count > 0)
                        {
                            resultado.Objects = new List<object>();

                            foreach (DataRow row in tablaAlumno.Rows)
                            {
                                ML.Alumno alumno = new ML.Alumno();

                                alumno.IdAlumno = int.Parse(row[0].ToString());
                                alumno.Nombre = row[1].ToString();
                                alumno.ApellidoPaterno = row[2].ToString();
                                alumno.ApellidoMaterno = row[3].ToString();

                                resultado.Objects.Add(alumno);
                            }
                            resultado.Correct = true;
                        } else
                        {
                            resultado.Correct = false;
                            resultado.ErrorMessage = "No se encontraron datos de alumnos.";
                        }
                    }

                    
                }
            } catch (Exception ex)
            {
                resultado.Correct = false;
                resultado.ErrorMessage = ex.InnerException.Message;
                resultado.Ex = ex;
            }

            return resultado;
        }

        public static ML.Result GetById(int idAlumno)
        {
            ML.Result resultado = new ML.Result();

            try
            {
                using (SqlConnection conexion = new SqlConnection(DL.Conexion.GetConnectionString()))
                {
                    string query = "AlumnoGetById";

                    using (SqlCommand command = new SqlCommand(query, conexion))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        SqlParameter[] collection = new SqlParameter[1];
                        collection[0] = new SqlParameter("@Id", System.Data.SqlDbType.Int);
                        collection[0].Value = idAlumno;
                        command.Parameters.AddRange(collection);


                        SqlDataAdapter adapter = new SqlDataAdapter(command);

                        DataTable tablaAlumno = new DataTable();

                        adapter.Fill(tablaAlumno);

                        if (tablaAlumno.Rows.Count > 0)
                        {
                            resultado.Objects = new List<object>();
                                                      
                            foreach (DataRow row in tablaAlumno.Rows)
                            {
                                ML.Alumno alumno = new ML.Alumno();

                                alumno.IdAlumno = int.Parse(row[0].ToString());
                                alumno.Nombre = row[1].ToString();
                                alumno.ApellidoPaterno = row[2].ToString();
                                alumno.ApellidoMaterno = row[3].ToString();

                                resultado.Object = alumno;
                            }
                            resultado.Correct = true;
                        }
                        else
                        {
                            resultado.Correct = false;
                            resultado.ErrorMessage = "No se encontraron datos del alumno.";
                        }
                    }


                }
            }
            catch (Exception ex)
            {
                resultado.Correct = false;
                resultado.ErrorMessage = ex.InnerException.Message;
                resultado.Ex = ex;
            }

            return resultado;
        }
    }
}
