using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class AlumnoMateria
    {

        public static ML.Result AddMaterias(int idAlumno, int idMateria)
        {
            ML.Result resultado = new ML.Result();

            try
            {
                using (DL.DTorresControlEscolarEntities conexion = new DL.DTorresControlEscolarEntities())
                {
                    var query = conexion.AlumnoMateriaAdd(idAlumno, idMateria);

                    if (query > 0)
                    {
                        resultado.Correct = true;
                    } else
                    {
                        resultado.Correct = false;
                        resultado.ErrorMessage = "No se realizo el registro";
                    }
                }
            } catch (Exception ex)
            {
                resultado.Correct = false;
                resultado.ErrorMessage = ex.Message;
                resultado.Ex = ex;
            }

            return resultado;
        }


        public static ML.Result GetMateriasAsignadas(int idAlumno)
        {
            ML.Result resultado = new ML.Result();

            try
            {
                using (DL.DTorresControlEscolarEntities conexion = new DL.DTorresControlEscolarEntities())
                {
                    var query = conexion.AlumnoMateriaGetMateriasAsignadas(idAlumno).ToList();

                    if (query.Count > 0)
                    {
                        resultado.Objects = new List<object>();

                        foreach (var objectResult in query)
                        {
                            ML.AlumnoMateria alumnoMateria = new ML.AlumnoMateria();
                            alumnoMateria.Alumno = new ML.Alumno();
                            alumnoMateria.Materia = new ML.Materia();

                            alumnoMateria.Alumno.IdAlumno = objectResult.IdAlumno;
                            alumnoMateria.Materia.IdMateria = objectResult.IdMateria;
                            alumnoMateria.Materia.Nombre = objectResult.Nombre;
                            alumnoMateria.Materia.Costo = objectResult.Costo.Value;

                            resultado.Objects.Add(alumnoMateria);
                        }
                        resultado.Correct = true;
                    } else
                    {
                        resultado.Correct = false;
                        resultado.ErrorMessage = "No se encontraron materias del alumno con id: " + idAlumno;
                    }
                }
            }
            catch (Exception ex)
            {
                resultado.Correct = true;
                resultado.ErrorMessage = ex.Message;
                resultado.Ex = ex;
            }

            return resultado;
        }

        public static ML.Result GetMateriasNoAsignadas(int idAlumno)
        {
            ML.Result resultado = new ML.Result();

            try
            {
                using (DL.DTorresControlEscolarEntities conexion = new DL.DTorresControlEscolarEntities())
                {
                    var query = conexion.AlumnoMateriaGetMateriasNoAsigadas(idAlumno).ToList();

                    if (query.Count > 0)
                    {
                        resultado.Objects = new List<object>();

                        foreach (var objectResult in query)
                        {
                            ML.AlumnoMateria alumnoMateria = new ML.AlumnoMateria();
                            alumnoMateria.Materia = new ML.Materia();
                            alumnoMateria.Alumno = new ML.Alumno();

                            alumnoMateria.Materia.IdMateria = objectResult.Id;
                            alumnoMateria.Materia.Nombre = objectResult.Nombre;
                            alumnoMateria.Materia.Costo = objectResult.Costo.Value;

                            resultado.Objects.Add(alumnoMateria);
                        }
                        resultado.Correct = true;
                    }
                    else
                    {
                        resultado.Correct = false;
                        resultado.ErrorMessage = "No se encontraron materias sin asignar.";
                    }
                }
            }
            catch (Exception ex)
            {
                resultado.Correct = true;
                resultado.ErrorMessage = ex.InnerException.Message;
                resultado.Ex = ex;
            }

            return resultado;
        }


        public static ML.Result DeleteMateriaAsignada(int idAlumno, int idMateria)
        {
            ML.Result resultado = new ML.Result();

            try
            {
                using (DL.DTorresControlEscolarEntities conexion = new DL.DTorresControlEscolarEntities())
                {
                    var query = conexion.AlumnoMateriaDeleteMateriaAsignada(idAlumno, idMateria);

                    if (query > 0)
                    {
                        resultado.Correct = true;
                    } else
                    {
                        resultado.Correct = false;
                        resultado.ErrorMessage = "No se pudo eliminar la materia asignada";
                    }
                }
            }
            catch (Exception ex)
            {
                resultado.Correct = false;
                resultado.ErrorMessage = ex.Message;
                resultado.Ex = ex;
            }

            return resultado;
        }
    }
}
