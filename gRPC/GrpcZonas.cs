using mx.autocom.servicio.paquetes.service;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static mx.autocom.servicio.paquetes.service.PaquetesGrpcService;

namespace CitaActiva.gRPC
{
    public class GrpcZonas
    {
        public async Task<string> ListarZonas()
        {
            PaquetesGrpc paquetesGrpc = new PaquetesGrpc();
            PaquetesGrpcServiceClient grpcService = paquetesGrpc.CargarPaquetesGrpc();
            Zonas zona = await grpcService.GetAllZonasAsync(new mx.autocom.servicio.paquetes.service.ZonaRequest { OrderBy = "1"});
            if (zona != null)
            {
                List<Models.Zonas> zonaList = new List<Models.Zonas>();
                foreach (Zona x in zona.Zonas_)
                {

                    zonaList.Add(new Models.Zonas
                    {
                        id_zona = x.IdZona,
                        nombre_zona = x.NombreZona
                    });
                }
                return JsonConvert.SerializeObject(zonaList);
            }
            else
            {
                return null;
            }
        }
    }
}
