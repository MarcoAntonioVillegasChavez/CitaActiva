using CitaActiva.Models;
using CitaActiva.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace CitaActiva.Services
{
    public class ReceptionistService
    {
        public async Task<string> GetReceptionistByWorkShop(Token token, string workshopId)
        {
            WebRequest request = WebRequest.Create(Constants.ReceptionistUrl + workshopId);
            request.ContentType = "application/json; charset=UTF-8";
            request.Method = "GET";
            request.Timeout = 2 * 60 * 1000; // 2 min tiempo
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
        public async Task<string> GetReceptionistById(Token token, string receptionistId)
        {
            WebRequest request = WebRequest.Create(Constants.ReceptionistUrlSingle + receptionistId);
            request.ContentType = "application/json; charset=UTF-8";
            request.Method = "GET";
            request.Timeout = 2 * 60 * 1000; // 2 min tiempo
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
    }
}
