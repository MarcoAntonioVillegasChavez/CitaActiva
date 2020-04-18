using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CitaActiva.gRPC;
using CitaActiva.Models;
using CitaActiva.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CitaActiva.Controllers
{
    public class ReceptionistController : Controller
    {
        Ambiente _environment;
        public ReceptionistController(IOptions<Ambiente> environment)
        {
            _environment = environment.Value;
        }
        const string SessionKeyName = "token";
        [HttpGet("{id}")]
        [Route("/Appointment/Receptionist/{agenciaId}", Name = "ReceptionistRoute")]
        public async Task<string> Index(string agenciaId)
        {
            GrpcTokenQis grpc = new GrpcTokenQis();
            Token token = await grpc.GetTokenQis(_environment.environment);
            
            ReceptionistService receptionistService = new ReceptionistService();
            string result = await receptionistService.GetReceptionistByWorkShop(token, agenciaId);
            JObject results = JObject.Parse(result);
            JArray arrayResults = (JArray)results["receptionists"];

            //ViewBag.scheduleId = arrayResults[0]["scheduleId"];
            return JsonConvert.SerializeObject(arrayResults);
            
        }
    }
}