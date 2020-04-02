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
    public class AgenciasService
    {
        public async Task<string> GetAgencias(Token token)
        {
            WebRequest request = WebRequest.Create(Constants.AgenciasUrl);
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
