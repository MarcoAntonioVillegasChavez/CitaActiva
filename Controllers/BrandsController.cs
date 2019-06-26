using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CitaActiva.Models;
using CitaActiva.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CitaActiva.Controllers
{
    public class BrandsController : Controller
    {
        [HttpGet]
        [Route("/Appointment/Brands", Name = "BrandsRoute")]
        public async Task<IActionResult> Index()
        {
            TokenController tokenController = new TokenController();
            Token token = tokenController.ObtenerToken();
            BrandsService brandsService = new BrandsService();
            string result = await brandsService.GetBrands(token);
            return Json(result);
        }
       
        /*
        public Token ObtenerToken()
        {

            Token token = new Token();
            TokenService tokenService = new TokenService();

            if (Request.Cookies["tokenVehicle"] == null)
            {
                CookieOptions tokenCookie = new CookieOptions();
                tokenCookie.Expires = DateTime.Now.AddSeconds(600);
                token = tokenService.ObtenerTokenVechicleStock();
                Response.Cookies.Append("tokenVehicle", token.access_token, tokenCookie);
            }
            else
            {
                token.access_token = Request.Cookies["tokenVehicle"];
            }
            return token;
        }*/

    }
}