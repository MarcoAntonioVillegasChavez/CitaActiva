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
                //using (DataContext db = new DataContext())
                //{
                //    var list = db.MarcasVehiculos.OrderBy(mv => mv.id_marca);
                //    string marcasVehiculo = JsonConvert.SerializeObject(list.ToArray());
                return null;//marcasVehiculo;
                //}
            }catch(Exception ex)
            {
                return null;
            }
           
        }
        public string GetMarcaByID(int id_marcavehiculo)
        {
            try
            {
                //using (DataContext db = new DataContext())
                //{
                //    var Marca = from mv in db.MarcasVehiculos
                //                where mv.id_marca == id_marcavehiculo
                //                select new
                //                {
                //                    mv.id_marca,
                //                    mv.codigo_qis,
                //                    mv.nombre_marca
                //                };
                //    MarcasVehiculo marcasVehiculo = new MarcasVehiculo();
                //    var MarcasList = Marca.ToList();

                //    if (MarcasList.Count > 0)
                //    {
                //        marcasVehiculo.id_marca = MarcasList[0].id_marca;
                //        marcasVehiculo.codigo_qis = MarcasList[0].codigo_qis;
                //        marcasVehiculo.nombre_marca = MarcasList[0].nombre_marca;

                //        return JsonConvert.SerializeObject(marcasVehiculo);
                //    }else
                //    {
                        return null;
                //    }
                //}
            }
            catch (Exception ex)
            {
                return null;
            }
            }
        }
    }
