using System;
using mx.autocom.servicio.paquetes.service;
using Newtonsoft.Json;
using System.Threading.Tasks;
using static mx.autocom.servicio.paquetes.service.PaquetesGrpcService;
using System.Collections.Generic;

namespace CitaActiva.gRPC
{
    public class GrpcServicios
    {
        public async Task<string> ListarServicios(string familia, string combustible )
        {
            PaquetesGrpc paquetesGrpc = new PaquetesGrpc();
            PaquetesGrpcServiceClient grpcService = paquetesGrpc.CargarPaquetesGrpc();
            Servicios servicios = await grpcService.GetAllServiciosAsync(new mx.autocom.servicio.paquetes.service.ServicioRequest {
                NombreFamilia = familia,                
                Combustible = combustible,
                OrderBy = " kilometraje_maximo asc"
            });
            if (servicios != null)
            {
                List<Models.Servicios> serviciosList = new List<Models.Servicios>();
                foreach (Servicio x in servicios.Servicios_)
                {
                    serviciosList.Add(new Models.Servicios
                    {
                        id_servicio = x.IdServicio,
                        kilometraje_maximo = x.KilometrajeMaximo,
                        anho = x.Anho,
                        id_familia = x.IdFamilia,
                        precio = x.Precio,
                        id_combustible = x.IdCombustible,
                        codigo_qis = x.CodigoQis
                    });
                }
                return JsonConvert.SerializeObject(serviciosList);
            }
            else
            {
                return null;
            }
        }
    }
}
