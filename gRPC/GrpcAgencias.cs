using mx.autocom.servicio.paquetes.service;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static mx.autocom.servicio.paquetes.service.PaquetesGrpcService;

namespace CitaActiva.gRPC
{
    public class GrpcAgencias
    {
        public async Task<string> ListarAgencias(int id)
        {
            PaquetesGrpc paquetesGrpc = new PaquetesGrpc();
            PaquetesGrpcServiceClient grpcService = paquetesGrpc.CargarPaquetesGrpc();
            Agencias agencias = await grpcService.GetAllAgenciasAsync(new mx.autocom.servicio.paquetes.service.AgenciaRequest { IdAgencia = id });
            if (agencias != null)
            {
                List<Models.Agencias> agenciasList = new List<Models.Agencias>();
                foreach (Agencia x in agencias.Agencias_)
                {

                    agenciasList.Add(new Models.Agencias
                    {
                        id_agencia = x.IdAgencia,
                        codigo_quis = x.CodigoQis,
                        nombre_agencia = x.NombreAgencia,
                        activa = x.Activa,
                        google_place = x.GooglePlace,
                        place_id = x.PlaceId
                    });
                }
                return JsonConvert.SerializeObject(agenciasList);
            }
            else
            {
                return null;
            }
        }

        public async Task<string> ListarAgenciasByIdZona(int id_zona)
        {
            PaquetesGrpc paquetesGrpc = new PaquetesGrpc();
            PaquetesGrpcServiceClient grpcService = paquetesGrpc.CargarPaquetesGrpc();
            Agencias agencias = await grpcService.GetAllAgenciasAsync(new mx.autocom.servicio.paquetes.service.AgenciaRequest { IdZona = id_zona });
            if (agencias != null)
            {
                List<Models.Agencias> agenciasList = new List<Models.Agencias>();
                foreach (Agencia x in agencias.Agencias_)
                {

                    agenciasList.Add(new Models.Agencias
                    {
                        id_agencia = x.IdAgencia,
                        codigo_quis = x.CodigoQis,
                        nombre_agencia = x.NombreAgencia,
                        activa = x.Activa,
                        google_place = x.GooglePlace,
                        place_id = x.PlaceId
                    });
                }
                return JsonConvert.SerializeObject(agenciasList);
            }
            else
            {
                return null;
            }
        }
    }
}
