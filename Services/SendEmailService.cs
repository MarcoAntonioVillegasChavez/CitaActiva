

using CitaActiva.Models;
using CitaActiva.ModelsViews;
using System;
using System.Net;
using System.Net.Mail;

namespace CitaActiva.Services
{
    public class SendEmailService
    {
        public bool SendEmail(string workShopName, string address, string city, Appointment appointment)
        {
            try
            {
                string brand = "";
                if (appointment.brandId == "NI")
                {
                    brand = "NISSAN";
                }
                else if (appointment.brandId == "IF")
                {
                    brand = "INFINITI";
                }

                string[] hr = appointment.plannedTime.Split(':');
                string plannedTime = "A las: " + hr[0] + ":" + hr[1];
                string eMailBody = "";
                string subject = "";

                if (appointment.deletedInd == 0)
                {
                    eMailBody = ""
                        + "<table>"
                         + "<tr>"
                        + "     <td  colspan=2> Estimado " + appointment.contactName+ ".</td>"
                        + "</tr>"
                        + "<tr>"
                        + "     <td  colspan=2> Se ha generado una cita con el Id. " + appointment.id + ".</td>"
                        + "</tr>"
                        + "<tr>"
                        + "     <td> En la Agencia: </td><td>" + workShopName + "</td>"
                        + "</tr>"
                        + "<tr>"
                        + "     <td> Ubicada en: </td><td>" + address + ", " + city + "</td>"
                        + "</tr>"
                        + "<tr>"
                        + "     <td> El dia: </td><td>" + appointment.plannedDate + "</td>"
                        + "</tr>"
                        + "<tr>"
                        + "     <td> A las: </td><td>" + plannedTime + "</td>"
                        + "</tr>"
                        + "<tr>"
                        + "     <td> Datos del Vehículo: </td><td></td>"
                        + "</tr>"
                        + "<tr>"
                        + "     <td> Placa: </td><td>" + appointment.vehiclePlate + "</td>"
                        + "</tr>"
                        + "<tr>"
                        + "     <td> Marca: </td><td>" + brand + "</td>"
                        + "</tr>"
                        + "<tr>"
                        + "     <td> Modelo: </td><td>" + appointment.version + "</td>"
                        + "</tr>"
                        + "<tr>"
                        + "     <td> Año: </td><td>" + appointment.vehicleYear + "</td>"
                        + "</tr>"
                        + "</table>";

                    subject = "Agendamiento de cita. Id " + appointment.id;
                }
                else if (appointment.deletedInd == 1)
                {
                    eMailBody = ""
                        + "<table>"
                        + "<tr>"
                        + "     <td colspan=2>Estimado " + appointment.contactName + ".</td>"
                        + "</tr>"
                         + "<tr>"
                        + "     <td colspan=2>Se ha CANCELADO su cita agendada con el Id. " + appointment.id + ".</td>"
                        + "</tr>"
                        + "<tr>"
                        + "     <td> El dia: </td><td>" + appointment.plannedDate + "</td>"
                        + "</tr>"
                        + "<tr>"
                        + "     <td> A las: </td><td>" + plannedTime + "</td>"
                        + "</tr>"
                        + "<tr>"
                        + "     <td> Datos del Vehículo: </td><td></td>"
                        + "</tr>"
                        + "<tr>"
                        + "     <td> Placa: </td><td>" + appointment.vehiclePlate + "</td>"
                        + "</tr>"
                        + "<tr>"
                        + "     <td> Marca: </td><td>" + brand + "</td>"
                        + "</tr>"
                        + "<tr>"
                        + "     <td> Modelo: </td><td>" + appointment.version + "</td>"
                        + "</tr>"
                        + "<tr>"
                        + "     <td> Año: </td><td>" + appointment.vehicleYear + "</td>"
                        + "</tr>"
                        + "</table>";

                    subject = "Cancelacion de cita. Id " + appointment.id;
                }


                using (var message = new MailMessage())
                {
                    message.To.Add(new MailAddress(appointment.contactMail, appointment.contactName));
                    message.From = new MailAddress("marco.villegas@autocom.mx", "Cita Activa");
                    //message.CC.Add(new MailAddress("cc@email.com", "CC Name"));
                    message.Bcc.Add(new MailAddress("marco.villegas@autocom.mx", "Cita Activa"));
                    message.Subject = subject;
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
