using CitaActiva.Models;
using CitaActiva.Util;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace CitaActiva.Services
{
    public class WorkshopService
    {
        public async Task <string> GetWorkshops(Token token, string postalCode)
        {
            WebRequest request = WebRequest.Create(Constants.WorkshopUrl);
            request.ContentType = "application/json; charset=UTF-8";
            request.Method = "GET";
            request.Headers["Authorization"] = " Bearer " + token.access_token;

            try
            {
                using (WebResponse response = await request.GetResponseAsync())
                {
                    StreamReader reader = new StreamReader(response.GetResponseStream());
                    return reader.ReadToEnd();
                }
            }
            catch (WebException we)
            {
                var reader = new StreamReader(we.Response.GetResponseStream());
                return reader.ReadToEnd().ToString();
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }

       
        public async Task<string> GetWorkshop(Token token, string id)
        {
            WebRequest request = WebRequest.Create(Constants.WorkshopUrl + "/" + id);
            request.ContentType = "application/json; charset=UTF-8";
            request.Method = "GET";
            request.Headers["Authorization"] = " Bearer " + token.access_token;

            try
            {
                using (WebResponse response = await request.GetResponseAsync())
                {
                    StreamReader reader = new StreamReader(response.GetResponseStream());
                    return reader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }      

    }
}
