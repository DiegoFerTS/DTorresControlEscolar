using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.ServiceModel;
using System.Web;
using System.Web.Mvc;

namespace DTorresControlEscolar.Controllers
{
    public class MateriaController : Controller
    {
        // GET: Materia
        public ActionResult GetAll()
        {
            ML.Materia materia = new ML.Materia();

            materia.Materias = new List<object>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["URLapi"].ToString());

                var responseTask = client.GetAsync("materia");
                responseTask.Wait();

                var resultServicio = responseTask.Result;

                if (resultServicio.IsSuccessStatusCode)
                {

                    var readTask = resultServicio.Content.ReadAsAsync<ML.Result>();
                    readTask.Wait();

                    foreach (var resultMateria in readTask.Result.Objects)
                    {
                        ML.Materia materiaRespuesta = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Materia>(resultMateria.ToString());
                        materia.Materias.Add(materiaRespuesta);
                    }
                }
            }
            return View(materia);
        }


        [HttpGet]
        public ActionResult Form(int? idMateria)
        {
            ML.Materia materia = new ML.Materia();


            if (idMateria != null)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(ConfigurationManager.AppSettings["URLApi"].ToString());

                    var responseTask = client.GetAsync("materia/" + idMateria);
                    responseTask.Wait();

                    var resultServicio = responseTask.Result;

                    if (resultServicio.IsSuccessStatusCode)
                    {

                        var readTask = resultServicio.Content.ReadAsAsync<ML.Result>();
                        readTask.Wait();


                        materia = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Materia>(readTask.Result.Object.ToString());

                    }
                }
            }
            return View(materia);
        }


        [HttpPost]
        public ActionResult Form(ML.Materia materia)
        {
            if (ModelState.IsValid)
            {


                if (materia.IdMateria == 0)
                {

                    using (var client = new HttpClient())  // ADD
                    {
                        client.BaseAddress = new Uri(ConfigurationManager.AppSettings["URLapi"].ToString());

                        //HTTP POST
                        var postTask = client.PostAsJsonAsync<ML.Materia>("materia", materia);
                        postTask.Wait();

                        var result = postTask.Result;

                        var readTask = result.Content.ReadAsAsync<ML.Result>();
                        readTask.Wait();

                        if (result.IsSuccessStatusCode)
                        {
                            ViewBag.Mensaje = "Se ha registrado la materia: " + materia.Nombre + " correctamente.";
                        }
                        else
                        {
                            ViewBag.Mensaje = "Error: " + readTask.Result.ErrorMessage;
                        }
                    }

                }
                else
                {

                    using (var client = new HttpClient())  // Update
                    {
                        client.BaseAddress = new Uri(ConfigurationManager.AppSettings["URLapi"].ToString());


                        //HTTP POST
                        var putTask = client.PutAsJsonAsync<ML.Materia>("materia/" + materia.IdMateria, materia);
                        putTask.Wait();

                        var result = putTask.Result;

                        var readTask = result.Content.ReadAsAsync<ML.Result>();
                        readTask.Wait();

                        if (result.IsSuccessStatusCode)
                        {
                            ViewBag.Mensaje = "Se ha actualizado la materia: " + materia.Nombre + " correctamente.";
                        }
                        else
                        {
                            ViewBag.Mensaje = "Error: " + readTask.Result.ErrorMessage;
                        }
                    }
                }
                return PartialView("Modal");

            } else
            {
                return View(materia);
            }

        }

        [HttpGet]
        public ActionResult Delete(int idMateria)
        {

            using (var client = new HttpClient())  // Delete
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["URLapi"].ToString());


                //HTTP delete
                var putTask = client.DeleteAsync("materia/" + idMateria);
                putTask.Wait();

                var result = putTask.Result;

                var readTask = result.Content.ReadAsAsync<ML.Result>();
                readTask.Wait();
                if (result.IsSuccessStatusCode)
                {
                    ViewBag.Mensaje = "Se ha eliminado la materia con el id: " + idMateria + " correctamente.";
                }
                else
                {
                    ViewBag.Mensaje = "Error: " + readTask.Result.ErrorMessage;
                }
            }

            return PartialView("Modal");
        }


    }
}