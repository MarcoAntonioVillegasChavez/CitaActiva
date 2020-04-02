using CitaActiva.Models;
using CitaActiva.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CitaActiva.Services
{
    public class TokenService
    {
        public Token ObtenerToken()
        {
            string parameters = "";
            WebRequest webRequest = WebRequest.Create(Constants.TokenUrlCitaActiva);
            webRequest.ContentType = "application/x-www-form-urlencoded";
            webRequest.Method = "POST";
            byte[] bytes = Encoding.ASCII.GetBytes(parameters);

            try
            {
                webRequest.ContentLength = bytes.Length;
                Stream stream = webRequest.GetRequestStream();
                stream.Write(bytes, 0, bytes.Length);
                stream.Close();

                Stream res = webRequest.GetResponse().GetResponseStream();
                StreamReader reader = new StreamReader(res);
                String result = reader.ReadToEnd();

                res.Close();
                reader.Close();

                Token token = Newtonsoft.Json.JsonConvert.DeserializeObject<Token>(result);
                return token;
            }
            catch (WebException ex)
            {
                Token token = new Token();
                token.token_type = "Error";
                token.access_token = ex.Message.ToString();
                return token;
            }
        }

        public Token ObtenerTokenVechicleStock()
        {
            string parameters = "";
            WebRequest webRequest = WebRequest.Create(Constants.TokenUrlVehicleStock);
            webRequest.ContentType = "multipart/form-data";
            webRequest.Method = "POST";
            byte[] bytes = Encoding.ASCII.GetBytes(parameters);

            try
            {

                webRequest.ContentLength = bytes.Length;
                Stream stream = webRequest.GetRequestStream();
                stream.Write(bytes, 0, bytes.Length);
                stream.Close();

                Stream res = webRequest.GetResponse().GetResponseStream();
                StreamReader reader = new StreamReader(res);
                String result = reader.ReadToEnd();

                res.Close();
                reader.Close();

                Token token = Newtonsoft.Json.JsonConvert.DeserializeObject<Token>(result);
                return token;
            }
            catch (WebException ex)
            {
                Token token = new Token();
                token.token_type = "Error";
                token.access_token = ex.Message.ToString();
                return token;
            }
        }
        public Token ObtenerTokenPaquetesServicio()
        {
            string parameters = "";
            WebRequest webRequest = WebRequest.Create(Constants.AgenciasUrl);
            webRequest.ContentType = "application/x-www-form-urlencoded";
            webRequest.Method = "POST";
            byte[] bytes = Encoding.ASCII.GetBytes(parameters);

            try
            {
                webRequest.ContentLength = bytes.Length;
                Stream stream = webRequest.GetRequestStream();
                stream.Write(bytes, 0, bytes.Length);
                stream.Close();

                Stream res = webRequest.GetResponse().GetResponseStream();
                StreamReader reader = new StreamReader(res);
                String result = reader.ReadToEnd();

                res.Close();
                reader.Close();

                var resultado = Newtonsoft.Json.JsonConvert.DeserializeObject(result);

                Token token = new Token();
                token.access_token ="";
                token.expires_in = (7 *(6000 * 24)).ToString();
                return token;
            }
            catch (WebException ex)
            {
                Token token = new Token();
                token.token_type = "Error";
                token.access_token = ex.Message.ToString();
                return token;
            }
        }

        }
}
