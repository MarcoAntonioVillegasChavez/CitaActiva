using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CitaActiva.Models;
using CitaActiva.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CitaActiva.Controllers
{
    public class ReceptionistController : Controller
    {
        const string SessionKeyName = "token";
        [HttpGet("{id}")]
        [Route("/Appointment/Receptionist/{agenciaId}", Name = "ReceptionistRoute")]
        public async Task<string> Index(string agenciaId, Token token)
        {
            if (token.access_token == null)
            {
                TokenController tokenController = new TokenController();
                token = tokenController.ObtenerToken();
            }
            ReceptionistService receptionistService = new ReceptionistService();
            string result = await receptionistService.GetReceptionistByWorkShop(token, agenciaId);
            JObject results = JObject.Parse(result);
            JArray arrayResults = (JArray)results["receptionists"];

            //ViewBag.scheduleId = arrayResults[0]["scheduleId"];
            return JsonConvert.SerializeObject(arrayResults);
            
        }
    }
}