using CitaActiva.Models;
using CitaActiva.ModelsViews;
using CitaActiva.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NToastNotify;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CitaActiva.Controllers
{
    //[Route("api/[controller]")]
    [Route("api/Appointment")]
    [ApiController]
    public class AppointmentController : Controller
    {
        const string SessionKeyName = "token";
        private readonly IToastNotification _toastNotification;

        public AppointmentController(IToastNotification toastNotification)
        {
            _toastNotification = toastNotification;
        }

        // GET: api/Appointment
        //[HttpGet("{appointmentModel}")]
        [HttpGet]
        [Route("/Appointment/Index/", Name = "AppintmentGetRoute")]
        public async Task<IActionResult> Index(string id)//(AppointmentModel appointmentModel)
        {
            Token token = new Token();
            token = ObtenerToken();

            WorkshopController workshopController = new WorkshopController();
            var workShopResult = await workshopController.Index(token, "");
            JObject workshopObject = JObject.Parse(workShopResult);
            JArray workshopArray = (JArray)workshopObject["workshops"];

            ViewBag.WorkshopList = workshopArray;


            AppointmentModel appointmentModel = new AppointmentModel();
            if (id != null)
            {
                AppointmentService appointmentService = new AppointmentService();
                var result = await appointmentService.GetAppointment(token, id);
                appointmentModel = JsonConvert.DeserializeObject<AppointmentModel>(result);

                ReceptionistService receptionistService = new ReceptionistService();
                string resultReceptionist = await receptionistService.GetReceptionistByWorkShop(token, appointmentModel.workshopId.ToString());
                JObject results = JObject.Parse(resultReceptionist);
                JArray arrayResults = (JArray)results["receptionists"];
                ViewBag.RecepcionistList = arrayResults;

                _toastNotification.AddInfoToastMessage("Se cargaron los datos de la Cita Agendada con el Id. " + id);
            }
            else
            {
                string result = "{'receptionists':[]}";
                JObject results = JObject.Parse(result);
                JArray arrayResults = (JArray)results["receptionists"];

                ViewBag.RecepcionistList = arrayResults;
            }


            return View(appointmentModel);
        }

        public IActionResult CreateAppointment()
        {
            return View();
        }

        // POST: api/Appointment
        [HttpPost]
        [Route("/Appointment/CreateAppointment", Name = "AppintmentRoute")]
        public async Task<IActionResult> CreateAppointment([FromForm] AppointmentModel appointmentModel)
        {

            Token token = new Token();
            token = ObtenerToken();            

            if (!ValidarAppointment(appointmentModel))
            {
                _toastNotification.AddWarningToastMessage("Por favor Llene todos los campos. Nunguno es opcional.");
                return RedirectToAction("Index", new RouteValueDictionary(
                   new { controller = "Appointment", action = "Index", appointmentModel.id }));
            }
            else
            {
                AppointmentService appointmentService = new AppointmentService();
                AppointmentModel resultado;
                if (appointmentModel.id == null)
                {
                    appointmentModel.id = "";
                    appointmentModel.plannedData.plannedTime = appointmentModel.plannedData.plannedTime + ":00";

                    resultado = JsonConvert.DeserializeObject<AppointmentModel>(await appointmentService.CreateAppointment(token, appointmentModel));
                }
                else
                {
                    appointmentModel.plannedData.plannedTime = appointmentModel.plannedData.plannedTime + ":00";
                    resultado = JsonConvert.DeserializeObject<AppointmentModel>(await appointmentService.UpdateAppointment(token, appointmentModel));
                }

                if (resultado.id != null)
                {
                    ViewData["IdAppointment"] = "La cita ha sido registrada con el Id: " + resultado.id;
                    SendEmailService sendEmailService = new SendEmailService();

                    Workshop workshop = new Workshop();
                    WorkshopController workshopController = new WorkshopController();
                    workshop = JsonConvert.DeserializeObject<Workshop>(await workshopController.GetWorkShop(token, resultado.workshopId.ToString()));

                    Receptionist receptionist = new Receptionist();
                    ReceptionistController receptionistController = new ReceptionistController();
                    receptionist = await receptionistController.GetReceptionist(token, resultado.plannedData.receptionistId.ToString());
                    sendEmailService.SendEmail(resultado, workshop.comercialName, workshop.address, workshop.city, receptionist.name);

                    _toastNotification.AddSuccessToastMessage("Se enviaron los datos del Agendamiento de la Cita al correo " + resultado.contactMail);
                }
                else
                {
                    ViewData["IdAppointment"] = "Ha ocurrido un error. La cita no se ha generado.";
                    _toastNotification.AddErrorToastMessage("Ha ocurrido un error. Favor de contactar al Administrador.");
                }


                return View();
            }
        }
        // PUT: api/Appointment/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpGet]
        public IActionResult Delete()
        {
            return View();
        }

        [HttpPost]
        [Route("/Appointment/Delete", Name = "DeleteAppointmentRoute")]
        public async Task<IActionResult> DeleteAppointment([FromForm] AppointmentModel appointmentModel)
        {
            Token token = new Token();
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(SessionKeyName)))
            {
                TokenService tokenServices = new TokenService();
                token = tokenServices.ObtenerToken();
                HttpContext.Session.SetString(SessionKeyName, JsonConvert.SerializeObject(token));
            }
            token = JsonConvert.DeserializeObject<Token>(HttpContext.Session.GetString(SessionKeyName));

            AppointmentService appointmentService = new AppointmentService();
            string resultado = await appointmentService.DeleteAppointment(token, appointmentModel.id);

            return View();
        }
        public Token ObtenerToken()
        {

            Token token = new Token();
            TokenService tokenService = new TokenService();

            if (Request.Cookies["token"] == null)
            {
                CookieOptions tokenCookie = new CookieOptions();
                tokenCookie.Expires = DateTime.Now.AddSeconds(600);
                token = tokenService.ObtenerToken();
                Response.Cookies.Append("token", token.access_token, tokenCookie);
            }
            else
            {
                token.access_token = Request.Cookies["token"];
            }
            return token;
        }
    
        public bool ValidarAppointment(AppointmentModel appointmentModel)
        {
            if (appointmentModel.workshopId == -1)
            {
                return false;
            }
            else if (appointmentModel.plannedData.receptionistId == -1)
            {
                return false;
            }
            else if (appointmentModel.plannedData.plannedDate == null)
            {
                return false;
            }
            else if (appointmentModel.plannedData.plannedTime == null)
            {
                return false;
            }
            else if (appointmentModel.vehiclePlate == null)
            {
                return false;
            }
            else if (appointmentModel.contactName == null)
            {
                return false;
            }
            else if (appointmentModel.contactMail == null)
            {
                return false;
            }
            else if (appointmentModel.contactPhone == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

    }
}
