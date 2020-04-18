using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CitaActiva.gRPC;
using CitaActiva.Models;
using CitaActiva.ModelsViews;
using CitaActiva.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CitaActiva.Controllers
{
    public class ScheduleController : Controller
    {
        Ambiente _ambiente;
        public ScheduleController(IOptions<Ambiente> environment)
        {
            _ambiente = environment.Value;
        }

        const string SessionKeyName = "token";
       
        [HttpGet("{id}")]
        [Route("/Appointment/Schedule/{scheduleId}", Name = "ScheduleRoute")]
        public async Task<string> Index(string scheduleId)
        {
            GrpcTokenQis grpc = new GrpcTokenQis();
            Token token = await grpc.GetTokenQis(_ambiente.environment);

            ScheduleService scheduleService = new ScheduleService();
            var scheduleResult = await scheduleService.GetScheduleById(token, scheduleId);

            JObject scheduleObject = JObject.Parse(scheduleResult);
            JArray scheduleArray = (JArray)scheduleObject["days"];
            ViewBag.scheduleList = scheduleArray;
            

            return JsonConvert.SerializeObject(scheduleArray);
        }
        [HttpGet]
        [Route("/Appointment/AllowTimes/{idAgencia}/{hrMin}/{hrMax}/{fecha}/{editInd}", Name = "AllowTimesRoute")]
        public string GetAllowTimes(int idAgencia, string hrMin, string hrMax, string fecha, string editInd)
        {
            List<Horarios> horariosList = new List<Horarios>();

            string[] hrMinSplit = hrMin.Split(':');
            string[] hrMaxSplit = hrMax.Split(':');

            if(hrMin == "null" || hrMax == "null")
            {
                return null;
            }

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
                    for (int x = 0; x <= 2; x++)
                    {
                        if (x == 0)
                        {
                            minutos = "00";
                        }
                        if (x == 1)
                        {
                            minutos = "20";
                        }
                        if (x == 2)
                        {
                            minutos = "40";
                        }
                        Horarios horaAgregada = new Horarios();
                        //horaAgregada.hora = TimeSpan.Parse(horas + ":" + minutos + ":00");
                        horaAgregada.hora = horas + ":" + minutos + ":00";//TimeSpan.Parse(horas + ":" + minutos);
                        horaAgregada.horario = horas + ":" + minutos;//TimeSpan.Parse( horas + ":" + minutos);

                        horariosList.Add(horaAgregada);
                    }
                }

                //var list = db.AgendamientoCita.Where(ac => ac.planned_date == DateTime.Parse(fecha) 
                //&& ac.id_agencia == idAgencia)
                //.Select(ac => ac.planned_time);



               //using (var db = new DataContext())
                
                using (var db = new PaquetesContext())
                {
                    //var list = from agc in db.AgendamientoCita
                    //           join ac in db.AgenciaCitas on agc.id_cita equals ac.id_cita
                    //           where agc.planned_date == DateTime.Parse(fecha) && ac.id_agencia == idAgencia
                    //           select (agc.planned_time);

                    var list = from c in db.cita
                               where c.fecha == DateTime.Parse(fecha) && c.id_agencia == (idAgencia)
                               select (c.hora);

                    var horariosOcupados = list.ToList();
                    //for de validacion

                    if (horariosOcupados.Count != 0)
                    {
                        for (int x = 0; x < horariosOcupados.Count; x++)
                        {
                            //Console.WriteLine("HorariosOcupados: " + horariosOcupados[x].ToString());
                            for (int i = 0; i < horariosList.Count; i++)
                            {
                                if (horariosOcupados[x].ToString() == horariosList[i].hora)
                                {
                                    horariosList.Remove(horariosList[i]);
                                }
                                
                            }
                        }

                    }

                    string allowTimes = JsonConvert.SerializeObject(horariosList.ToArray());
                    return allowTimes;
                }
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