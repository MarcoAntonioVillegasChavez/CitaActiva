using mx.autocom.servicio.paquetes.service;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static mx.autocom.servicio.paquetes.service.PaquetesGrpcService;

namespace CitaActiva.gRPC
{
    public class GrpcServiciosAdicionales
    {
        public async Task<string> ListarServicioAdicional()
        {
            PaquetesGrpc paquetesGrpc = new PaquetesGrpc();
            PaquetesGrpcServiceClient grpcService = paquetesGrpc.CargarPaquetesGrpc();
            Adicionales adicional = await grpcService.GetAllAdicionalesAsync(new mx.autocom.servicio.paquetes.service.AdicionalRequest { });
            if (adicional != null)
            {
                List<Models.ServicioAdicional> adicionalList = new List<Models.ServicioAdicional>();
                foreach (Adicional x in adicional.Adicionales_)
                {
                    adicionalList.Add(new Models.ServicioAdicional
                    {
                        id_adicional = x.IdAdicional,
                        codigo_qis = x.CodigoQis,
                        descripcion = x.Descripcion,
                        costo = (decimal)x.Costo,
                        id_servicio_adicional =x.TiposAdicionales.IdTipoAdicional,
                        servicio_adicional_desc = x.TiposAdicionales.Descripcion
                    });

                }
                return JsonConvert.SerializeObject(adicionalList);
            }
            else
            {
                return null;
            }
        }
    }
}
