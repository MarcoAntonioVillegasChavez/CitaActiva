using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CitaActiva.Models;
using CitaActiva.ModelsViews;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CitaActiva.Controllers
{
    public class AgenciasController : Controller
    {
        public string GetAgencias(Token token, string postalCode, int zona)
        {
            using (DataContext db = new DataContext())
            {
                var list = db.Agencias.Where(a => a.active_ind == 1).OrderBy(a => a.id_agencia);
                return JsonConvert.SerializeObject(list.ToArray());
            }
        }


        [HttpGet]
        [Route("/Agencias/GetAgenciasByPlaceId/{place_id}", Name = "GetAgenciasByPlaceIdRoute")]
        public string GetAgenciasByPlaceId(string place_id)
        {
            using (DataContext db = new DataContext())
            {                
                var list = from a in db.Agencias
                           where a.place_id == place_id
                           select new
                           {
                               a.id_agencia,
                               a.nombre_agencia
                           };
               // var agenciaList = list.ToList();

                return JsonConvert.SerializeObject(list.ToArray());  //agenciaList[0].id_agencia.ToString();
            }           
        }
    }
}