using mx.autocom.servicio.paquetes.service;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static mx.autocom.servicio.paquetes.service.PaquetesGrpcService;

namespace CitaActiva.gRPC
{
    public class GrpcServicioEspecifico
    {
        public async Task<string> ListarServicioEspecifico()
        {
            PaquetesGrpc paquetesGrpc = new PaquetesGrpc();
            PaquetesGrpcServiceClient grpcService = paquetesGrpc.CargarPaquetesGrpc();
            ServicioEspecificoResponse especifico = await grpcService.GetAllServiciosEspecificosAsync(new mx.autocom.servicio.paquetes.service.ServicioEspecificoRequest { OrderBy = "" });
            if (especifico != null)
            {
                List<Models.ServicioEspecifico> especificoList = new List<Models.ServicioEspecifico>();
                foreach (ServicioEspecifico x in especifico.ServicioEspecifico)
                {

                    especificoList.Add(new Models.ServicioEspecifico
                    {
                        id_especifico =(int)x.IdServicioEspecifico,
                        codigo_qis = x.CodigoQis,
                        descripcion = x.Descripcion,
                        costo = (decimal) x.Costo
                    });
              
                }
                return JsonConvert.SerializeObject(especificoList);
            }
            else
            {
                return null;
            }
        }
    }
}
