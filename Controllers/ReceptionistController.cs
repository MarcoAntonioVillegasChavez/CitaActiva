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
        [Route("/Appointment/Receptionist/{workshopId}", Name = "ReceptionistRoute")]
        public async Task<string> Index(string workshopId, Token token)
        {
            if (token.access_token == null)
            {
             token = ObtenerToken();
            }
            ReceptionistService receptionistService = new ReceptionistService();
            string result = await receptionistService.GetReceptionistByWorkShop(token, workshopId);
            JObject results = JObject.Parse(result);
            JArray arrayResults = (JArray)results["receptionists"];

            //ViewBag.scheduleId = arrayResults[0]["scheduleId"];
            return JsonConvert.SerializeObject(arrayResults);
            
        }

        [HttpGet("{id}")]
        [Route("/Appointment/ReceptionistById/{receptionistId}", Name = "ReceptionistIdRoute")]
        public async Task<Receptionist> GetReceptionist(Token token, string receptionistId)
        {
            if (token.access_token == null)
            {
                token = ObtenerToken();
            }

            ReceptionistService receptionistService = new ReceptionistService();
            string result = await receptionistService.GetReceptionistById(token, receptionistId);
            Receptionist receptionist = JsonConvert.DeserializeObject<Receptionist>(result);
            return receptionist;

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
    }
}