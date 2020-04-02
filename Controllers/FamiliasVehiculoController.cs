using System;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using CitaActiva.gRPC;
using System.Threading.Tasks;
using System.Collections.Generic;
using CitaActiva.Models;

namespace CitaActiva.Controllers
{
    public class FamiliasVehiculoController : Controller
    {
        [HttpGet]
        [Route("/Citas/FamiiasVehiculo/{codigoQis}", Name = "MarcasVehiculoRoute")]
        public async Task<string> GetFamiliasVehiculoAsync(string codigoQis)
        {
            try
            {
                //using (DataContext db = new DataContext())
                //{
                //    var list = db.FamiliasVehiculos.Where(fv => fv.id_marcavehiculo == id_marcavehiculo).OrderBy(fv => fv.id_familiavehiculo);
                //    string familiasVehiculo = JsonConvert.SerializeObject(list.ToArray());
                //    return familiasVehiculo;
                //}
                GrpcFamilias grpcFamilias = new GrpcFamilias();
                //var lista = await grpcFamilias.ListarFamilias(codigoQis);
                //return JsonConvert.SerializeObject(lista.ToArray());
                return await grpcFamilias.ListarFamilias(codigoQis);

            } catch(Exception ex)
            {
                return null;
            }
        }
    }
}