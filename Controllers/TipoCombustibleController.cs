using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CitaActiva.ModelsViews;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CitaActiva.Controllers
{
    public class TipoCombustibleController : Controller
    {
        public string GetTipoCombustible()
        {
            try
            {
                //using (DataContext db = new DataContext())
                //{
                //    var list = db.TipoCombustibles.OrderBy(tc => tc.descripcion);
                //    string tipoCombustibles = JsonConvert.SerializeObject(list.ToArray());
                return null; // tipoCombustibles;
                //}
            }catch(Exception ex)
            {
                return null;
            }
               
        }
    }
}