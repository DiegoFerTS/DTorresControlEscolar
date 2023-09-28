using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DTorresControlEscolar.Controllers
{
    public class AlumnoMateriaController : Controller
    {
        // GET: AsignarMateria
        public ActionResult GetAll()
        {
            ML.Result resultado = BL.Alumno.GetAll();
            ML.Alumno alumno = new ML.Alumno();

            if (resultado.Correct)
            {
                alumno.Alumnos = resultado.Objects;
                return View(alumno);
            } else
            {
                return View(alumno);
            }
        }

        [HttpGet]
        public ActionResult GetMateriasAsignadas(int idAlumno, string nombreAlumno)
        {
            ML.Result resultado = BL.AlumnoMateria.GetMateriasAsignadas(idAlumno);
            ML.AlumnoMateria alumnoMateria = new ML.AlumnoMateria();
            alumnoMateria.Alumno = new ML.Alumno();
            alumnoMateria.Materia = new ML.Materia();
            alumnoMateria.AlumnoMaterias = new List<object>();


            if (resultado.Correct)
            {
                Session["IdAlumno"] = idAlumno;
                Session["NombreAlumno"] = nombreAlumno;
                alumnoMateria.AlumnoMaterias = resultado.Objects;
                alumnoMateria.Alumno = new ML.Alumno();
                alumnoMateria.Materia = new ML.Materia();
                return View(alumnoMateria);
            } else {
                return View(alumnoMateria);
            }

        }

        [HttpGet]
        public ActionResult GetMateriasNoAsignadas()
        {
            ML.Result resultado = BL.AlumnoMateria.GetMateriasNoAsignadas(int.Parse(Session["IdAlumno"].ToString()));
            ML.AlumnoMateria alumnoMateria = new ML.AlumnoMateria();
            alumnoMateria.Alumno = new ML.Alumno();
            alumnoMateria.Materia = new ML.Materia();
            alumnoMateria.AlumnoMaterias = new List<object>();

            if (resultado.Correct)
            {
                alumnoMateria.AlumnoMaterias = resultado.Objects;
                ViewBag.Vista_2 = true;

                return View(alumnoMateria);
            }
            else
            {
                return View(alumnoMateria);
            }

        }

        [HttpGet]
        public ActionResult DeleteMateriaAsignada(int idAlumno, int idMateria)
        {
            ML.Result resultado = BL.AlumnoMateria.DeleteMateriaAsignada(idAlumno, idMateria);
            ML.AlumnoMateria alumnoMateria = new ML.AlumnoMateria();

            if (resultado.Correct)
            {
                ViewBag.Mensaje = "Se ha eliminado la materia correctamente.";
            }
            else
            {
                ViewBag.Mensaje = "No se pudo eliminar la materia.";
            }

            ViewBag.Vista_2 = true;
            return PartialView("Modal");

        }


        [HttpPost]
        public ActionResult AddMaterias()
        {
            string[] materiasSeleccionadas = Request.Form.GetValues("materia");

            if (materiasSeleccionadas != null)
            {
                foreach (string materiaSeleccionada in materiasSeleccionadas)
                {
                    string[] splitMateriaSeleccionada = materiaSeleccionada.Split(',');

                    ML.Result resultado = BL.AlumnoMateria.AddMaterias(int.Parse(splitMateriaSeleccionada[0]), int.Parse(splitMateriaSeleccionada[1]));

                    if (resultado.Correct)
                    {
                        ViewBag.Mensaje = "Se registraron las materias correctamente.";
                        ViewBag.Vista_3 = false;
                    } else
                    {
                        ViewBag.Mensaje = "No se registraron las materias seleccionadas.";
                        ViewBag.Vista_3 = true;

                    }
                }
            }

            return PartialView("Modal");

        }
    }
}