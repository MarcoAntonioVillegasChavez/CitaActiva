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
    public class GrpcMarcas
    {
        public async Task<string> ListarMarcas()
        {
            PaquetesGrpc paquetesGrpc = new PaquetesGrpc();
            PaquetesGrpcServiceClient grpcService = paquetesGrpc.CargarPaquetesGrpc();
            Marcas marcas = await grpcService.GetAllMarcasAsync(new mx.autocom.servicio.paquetes.service.MarcaRequest { });
            if (marcas != null)
            {
                List<MarcasVehiculo> marcasVehiculosList = new List<MarcasVehiculo>();
                foreach (Marca x in marcas.Marcas_)
                {
                    marcasVehiculosList.Add(new MarcasVehiculo
                    {
                        id_marca = x.IdMarca,
                        nombre_marca = x.NombreMarca,
                        codigo_qis = x.CodigoQis
                    });
                }
                return JsonConvert.SerializeObject(marcasVehiculosList);
            }
            else
            {
                return null;
            }
        }
    }
}
