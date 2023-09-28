using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Materia
    {
        public static ML.Result Add(ML.Materia materia)
        {
            ML.Result resultado = new ML.Result();

            try
            {
                using (DL.DTorresControlEscolarEntities conexion = new DL.DTorresControlEscolarEntities())
                {
                    var query = conexion.MateriaAdd(materia.Nombre, materia.Costo);

                    if (query > 0)
                    {
                        resultado.Correct = true;
                    } else
                    {
                        resultado.Correct = false;
                        resultado.ErrorMessage = "No se pudo registrar la materia: " + materia.Nombre;
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

        public static ML.Result Update(ML.Materia materia)
        {
            ML.Result resultado = new ML.Result();

            try
            {
                using (DL.DTorresControlEscolarEntities conexion = new DL.DTorresControlEscolarEntities())
                {
                    var query = conexion.MateriaUpdate(materia.IdMateria, materia.Nombre, materia.Costo);

                    if (query > 0)
                    {
                        resultado.Correct = true;
                    }
                    else
                    {
                        resultado.Correct = false;
                        resultado.ErrorMessage = "No se pudo actualizar la materia: " + materia.Nombre;
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

        public static ML.Result Delete(int idMateria)
        {
            ML.Result resultado = new ML.Result();

            try
            {
                using (DL.DTorresControlEscolarEntities conexion = new DL.DTorresControlEscolarEntities())
                {
                    var query = conexion.MateriaDelete(idMateria);

                    if (query > 0)
                    {
                        resultado.Correct = true;
                    }
                    else
                    {
                        resultado.Correct = false;
                        resultado.ErrorMessage = "No se pudo eliminar la materia con el id: " + idMateria;
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
                using (DL.DTorresControlEscolarEntities conexion = new DL.DTorresControlEscolarEntities())
                {
                    var query = conexion.MateriaGetAll().ToList();

                    if (query.Count > 0)
                    {
                        resultado.Objects = new List<object>();

                        foreach (var materiaResult in query)
                        {
                            ML.Materia materia = new ML.Materia();

                            materia.IdMateria = materiaResult.Id;
                            materia.Nombre = materiaResult.Nombre;
                            materia.Costo = materiaResult.Costo.Value;

                            resultado.Objects.Add(materia);
                        }

                        resultado.Correct = true;
                    }
                    else
                    {
                        resultado.Correct = false;
                        resultado.ErrorMessage = "No se encontraron datos.";
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

        public static ML.Result GetById(int idMateria)
        {
            ML.Result resultado = new ML.Result();

            try
            {
                using (DL.DTorresControlEscolarEntities conexion = new DL.DTorresControlEscolarEntities())
                {
                    var query = conexion.MateriaGetById(idMateria).FirstOrDefault();

                    if (query != null)
                    {                                               
                        ML.Materia materia = new ML.Materia();

                        materia.IdMateria = query.Id;
                        materia.Nombre = query.Nombre;
                        materia.Costo = query.Costo.Value;

                        resultado.Object = materia;                       

                        resultado.Correct = true;
                    }
                    else
                    {
                        resultado.Correct = false;
                        resultado.ErrorMessage = "No se encontro la materia.";
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
