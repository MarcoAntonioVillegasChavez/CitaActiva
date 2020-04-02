using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CitaActiva.gRPC;
using CitaActiva.Models;
using CitaActiva.ModelsViews;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CitaActiva.Controllers
{
    public class ServiciosController : Controller
    {
        [HttpGet]
        [Route("/Servicios/GetServicios/", Name = "ServiciosRoute")]
        public string GetServicios()
        {
            try
            {
                List<Servicios> serviciosList = new List<Servicios>();
                serviciosList.Add(new Servicios
                {
                    id_servicio = 0,
                    kilometraje_maximo = 5000,
                });
                for (int i = 1; i <= 20; i++) {
                    serviciosList.Add(new Servicios
                    {
                        id_servicio =i,
                        kilometraje_maximo = i * 10000,
                    });
                }
                return JsonConvert.SerializeObject(serviciosList);
            }
            catch(Exception ex)
            {
                return null;
            }
               
        }
    }
}