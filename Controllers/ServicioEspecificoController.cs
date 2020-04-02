using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CitaActiva.gRPC;
using Microsoft.AspNetCore.Mvc;

namespace CitaActiva.Controllers
{
    public class ServicioEspecificoController : Controller
    {
        [HttpGet]
        [Route("/ServicioEspecifico/GetServicioEspecifico", Name = "SERoute")]
        public async Task<string> GetServicioEspecifico()
        {
            GrpcServicioEspecifico grpcServicioEspecifico = new GrpcServicioEspecifico();
            return await grpcServicioEspecifico.ListarServicioEspecifico();
        }
    }
}