using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CitaActiva.Models;
using CitaActiva.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace CitaActiva.Controllers
{
    public class VersionsController : Controller
    {
        [HttpGet]
        [Route("/Appointment/Versions/{brandId}", Name = "VersionsRoute")]
        public async Task<IActionResult> Versions(string brandId)
        {
            Token token = ObtenerToken();
            VersionsService versionsService = new VersionsService();
            string result = await versionsService.GetVersions(token, brandId);
            JObject results = JObject.Parse(result);
            JArray arrayResults = (JArray)results["versions"];
            

            return Json(QuitarCamposSobrantes(arrayResults));
        }

        public Token ObtenerToken()
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