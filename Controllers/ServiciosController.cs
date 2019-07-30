using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CitaActiva.ModelsViews;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CitaActiva.Controllers
{
    public class ServiciosController : Controller
    {
        [HttpGet]
        [Route("/Servicios/GetServicios/{anho}/{id_familiasvehiculo}/{id_tipocombustible}", Name = "ServiciosRoute")]
        public string GetServicios(int anho, int id_familiasvehiculo, int id_tipocombustible)
        {
            try
            {

            using (DataContext db = new DataContext())
            {
                var list = db.Servicios.Where(s => s.anho == anho
                                                && s.id_familiavehiculo == id_familiasvehiculo 
                                                && s.id_tipocombustible == id_tipocombustible)
                                                .OrderBy(s => s.id_servicio);

                return JsonConvert.SerializeObject(list.ToArray());
            }
            }catch(Exception ex)
            {
                return null;
            }
               
        }
    }
}