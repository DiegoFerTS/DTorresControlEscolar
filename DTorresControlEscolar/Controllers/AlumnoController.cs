using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace DTorresControlEscolar.Controllers
{
    public class AlumnoController : Controller
    {
        // GET: Alumno
        public ActionResult GetAll()
        {
            ML.Alumno alumno = new ML.Alumno();

            ServiceReferenceAlumno.ServiceAlumnoClient alumnoWCF = new ServiceReferenceAlumno.ServiceAlumnoClient();
            var resultadoAlumnoWCF = alumnoWCF.GetAll();

            if (resultadoAlumnoWCF.Correct)
            {
                alumno.Alumnos = resultadoAlumnoWCF.Objects.ToList();

                return View(alumno);
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult Form(int? idAlumno = 0)
        {
            ML.Alumno alumno = new ML.Alumno();

            if (idAlumno == 0 || idAlumno == null) // ADD
            {
                return View(alumno);
            }
            else // UPDATE
            {
                ServiceReferenceAlumno.ServiceAlumnoClient alumnoWCF = new ServiceReferenceAlumno.ServiceAlumnoClient();
                var resultadoAlumnoWCF = alumnoWCF.GetById(idAlumno.Value);
                if (resultadoAlumnoWCF.Correct)
                {
                    alumno = (ML.Alumno)resultadoAlumnoWCF.Object;
                    return View(alumno);
                }
                else
                {
                    return View(alumno);
                }
            }
        }

        [HttpPost]
        public ActionResult Form(ML.Alumno alumno)
        {
            if (alumno.IdAlumno == 0 || alumno.IdAlumno == null)
            {
                ServiceReferenceAlumno.ServiceAlumnoClient alumnoWCF = new ServiceReferenceAlumno.ServiceAlumnoClient();
                var resultadoAlumnoWCF = alumnoWCF.Add(alumno);
                if (resultadoAlumnoWCF.Correct)
                {
                    ViewBag.Mensaje = "El alumno " + alumno.Nombre + " ha sido registrado.";
                }
                else
                {
                    ViewBag.Mensaje = "El alumno " + alumno.Nombre + " no ha sido registrado.";

                }
            }
            else
            {
                ServiceReferenceAlumno.ServiceAlumnoClient alumnoWCF = new ServiceReferenceAlumno.ServiceAlumnoClient();
                var resultadoAlumnoWCF = alumnoWCF.Update(alumno);
                if (resultadoAlumnoWCF.Correct)
                {
                    ViewBag.Mensaje = "El alumno " + alumno.Nombre + " ha sido actualizado.";
                }
                else
                {
                    ViewBag.Mensaje = "El alumno " + alumno.Nombre + " no ha sido actualizado.";

                }
            }
            return PartialView("Modal");
        }

        [HttpGet]
        public ActionResult Delete(int idAlumno)
        {
            ServiceReferenceAlumno.ServiceAlumnoClient alumnoWCF = new ServiceReferenceAlumno.ServiceAlumnoClient();
            var resultado = alumnoWCF.Delete(idAlumno);


            if (resultado.Correct)
            {
                ViewBag.Mensaje = "Se ha eliminado al alumno con el id: " + idAlumno + " correctamente.";
            }
            else
            {
                ViewBag.Mensaje = "Error: " + resultado.ErrorMessage;
            }

            return PartialView("Modal");


        }
    }
}