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

namespace CitaActiva.Controllers
{
    public class HomeController : Controller
    {
        private readonly IToastNotification _toastNotification;
        public HomeController(IToastNotification toastNotification)
        {
            _toastNotification = toastNotification;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("/Home/Redirect", Name = "HomeGetRoute")]
        public IActionResult Index([FromForm] AppointmentModel appointmentModel)
        {
            if (appointmentModel.id == null)
            {
                //Warning
                _toastNotification.AddWarningToastMessage("Por favor agregue el Id de su Cita Agendada con anterioridad.");
                return View();
            }
            else
            {
                return RedirectToAction("Index", new RouteValueDictionary(
                    new { controller = "Appointment", action = "Index", appointmentModel.id }));
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
    }
}
