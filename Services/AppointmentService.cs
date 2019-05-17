using CitaActiva.Models;
using CitaActiva.ModelsViews;
using CitaActiva.Util;
using Newtonsoft.Json;
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
    public class AppointmentService
    {
        public async Task<string> GetAppointments(Token token)
        {
           WebRequest request = WebRequest.Create(Constants.AppointmentUrl);
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
        public async Task <string> GetAppointment(Token token, string id)
        {
            WebRequest request = WebRequest.Create(Constants.AppointmentUrl+ "/" + id);
            request.ContentType = "application/json; charset=UTF-8";
            request.Method = "GET";
            request.Headers["Authorization"] = " Bearer " + token.access_token;

            try
            {
                using (WebResponse response = await request.GetResponseAsync())
                {
                    StreamReader reader = new StreamReader(response.GetResponseStream());
                    string result = reader.ReadToEnd();
                    response.Close();
                    reader.Close();
                    return result;
                }
            }
            catch (WebException we)
            {
                var reader = new StreamReader(we.Response.GetResponseStream());
                return reader.ReadToEnd().ToString();
            }
        }
        public async Task<string> CreateAppointment(Token token, AppointmentModel appointmentModel)
        {
            WebRequest request = (HttpWebRequest)WebRequest.Create(Constants.AppointmentUrl);
            request.ContentType = "application/json";
            request.Method = "POST";
            request.Headers["Authorization"] = " Bearer " + token.access_token;

            byte[] body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(appointmentModel));
            using (var postStream = await request.GetRequestStreamAsync())
            {
                await postStream.WriteAsync(body, 0, body.Length);
            }
            string result ="";
            try
            {
                var response = (HttpWebResponse)await request.GetResponseAsync();
                if (response != null)
                {
                    var reader = new StreamReader(response.GetResponseStream());
                   
                    string responseString = await reader.ReadToEndAsync();
                    result= responseString;
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

        public async Task<string> UpdateAppointment(Token token, AppointmentModel appointmentModel)
        {
            WebRequest request = (HttpWebRequest)WebRequest.Create(Constants.AppointmentUrl + "/" + appointmentModel.id);
            request.ContentType = "application/json";
            request.Method = "PUT";
            request.Headers["Authorization"] = " Bearer " + token.access_token;

            byte[] body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(appointmentModel));
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
        public async Task<string> DeleteAppointment(Token token, string appointmentId)
        {
            WebRequest request = (HttpWebRequest)WebRequest.Create(Constants.AppointmentUrl+ "/delete");
            request.ContentType = "application/json";
            request.Method = "POST";
            request.Headers["Authorization"] = " Bearer " + token.access_token;

            CitaDelete citaDelete = new CitaDelete();
            citaDelete.appointmentId = appointmentId;

            byte[] body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(citaDelete));
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
