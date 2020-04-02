using System;

using System.Threading.Tasks;
using CitaActiva.gRPC;
using Microsoft.AspNetCore.Mvc;

namespace CitaActiva.Controllers
{
    public class ServicioAdicionalController : Controller
    {
        [HttpGet]
        [Route("/ServicioAdicional/GetServicioAdicional", Name = "SARoute")]
        public async Task<string> GetServicioAdicional()
        {
            GrpcServiciosAdicionales grpcServiciosAdicionales = new GrpcServiciosAdicionales();
            return await grpcServiciosAdicionales.ListarServicioAdicional();
        }
    }
}