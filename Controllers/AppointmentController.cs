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
using System.Linq;
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
        private DataContext db = new DataContext();

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

            List<Labours> laboursList = new List<Labours>();
            laboursList = db.Labours.ToList();
            ViewBag.laboursList = laboursList;

            AppointmentModel appointmentModel = new AppointmentModel();
            if (id != null)
            {
                AppointmentService appointmentService = new AppointmentService();
                var result = await appointmentService.GetAppointment(token, id);
                AppointmentResult appointmentResult = JsonConvert.DeserializeObject<AppointmentResult>(result);

                appointmentModel.contactName = appointmentResult.contactName;
                appointmentModel.contactMail = appointmentResult.contactMail;
                appointmentModel.contactPhone = appointmentResult.contactPhone;
                appointmentModel.vehiclePlate = appointmentResult.vehiclePlate;
                appointmentModel.workshopId = appointmentResult.workshopId;
                appointmentModel.plannedData = appointmentResult.plannedData;

                ViewBag.IsReadOnly = 1;
                ViewBag.id = id;

                Appointment appointment = new Appointment();
                appointment = db.Appointment.Find(id);
                if (appointment != null)
                {
                    Labours labours = new Labours();
                    labours.id = appointment.laboursId;
                    labours.description = appointment.labours;

                    appointmentModel.brandId = appointment.brandId;
                    appointmentModel.versionId = appointment.versionId;
                    appointmentModel.vehicleYear = appointment.vehicleYear.ToString();
                    //appointmentModel.labours.id = appointment.laboursId;
                    appointmentModel.labours = labours;

                    _toastNotification.AddInfoToastMessage("Se cargaron los datos de la Cita Agendada con el Id. " + id);
                }
                else
                {
                    _toastNotification.AddAlertToastMessage("No de encontraron los datos con el Id. " + id);
                }

                Receptionist receptionist = new Receptionist();
                ReceptionistController receptionistController = new ReceptionistController();
                var receptionistListResult = await receptionistController.Index(appointmentModel.workshopId.ToString(), token);
                List<Receptionist> receptionistsList = JsonConvert.DeserializeObject<List<Receptionist>>(receptionistListResult);
                // JObject receptionisObject = JObject.Parse(await receptionistController.Index(appointmentModel.workshopId.ToString(), token));
                //JArray scheduleArray = (JArray)receptionisObject["days"];

                string scheduleId = receptionistsList[0].scheduleId.ToString(); 

                ScheduleController scheduleController = new ScheduleController();
                var scheadule = await scheduleController.Index(scheduleId, token);
                List<Days> scheduleList = JsonConvert.DeserializeObject<List<Days>>(scheadule);

                string[] fecha = appointment.plannedDate.Split("-");

                int dayOfWeek = Convert.ToInt32(new DateTime(Convert.ToInt32(fecha[0]), Convert.ToInt32(fecha[1]), Convert.ToInt32(fecha[2])).DayOfWeek);
                //int numDayOfWeek = (int)day.DayOfWeek;
                if(dayOfWeek == 0)
                {
                    dayOfWeek = 7;
                }

                Days days = new Days();
                for (int i = 0; i < scheduleList.Count; i++)
                {
                    if (dayOfWeek == Convert.ToInt32(scheduleList[i].weekday))
                    {
                        days = scheduleList[i];
                    }
                }

                var horarios = JsonConvert.DeserializeObject<List<Horarios>> (scheduleController.GetAllowTimes(days.beginning, days.ending, appointment.plannedDate, "1"));
                ViewBag.horarios = horarios;


                Token tokenVehicle = ObtenerTokenVehicle();
                VersionsService versionsService = new VersionsService();
                string resultVersion = await versionsService.GetVersions(tokenVehicle, appointmentModel.brandId);
                JObject results = JObject.Parse(resultVersion);
                JArray arrayResults = (JArray)results["versions"];

                ViewBag.versions = arrayResults;               
            }
            else
            {
                string result = "{'receptionists':[]}";
                JObject results = JObject.Parse(result);
                JArray arrayResults = (JArray)results["receptionists"];

                ViewBag.horarios = "";
                ViewBag.IsReadOnly = 0;
                ViewBag.id = "";
                ViewBag.versions = "";
                ViewBag.RecepcionistList = arrayResults;
            }

            //ViewData["allowTimes"] = "12:15";
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
                AppointmentResult resultado;

                Labours labours = new Labours();
                labours = db.Labours.Find(appointmentModel.labours.id);
                appointmentModel.labours = labours; 

                //Si es nuevo agendamiento.
                if (appointmentModel.id == null)
                {
                    appointmentModel.id = "";
                    appointmentModel.plannedData.plannedTime = appointmentModel.plannedData.plannedTime + ":00";

                    if(appointmentModel.vehiclePlate == null)
                    {
                        appointmentModel.vehiclePlate = appointmentModel.brandId + " " + appointmentModel.versionId + " " + appointmentModel.vehicleYear;
                    }
                    string resultCreate = await appointmentService.CreateAppointment(token, appointmentModel);
                    resultado = JsonConvert.DeserializeObject<AppointmentResult>(resultCreate);

                    if (resultado.id != null)
                    {
                        Appointment appointment = new Appointment();
                        appointment.id = resultado.id;
                        appointment.contactName = resultado.contactName;
                        appointment.contactMail = resultado.contactMail;
                        appointment.contactPhone = resultado.contactPhone;
                        appointment.brandId = appointmentModel.brandId;
                        appointment.versionId = appointmentModel.versionId;
                        appointment.version = appointmentModel.versionId;
                        appointment.vehicleYear = Convert.ToInt32(appointmentModel.vehicleYear);
                        appointment.vehiclePlate = resultado.vehiclePlate;
                        appointment.labours = appointmentModel.labours.description;
                        appointment.laboursId = appointmentModel.labours.id;
                        appointment.workshopId = appointmentModel.workshopId;
                        appointment.plannedDate = resultado.plannedData.plannedDate;
                        appointment.plannedTime = resultado.plannedData.plannedTime;

                        db.Appointment.Add(appointment);
                        db.SaveChanges();
                    }
                }
                //Si es un agendamiento que se va a modificar.
                else
                {
                    appointmentModel.plannedData.plannedTime = appointmentModel.plannedData.plannedTime + ":00";
                    resultado = JsonConvert.DeserializeObject<AppointmentResult>(await appointmentService.UpdateAppointment(token, appointmentModel));

                    Appointment appointment = new Appointment();
                    appointment.id = resultado.id;
                    appointment.contactName = resultado.contactName;
                    appointment.contactMail = resultado.contactMail;
                    appointment.contactPhone = resultado.contactPhone;
                    appointment.brandId = appointmentModel.brandId;
                    appointment.versionId = appointmentModel.versionId;
                    appointment.version = appointmentModel.versionId;
                    appointment.vehicleYear = Convert.ToInt32(appointmentModel.vehicleYear);
                    appointment.vehiclePlate = resultado.vehiclePlate;
                    appointment.labours = appointmentModel.labours.description;
                    appointment.laboursId = appointmentModel.labours.id;
                    appointment.workshopId = appointmentModel.workshopId;
                    appointment.plannedDate = resultado.plannedData.plannedDate;
                    appointment.plannedTime = resultado.plannedData.plannedTime;

                    db.Appointment.Update(appointment);
                    db.SaveChanges();
                }

                if (resultado.id != null)
                {
                    Workshop workshop = new Workshop();
                    WorkshopController workshopController = new WorkshopController();
                    workshop = JsonConvert.DeserializeObject<Workshop>(await workshopController.GetWorkShop(token, resultado.workshopId.ToString()));

                    Receptionist receptionist = new Receptionist();
                    ReceptionistController receptionistController = new ReceptionistController();
                    receptionist = await receptionistController.GetReceptionist(token, resultado.plannedData.receptionistId.ToString());

                    ViewData["cuerpoResultado"] = "Estimado " + appointmentModel.contactName + " Se ha generado una Cita con el Id. " + appointmentModel.id;
                    ViewData["cuerpoResultado1"] = "En la Agencia: " + workshop.comercialName;
                    ViewData["cuerpoResultado2"] = "Ubicada en: " + workshop.address + ", " + workshop.city;
                    ViewData["cuerpoResultado3"] = "El dia: " + appointmentModel.plannedData.plannedDate;
                    ViewData["cuerpoResultado4"] = "A las: " + appointmentModel.plannedData.plannedTime;
                    ViewData["cuerpoResultado5"] = "Datos del Vehículo: " + appointmentModel.vehiclePlate;
                   

                   
                    SendEmailService sendEmailService = new SendEmailService();
                    sendEmailService.SendEmail(resultado, workshop.comercialName, workshop.address, workshop.city, receptionist.name);

                    _toastNotification.AddSuccessToastMessage("Se enviaron los datos del Agendamiento de la Cita al correo " + resultado.contactMail);
                }
                else
                {
                    ViewData["cuerpoResultado"] = "Ha ocurrido un error. La cita no se ha generado.";
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
        [Route("/Appointment/Delete", Name = "DeleteAppointmentGetRoute")]
        public IActionResult Delete()
        {
            return View();
        }

        [HttpGet]
        [Route("/Appointment/Delete/{id}", Name = "DeleteAppointmentRoute")]
        public async Task<IActionResult> DeleteAppointment(string id)
        {
            if (id != null)
            {
                Token token = new Token();
                token = ObtenerToken();
                AppointmentService appointmentService = new AppointmentService();
                string resultado = await appointmentService.DeleteAppointment(token, id);

                _toastNotification.AddSuccessToastMessage("La cita" + id + " se ha cancelado");

                return RedirectToAction("Index", "Home");
            }
            else
            {
                _toastNotification.AddWarningToastMessage("El campo Id no es Opcional.");
                return RedirectToAction("DeleteAppointment", new RouteValueDictionary(
                   new { controller = "Index", action = "Appointment", id }));
            }
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
        public Token ObtenerTokenVehicle()
        {

            Token token = new Token();
            TokenService tokenService = new TokenService();

            if (Request.Cookies["tokenVehicle"] == null)
            {
                CookieOptions tokenCookie = new CookieOptions();
                tokenCookie.Expires = DateTime.Now.AddSeconds(600);
                token = tokenService.ObtenerTokenVechicleStock();
                Response.Cookies.Append("tokenVehicle", token.access_token, tokenCookie);
            }
            else
            {
                token.access_token = Request.Cookies["tokenVehicle"];
            }
            return token;
        }

        public bool ValidarAppointment(AppointmentModel appointmentModel)
        {
            if (appointmentModel.workshopId == -1)
            {
                return false;
            }
            if (appointmentModel.versionId == "-1")
            {
                return false;
            }
            if (appointmentModel.brandId == "-1")
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
