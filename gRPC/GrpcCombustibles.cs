using CitaActiva.Models;
using mx.autocom.servicio.paquetes.service;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static mx.autocom.servicio.paquetes.service.PaquetesGrpcService;

namespace CitaActiva.gRPC
{
    public class GrpcCombustibles
    {
        public async Task<string> ListarCombustibles()
        {
            PaquetesGrpc paquetesGrpc = new PaquetesGrpc();
            PaquetesGrpcServiceClient grpcService = paquetesGrpc.CargarPaquetesGrpc();
            Combustibles combustibles = await grpcService.GetAllCombustiblesAsync(new mx.autocom.servicio.paquetes.service.CombustibleRequest { });
            if (combustibles != null)
            {
                List<TipoCombustible> tipoCombustibleList = new List<TipoCombustible>();
                foreach (Combustible x in combustibles.Combustibles_)
                {
                    tipoCombustibleList.Add(new TipoCombustible
                    {
                        id_combustible = x.IdCombustible,
                        clave = x.Clave,
                        descripcion = x.Descripcion
                    });
                }
                return JsonConvert.SerializeObject(tipoCombustibleList);
            }
            else
            {
                return null;
            }

        }
    }
}
