

using CitaActiva.ModelsViews;
using System;
using System.Net;
using System.Net.Mail;

namespace CitaActiva.Services
{
    public class SendEmailService
    {
        public bool SendEmail(AppointmentModel appointmentModel, string workShopName, string address, string city, string receptionistName)
        {
            try
            {
                string eMailBody = ""
                    + "<p>Estimado " + appointmentModel.contactName +" Se ha generado una Cita con el Id. " + appointmentModel.id + "</p>"
                    + "<table>"
                    + "<tr>"
                    + "     <td> En la Agencia: </td><td>" + workShopName + "</td>"
                    + "</tr>"
                    + "<tr>"
                    + "     <td> Ubicada en: </td><td>" + address + ", "+ city + "</td>"
                    + "</tr>"
                    + "<tr>"
                    + "     <td> El dia: </td><td>" + appointmentModel.plannedData.plannedDate + "</td>"
                    + "</tr>"
                    + "<tr>"
                    + "     <td> A las: </td><td>" + appointmentModel.plannedData.plannedTime + "</td>"
                    + "</tr>"
                    + "<tr>"
                    + "     <td> Será atendido por: </td><td>" + receptionistName + "</td>"
                    + "</tr>"
                    + "<tr>"
                    + "     <td> Datos del Vehículo: </td><td>" + appointmentModel.vehiclePlate + "</td>"
                    + "</tr>"
                    + "</table>";

                using (var message = new MailMessage())
                {
                    message.To.Add(new MailAddress(appointmentModel.contactMail, appointmentModel.contactName));
                    message.From = new MailAddress("marco.villegas@autocom.mx", "Cita Activa");
                    //message.CC.Add(new MailAddress("cc@email.com", "CC Name"));
                    //message.Bcc.Add(new MailAddress("bcc@email.com", "BCC Name"));
                    message.Subject = "Test Cita Activa";
                    message.Body = eMailBody;
                    message.IsBodyHtml = true;

                    using (var client = new SmtpClient("smtp.gmail.com"))
                    {
                        client.Port = 587;
                        client.UseDefaultCredentials = false;
                        client.Credentials = new NetworkCredential("marco.villegas@autocom.mx", "ApewNp1J");
                        client.EnableSsl = true;
                        client.Send(message);
                    }
                }
                return true;
            }catch(Exception ex)
            {
                return false;
            }


        }
    }
}
