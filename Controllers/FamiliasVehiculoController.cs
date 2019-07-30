using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CitaActiva.ModelsViews;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CitaActiva.Controllers
{
    public class FamiliasVehiculoController : Controller
    {
        [HttpGet]
        [Route("/Citas/FamiiasVehiculo/{id_marcavehiculo}", Name = "MarcasVehiculoRoute")]
        public string GetFemiliasVehiculo(int id_marcavehiculo)
        {
            try
            {
                using (DataContext db = new DataContext())
                {
                    var list = db.FamiliasVehiculos.Where(fv => fv.id_marcavehiculo == id_marcavehiculo).OrderBy(fv => fv.id_familiavehiculo);
                    string familiasVehiculo = JsonConvert.SerializeObject(list.ToArray());

                    return familiasVehiculo;
                }
            } catch(Exception ex)
            {
                return null;
            }
        }
    }
}