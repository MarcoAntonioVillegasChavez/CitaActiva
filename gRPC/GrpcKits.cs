using mx.autocom.servicio.paquetes.service;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static mx.autocom.servicio.paquetes.service.PaquetesGrpcService;

namespace CitaActiva.gRPC
{
    public class GrpcKits
    {
        public async Task<string> ListarKits()
        {
            PaquetesGrpc paquetesGrpc = new PaquetesGrpc();
            PaquetesGrpcServiceClient grpcService = paquetesGrpc.CargarPaquetesGrpc();
            Kits kits = await grpcService.GetAllKitsAsync(new mx.autocom.servicio.paquetes.service.KitRequest { IdZona = "1", OrderBy = "id_kit" }); 
            if (kits != null)
            {
                List<Models.Kits> kitsList = new List<Models.Kits>();
                foreach (Kit x in kits.Kits_)
                {
                    List<Models.Articulos> articulosList = new List<Models.Articulos>();
                    foreach (ArticuloKit ak in x.Articulos)
                    {
                        articulosList.Add(new Models.Articulos {
                            id_articulo = ak.IdArticulo,
                            descripcion = ak.Descripcion,
                            precio = ak.Precio                              
                        });
                    }
                    List<Models.Mo> moList = new List<Models.Mo>();
                    foreach (KitManoObra mo in x.ManoObra)
                    {
                        moList.Add(new Models.Mo {
                            id_mano_obra = mo.IdManoObra,
                            descripcion = mo.Descripcion,
                            precio = mo.Precio,
                            descuento = mo.Descuento                            
                        });
                    }
                    List<Models.KitServicio> kitServiciosList = new List<Models.KitServicio>();
                    foreach(KitServicio ks in x.Servicios)
                    {
                        kitServiciosList.Add(new Models.KitServicio {
                            id_kit_servicio = ks.IdKitServicio,
                            id_kit = ks.IdKit,
                            descripcion = ks.Descripcion,
                            urlImg1 = ks.UriImagen1,
                            urlImg2 = ks.UriImagen2
                        });
                    }

                    List<Models.Concepto> conceptoList = new List<Models.Concepto>();
                    foreach (KitConcepto c in x.Concepto)
                    {
                        conceptoList.Add(new Models.Concepto
                        {
                            id_concepto = c.IdConcepto,
                            concepto = c.Descripcion,
                            tipo = c.Tipo,
                            id_kit_concepto = c.IdKitConcepto
                        });
                    }

                    kitsList.Add(new Models.Kits {
                        id_kit = x.IdKit,
                        codigo_qis = x.CodigoQis,
                        descripcion = x.Descripcion,
                        color = x.Color,
                        uri_foto = x.UriFoto,
                        articulos = articulosList,
                        mos = moList,
                        kitServicios = kitServiciosList,
                        conceptos = conceptoList
                    });                   
                }
                return JsonConvert.SerializeObject(kitsList);
            }
            else
            {
                return null;
            }

        }
    }
}
