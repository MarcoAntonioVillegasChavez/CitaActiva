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
    public class ScheduleController : Controller
    {
        const string SessionKeyName = "token";

        private DataContext db = new DataContext();

        [HttpGet("{id}")]
        [Route("/Appointment/Schedule/{scheduleId}", Name = "ScheduleRoute")]
        public async Task<string> Index(string scheduleId, Token token)
        {
            //Token token = new Token();
            if (token.access_token == null)
            {
                TokenController tokenController = new TokenController();
                token = tokenController.ObtenerToken();
            }

            ScheduleService scheduleService = new ScheduleService();
            var scheduleResult = await scheduleService.GetScheduleById(token, scheduleId);

            JObject scheduleObject = JObject.Parse(scheduleResult);
            JArray scheduleArray = (JArray)scheduleObject["days"];
            ViewBag.scheduleList = scheduleArray;
            

            return JsonConvert.SerializeObject(scheduleArray);
        }
        [HttpGet]
        [Route("/Appointment/AllowTimes/{workshopId}/{hrMin}/{hrMax}/{fecha}/{editInd}", Name = "AllowTimesRoute")]
        public string GetAllowTimes(int workshopId, string hrMin, string hrMax, string fecha, string editInd)
        {

                List<Horarios> horariosList = new List<Horarios>();

                string[] hrMinSplit = hrMin.Split(':');
                string[] hrMaxSplit = hrMax.Split(':');

                int hrMinima = Convert.ToInt32(hrMinSplit[0]);
                int hrMaxima = Convert.ToInt32(hrMaxSplit[0]);

                if (hrMinima < hrMaxima)
                {

                    //for de las horas
                    for (int i = hrMinima; i <= hrMaxima - 1; i++)
                    {
                        string horas = "";
                        string minutos = "";

                        if (i < 10)
                        {
                            horas = "0" + i.ToString();
                        }
                        else
                        {
                            horas = i.ToString();
                        }

                        //horas = i.ToString();

                        //for de los minutos
                        for (int x = 0; x <= 3; x++)
                        {
                            if (x == 0)
                            {
                                minutos = "00";
                            }
                            if (x == 1)
                            {
                                minutos = "15";
                            }
                            if (x == 2)
                            {
                                minutos = "30";
                            }
                            if (x == 3)
                            {
                                minutos = "45";
                            }

                            Horarios horaAgregada = new Horarios();
                            horaAgregada.hora = horas + ":" + minutos + ":00";
                            horaAgregada.horario = horas + ":" + minutos;

                            horariosList.Add(horaAgregada);
                        }
                    }


                    var list = db.Appointment.Where(pt => pt.plannedDate == fecha && pt.workshopId == workshopId && pt.deletedInd != 1).Select(pt => pt.plannedTime);
                    var horariosOcupados = list.ToList();
                    //for de validacion

                    if (horariosOcupados.Count != 0)
                    {
                        for (int i = 0; i < horariosList.Count; i++)
                        {
                            for (int x = 0; x < horariosOcupados.Count; x++)
                            {
                                if (horariosList[i].hora == horariosOcupados[x])
                                {
                                    if (editInd != "1")
                                    {
                                        horariosList.Remove(horariosList[i]);
                                    }
                                }
                            }
                        }
                    }

                    string allowTimes = JsonConvert.SerializeObject(horariosList.ToArray());
                    return allowTimes;
                }
                else
                {
                    return null;
                }
        }

        /*
        [HttpGet]
        [Route("/Appointment/Schedule/", Name = "GetScheduleRoute")]
        public async Task<IActionResult> GetSchedule(string scheduleId)
        {
            scheduleId = "AS18";
            Token token = new Token();
            token = ObtenerToken();

            ScheduleService scheduleService = new ScheduleService();
            var scheduleResult = await scheduleService.GetScheduleById(token, scheduleId);

            JObject scheduleObject = JObject.Parse(scheduleResult);
            JArray scheduleArray = (JArray)scheduleObject["days"];
            ViewBag.scheduleList = scheduleArray;


            return Json(scheduleArray);
        }
        */
        /*
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
        }*/
    }
    public class Horarios
    {
        public string hora { get; set; }
        public string horario { get; set; }
    }
}