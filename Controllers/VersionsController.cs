using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CitaActiva.Models;
using CitaActiva.ModelsViews;
using CitaActiva.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CitaActiva.Controllers
{
    public class VersionsController : Controller
    {
        DataContext db = new DataContext();
        [HttpGet]
        [Route("/Appointment/Versions/{brandId}", Name = "VersionsRoute")]
        public string Versions(string brandId)
        {
           

            var list = db.Versions.Where(v => v.brandId == brandId).OrderBy(v => v.description);
            string versions =JsonConvert.SerializeObject(list.ToArray());
            return versions;
        }

        JArray QuitarCamposSobrantes(JArray arrayResults)
        {
            string[] strArray = new string[14];
            strArray[0] = "totalPrice";
            strArray[1] = "shippingCost";
            strArray[2] = "fuel";
            strArray[3] = "engineCapacity";
            strArray[4] = "priceWOTaxes";
            strArray[5] = "seatingCapacity";
            strArray[6] = "doors";
            strArray[7] = "shippingPrice";
            strArray[8] = "modelCode";
            strArray[9] = "showInList";
            strArray[10] = "brandId";
            strArray[11] = "equipmentPrice";
            strArray[12] = "vehicleType";
            strArray[13] = "distributorPrice";

            try
            {

            for (int i = 0; i< strArray.Length; i++)
            {
                arrayResults.Descendants().OfType<JProperty>()
                   .Where(p => p.Name == strArray[i])
                   .ToList()
                   .ForEach(att => att.Remove());
            }

            var newJson = arrayResults.ToString();

            return arrayResults;
            }catch(Exception ex)
            {
                string str = ex.Message.ToString();
                return null;
            }
        }
    }
}