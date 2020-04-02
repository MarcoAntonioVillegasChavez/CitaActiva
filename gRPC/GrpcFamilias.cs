using mx.autocom.servicio.paquetes.service;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Threading.Tasks;
using static mx.autocom.servicio.paquetes.service.PaquetesGrpcService;

namespace CitaActiva.gRPC
{
    public class GrpcFamilias
    {
        public object JSonConvert { get; private set; }

        public async Task<string> ListarFamilias(string codigoQis)
        {
            PaquetesGrpc paquetesGrpc = new PaquetesGrpc();
            PaquetesGrpcServiceClient grpcService = paquetesGrpc.CargarPaquetesGrpc();
            Familias familias = await grpcService.GetAllFamiliasAsync(new mx.autocom.servicio.paquetes.service.FamiliaRequest { NombreMarca = codigoQis });
            if (familias != null)
            {
                List<Models.FamiliasVehiculo> familiasList = new List<Models.FamiliasVehiculo>();
                foreach (Familia x in familias.Familias_)
                {
                    familiasList.Add(new Models.FamiliasVehiculo
                    {
                        id_familia = x.IdFamilia,
                        nombre_familia = x.NombreFamilia,
                        id_marca = x.Marca.IdMarca,
                        codigo_qis = x.CodigoQis
                    });
                    //Console.WriteLine("============= FAMILIAS ============");
                    //Console.WriteLine($"Id: {x.IdFamilia}");
                    //Console.WriteLine($"Nombre: {x.NombreFamilia}");
                    //Console.WriteLine($"Id Zona: {x.Marca.IdMarca}");
                    //Console.WriteLine($"Nombre Zona: {x.Marca.NombreMarca}");
                    //Console.WriteLine($"Código QIS: {x.CodigoQis ?? string.Empty}");
                    //Console.WriteLine("=========================");
                }
                return JsonConvert.SerializeObject(familiasList.ToArray());
            }
            else
            {
                return null;
            }
        }
    }
}
