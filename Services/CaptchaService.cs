using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace CitaActiva.Services
{
    public class CaptchaService
    {
        public async Task<string> GetAppointments()
        {
            WebRequest request = WebRequest.Create("");
            //request.ContentType = "application/json; charset=UTF-8";
            //request.Method = "GET";
            //request.Timeout = 2 * 60 * 1000; // 2 min tiempo
            //request.Headers["Authorization"] = " Bearer " + token.access_token;

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
