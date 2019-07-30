using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CitaActiva.Models;
using CitaActiva.ModelsViews;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CitaActiva.Controllers
{
    public class MarcasVehiculoController : Controller
    {
        public string GetMarcasVehiculo()
        {
            try
            {
                using (DataContext db = new DataContext())
                {
                    var list = db.MarcasVehiculos.OrderBy(mv => mv.id_marcavehiculo);
                    string marcasVehiculo = JsonConvert.SerializeObject(list.ToArray());
                    return marcasVehiculo;
                }
            }catch(Exception ex)
            {
                return null;
            }
           
        }
        public string GetMarcaByID(int id_marcavehiculo)
        {
            try
            {
                using (DataContext db = new DataContext())
                {
                    var Marca = from mv in db.MarcasVehiculos
                                where mv.id_marcavehiculo == id_marcavehiculo
                                select new
                                {
                                    mv.id_marcavehiculo,
                                    mv.codigo,
                                    mv.descripcion
                                };
                    MarcasVehiculo marcasVehiculo = new MarcasVehiculo();
                    var MarcasList = Marca.ToList();

                    if (MarcasList.Count > 0)
                    {
                        marcasVehiculo.id_marcavehiculo = MarcasList[0].id_marcavehiculo;
                        marcasVehiculo.codigo = MarcasList[0].codigo;
                        marcasVehiculo.descripcion = MarcasList[0].descripcion;

                        return JsonConvert.SerializeObject(marcasVehiculo);
                    }else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            }
        }
    }
