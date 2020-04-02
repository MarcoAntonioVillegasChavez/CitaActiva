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
    public class AgenciasController : Controller
    {
        public string GetAgencias(Token token, string postalCode, int zona)
        {
            return null;
        }


        [HttpGet]
        [Route("/Agencias/GetAgenciasByPlaceId/{place_id}", Name = "GetAgenciasByPlaceIdRoute")]
        public async Task<string> GetAgenciasByPlaceId(int place_id)
        {
            try
            {
                GrpcAgencias grpcAgencias = new GrpcAgencias();
                return await grpcAgencias.ListarAgencias(place_id); 
            }catch(Exception ex)
            {
                return null;
            }
        }
        [HttpGet]
        [Route("/Agencias/GetAgenciasByIdZona/{id_zona}", Name = "GetAgenciasById")]
        public async Task<string> GetAgenciasByIdZona(int id_zona)
        {
            try
            {
                GrpcAgencias grpcAgencias = new GrpcAgencias();
                return await grpcAgencias.ListarAgenciasByIdZona(id_zona);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}