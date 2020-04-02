using CitaActiva.Models;
using mx.autocom.servicio.paquetes.service;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using static mx.autocom.servicio.paquetes.service.PaquetesGrpcService;

namespace CitaActiva.gRPC
{
    public class GrpcKitsServicios
    {
        public async Task<string> ListarKitsServicios(int idKitServicio)
        {
            PaquetesGrpc paquetesGrpc = new PaquetesGrpc();
            PaquetesGrpcServiceClient grpcService = paquetesGrpc.CargarPaquetesGrpc();
            KitServicios kitServicios = await grpcService.GetAllKitsServiciosAsync(new mx.autocom.servicio.paquetes.service.KitServicioRequest { IdKitServicio = idKitServicio });
            if(kitServicios != null)
            {
                List<Models.KitServicio> kitServicioList = new List<Models.KitServicio>();
                foreach(mx.autocom.servicio.paquetes.service.KitServicio x in kitServicios.KitServicios_)
                {
                    List<Models.Mo> ManoObraList = new List<Models.Mo>();
                    foreach (KitServicioManoObra ksmo in x.KitServicioManoObra)
                    {
                        ManoObraList.Add(new Models.Mo
                        {
                            id_mano_obra = ksmo.IdManoObra,
                            codigo_qis = ksmo.CodigoQis,
                            descripcion = ksmo.Descripcion
                        });                        
                    }
                    List<Models.Articulos> ArticuloList = new List<Models.Articulos>();
                    foreach(KitServicioArticulo ksa in x.KitServicioRefaccion)
                    {
                        ArticuloList.Add(new Models.Articulos
                        {
                            id_articulo = ksa.IdArticulo,
                            codigo_qis = ksa.CodigoQis,
                            descripcion = ksa.Descripcion
                        });
                    }
                    kitServicioList.Add(new Models.KitServicio
                    {
                        id_kit = x.IdKit,
                        id_kit_servicio = x.IdKitServicio,
                        descripcion = x.Descripcion,
                        Mo = ManoObraList,
                        Articulos = ArticuloList
                    });
                }               

                return JsonConvert.SerializeObject(kitServicioList);
            }
            else
            {
                return null;
            }

        }
    }
}
