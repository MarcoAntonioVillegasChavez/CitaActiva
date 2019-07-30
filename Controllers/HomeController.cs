using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CitaActiva.Models;
using NToastNotify;
using CitaActiva.ModelsViews;
using Microsoft.AspNetCore.Routing;
using CitaActiva.Services;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace CitaActiva.Controllers
{
    public class HomeController : Controller
    {
        private readonly IToastNotification _toastNotification;
        public HomeController(IToastNotification toastNotification)
        {
            _toastNotification = toastNotification;
        }
        [HttpGet]
        public IActionResult Index()
        {
            if (Request.Cookies["cliente"] != null)
            {
                Clientes clientes = new Clientes();
                CustomerService customerService = new CustomerService();
                clientes = customerService.DatosCliente(Request.Cookies["cliente"]);
            }
            //else
            //{
            //    return RedirectToAction("Index", new RouteValueDictionary(
            //        new { controller = "Clientes", action = "Index" }));
            //}

            return View();
        }

        [HttpPost]
        [Route("/Home/Index", Name = "HomeGetRoute")]
        public async Task< IActionResult> Index([FromForm] AppointmentModel appointmentModel)
        {
            if (appointmentModel.id == null)
            {
                //Warning
                _toastNotification.AddWarningToastMessage("Por favor agregue el Id de su Cita Agendada con anterioridad.");
                return View();
            }
            else
            {
                Token token = new Token();
                TokenController tokenController = new TokenController();

                token = tokenController.ObtenerToken();

               try
                {
                    AppointmentService appointmentService = new AppointmentService();
                    var result = await appointmentService.GetAppointment(token, appointmentModel.id);
                    AppointmentResult appointmentResult = JsonConvert.DeserializeObject<AppointmentResult>(result);
                    
                    if(appointmentResult.id != null)
                    {
                        return RedirectToAction("Index", new RouteValueDictionary(
                            new { controller = "Appointment", action = "Index", appointmentModel.id }));
                    }
                    else
                    {
                        _toastNotification.AddErrorToastMessage("La cita que desea consultar, se ha cancelado.");
                        return View();
                    }
                }catch(Exception ex)
                {
                    _toastNotification.AddErrorToastMessage("La cita que desea consultar, se ha cancelado.");
                    return View();
                }
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public IActionResult CerrarSesion()
        {
            Response.Cookies.Delete("cliente");
            Response.Cookies.Delete("nombreCliente");
            

            return RedirectToAction("Index", new RouteValueDictionary(
                   new { controller = "Home", action = "Index" }));
        }
    }
}
