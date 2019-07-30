using CitaActiva.Models;
using CitaActiva.ModelsViews;
using CitaActiva.Util;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
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

        public Clientes DatosCliente(string cuenta_personal)
        {
            try
            {
                using (var ctx = new DataContext())
                {
                    var clientes = from c in ctx.Clientes
                                   where c.cuenta_personal == cuenta_personal
                                   select new
                                   {
                                       c.id_cliente,
                                       c.cuenta_personal,
                                       c.nombre_cliente,
                                       c.apellido_paterno,
                                       c.apellido_materno,
                                       c.fecha_nacimiento,
                                       c.email_cliente,
                                       c.telefono,
                                       c.password,
                                       c.rfc,
                                       c.fecha_registro,
                                       c.cliente_activo
                                   };
                    var clienteList = clientes.ToList();

                    Clientes cliente = new Clientes();

                    cliente.id_cliente = clienteList[0].id_cliente;
                    cliente.cuenta_personal = clienteList[0].cuenta_personal;
                    cliente.nombre_cliente = clienteList[0].nombre_cliente;
                    cliente.apellido_paterno = clienteList[0].apellido_paterno;
                    cliente.apellido_materno = clienteList[0].apellido_materno;
                    cliente.fecha_nacimiento = clienteList[0].fecha_nacimiento;
                    cliente.email_cliente = clienteList[0].email_cliente;
                    cliente.telefono = clienteList[0].telefono;
                    cliente.password = clienteList[0].password;
                    cliente.rfc = clienteList[0].rfc;
                    cliente.fecha_registro = clienteList[0].fecha_registro;

                    return cliente;

                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public bool ReCaptchaPassed(string gRecaptchaResponse, string secret)
        {
            HttpClient httpClient = new HttpClient();
            var res = httpClient.GetAsync($"https://www.google.com/recaptcha/api/siteverify?secret={secret}&response={gRecaptchaResponse}").Result;
            if (res.StatusCode != HttpStatusCode.OK)
            {
                // _logger.LogError("Error while sending request to ReCaptcha");
                return false;
            }

            string JSONres = res.Content.ReadAsStringAsync().Result;
            dynamic JSONdata = JObject.Parse(JSONres);
            if (JSONdata.success != "true")
            {
                return false;
            }

            return true;
        }
    }
}
