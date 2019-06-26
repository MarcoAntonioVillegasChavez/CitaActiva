using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CitaActiva.Models;
using CitaActiva.ModelsViews;
using CitaActiva.Services;
using CitaActiva.Util;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CitaActiva.Controllers
{
    //[Route("api/[controller]")]
    
    public class WorkshopController : Controller
    {
        private DataContext db = new DataContext();
        // GET: api/Workshop
        [HttpGet]
        [Route("/Appointment/WorkShop", Name = "WorkShopsRoute")]
        public async Task<string> Index(Token token, string postalCode)
        {
            //WorkshopService workshopService = new WorkshopService();
            //return await workshopService.GetWorkshops(token, postalCode);

            var list = db.Workshops.Where(w => w.activeInd == 1).OrderBy(w => w.id);
            string sorkshops = JsonConvert.SerializeObject(list.ToArray());
            
            return sorkshops;
        }

        [HttpGet("{id}")]
        [Route("/Appointment/WorkShop/{workShopId}", Name = "WorkShopRoute")]
        public async Task<string> GetWorkShop(Token token, string workshopId)
        {
            if (token.access_token == null)
            {
                TokenController tokenController = new TokenController();
                token = tokenController.ObtenerToken();
            }
            WorkshopService workshopService = new WorkshopService();
            return await workshopService.GetWorkshop(token, workshopId);
        }

        /*
        public Token ObtenerToken()
        {

            Token token = new Token();
            TokenService tokenService = new TokenService();

            if (Request.Cookies["token"] == null)
            {
                CookieOptions tokenCookie = new CookieOptions();
                tokenCookie.Expires = DateTime.Now.AddSeconds(600);
                token = tokenService.ObtenerToken();
                Response.Cookies.Append("token", token.access_token, tokenCookie);
            }
            else
            {
                token.access_token = Request.Cookies["token"];
            }
            return token;
        } */
    }
}
