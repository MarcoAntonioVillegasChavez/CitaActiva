using CitaActiva.Models;
using CitaActiva.Util;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CitaActiva.Services
{
    public class CustomerService
    {
        public async Task<string> GetCustomer(Token token, string id)
        {
            WebRequest request = WebRequest.Create(Constants.CustomerUrl + "/" + id);
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

        public async Task<string> PostCustomer(Token token, Clientes clientes)
        {
            WebRequest request = (HttpWebRequest)WebRequest.Create(Constants.CustomerUrl);
            request.ContentType = "application/json";
            request.Method = "POST";
            request.Headers["Authorization"] = " Bearer " + token.access_token;

            byte[] body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(clientes));
            using (var postStream = await request.GetRequestStreamAsync())
            {
                await postStream.WriteAsync(body, 0, body.Length);
            }
            string result = "";
            try
            {
                var response = (HttpWebResponse)await request.GetResponseAsync();
                if (response != null)
                {
                    var reader = new StreamReader(response.GetResponseStream());

                    string responseString = await reader.ReadToEndAsync();
                    result = responseString;
                }
                return result;
            }
            catch (WebException we)
            {
                var reader = new StreamReader(we.Response.GetResponseStream());
                result = reader.ReadToEnd().ToString();
                return result;
            }

        }

        public async Task<string> PutCustomer(Token token, Clientes cliente, string id)
        {
            WebRequest request = (HttpWebRequest)WebRequest.Create(Constants.CustomerUrl + "/" + id);
            request.ContentType = "application/json";
            request.Method = "PUT";
            request.Headers["Authorization"] = " Bearer " + token.access_token;

            byte[] body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(cliente));
            using (var postStream = await request.GetRequestStreamAsync())
            {
                await postStream.WriteAsync(body, 0, body.Length);
            }
            string result = "";
            try
            {
                var response = (HttpWebResponse)await request.GetResponseAsync();
                if (response != null)
                {
                    var reader = new StreamReader(response.GetResponseStream());

                    string responseString = await reader.ReadToEndAsync();
                    result = responseString;
                }
                return result;
            }
            catch (WebException we)
            {
                var reader = new StreamReader(we.Response.GetResponseStream());
                result = reader.ReadToEnd().ToString();
                return result;
            }

        }
    }
}
