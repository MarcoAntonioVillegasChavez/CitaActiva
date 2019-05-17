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
    public class ScheduleController : Controller
    {
        const string SessionKeyName = "token";
        [HttpGet("{id}")]
        [Route("/Appointment/Schedule/{scheduleId}", Name = "ScheduleRoute")]
        public async Task<IActionResult> Index(string scheduleId)
        {
            Token token = new Token();
            token = ObtenerToken();

            ScheduleService scheduleService = new ScheduleService();
            var scheduleResult = await scheduleService.GetScheduleById(token, scheduleId);

            JObject scheduleObject = JObject.Parse(scheduleResult);
            JArray scheduleArray = (JArray)scheduleObject["days"];
            ViewBag.scheduleList = scheduleArray;
            

            return Json(scheduleArray);
        }
        public Token ObtenerToken()
        {
            Token token = new Token();
            TokenService tokenService = new TokenService();

            if (Request.Cookies["token"] == null)
            {
                CookieOptions tokenCookie = new CookieOptions();
                tokenCookie.Expires = DateTime.Now.AddSeconds(600);
                //tokenCookie.IsEssential = true;
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