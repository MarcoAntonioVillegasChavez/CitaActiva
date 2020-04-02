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
    public class GrpcKitsActividades
    {
        public async Task<string> ListarKitsActividades(int idKitConcepto)
        {
            PaquetesGrpc paquetesGrpc = new PaquetesGrpc();
            PaquetesGrpcServiceClient grpcService = paquetesGrpc.CargarPaquetesGrpc();
            KitConceptos kitConceptos = await grpcService.GetAllKitsConceptosAsync (new mx.autocom.servicio.paquetes.service.KitConceptoRequest { IdKitConcepto=idKitConcepto  });
            if (kitConceptos != null)
            {
                List<Models.KitConcepto> kitConceptosList = new List<Models.KitConcepto>();
                foreach (mx.autocom.servicio.paquetes.service.KitConcepto x in kitConceptos.KitConceptos_)
                {
                    List<Models.Actividades> actividadesList = new List<Models.Actividades>();
                    foreach (mx.autocom.servicio.paquetes.service.KitConceptoActividad kca in x.Actividad)
                    {
                        actividadesList.Add(new Models.Actividades
                        {
                            id_actividad = kca.IdActividad,
                            actividades = kca.Actividad,
                            activo = kca.Activo
                        });
                    }

                    kitConceptosList.Add(new Models.KitConcepto {
                        id_kit_concepto = x.IdKitConcepto,
                        id_kit = x.IdKit,
                        id_concepto = x.IdConcepto,
                        actividades = actividadesList
                    });
                }

                return JsonConvert.SerializeObject( kitConceptosList);
            }
            else
            {
                return null;
            }
        }
    }
}
