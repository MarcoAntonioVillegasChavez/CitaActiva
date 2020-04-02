using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CitaActiva.gRPC;
using Microsoft.AspNetCore.Mvc;

namespace CitaActiva.Controllers
{
    public class KitsController : Controller
    {
        [HttpGet]
        [Route("/Kits/GetKits", Name = "KitsRoute")]
        public async Task<string> GetKits()
        {
            GrpcKits grpcKits = new GrpcKits();
            return await grpcKits.ListarKits();
        }

        [HttpGet]
        [Route("/Kits/GetKitServicios/{idKitServicio}", Name = "KitServiciosRoute")]
        public async Task<string> GetKitServicioByid(int idKitServicio)
        {
            GrpcKitsServicios grpcKitsServicios = new GrpcKitsServicios();
            return await grpcKitsServicios.ListarKitsServicios(idKitServicio);
        }

        [HttpGet]
        [Route("/Kits/GetKitConcepto/{idKitConcepto}", Name = "KitConceptossRoute")]
        public async Task<string> GetKitConceptosByid(int idKitConcepto)
        {
            GrpcKitsActividades grpcKitsActividades = new GrpcKitsActividades();
            return await grpcKitsActividades.ListarKitsActividades(idKitConcepto);
        }

    }

}