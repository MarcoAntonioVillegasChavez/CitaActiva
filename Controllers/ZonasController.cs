using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CitaActiva.gRPC;
using CitaActiva.Models;
using CitaActiva.ModelsViews;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CitaActiva.Controllers
{
    public class ZonasController : Controller
    {
        [HttpGet]
        [Route("/Agencias/GetAllZonas/", Name = "GetAllZonas")]
        public async Task<string> GetZonas()
        {
            try
            {
                GrpcZonas grpcZonas = new GrpcZonas();
                return await grpcZonas.ListarZonas();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}