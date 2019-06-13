

using CitaActiva.Models;
using CitaActiva.ModelsViews;
using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;

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
                string imgRoute = "";



                if (appointment.deletedInd == 0)
                {
                    eMailBody = ""
                        + "<img src='cid:imagen' />"
                        + "<table style='width: 100 %'>"
                         + "<tr valign = 'center'>"
                        + "     <td  colspan='2' align='center' valign = 'center'> Estimado <b>" + appointment.contactName + ".</b></td>"
                        + "</tr>"
                        + "<tr valign = 'center'>"
                        + "     <td colspan='2' align='center' valign = 'center'> Se ha generado una cita con el ID <b>" + appointment.id + "</b>.</td>"
                        + "</tr>"
                        + "<tr valign = 'center'>"
                        + "     <td  width='50 %' align='rigth' ><b> En la Agencia: </b></td><td align='left' width='50 %' >" + workShopName + "</td>"
                        + "</tr>"
                        + "<tr valign = 'center'>"
                        + "     <td  width='50 %' align='rigth' > <b>Ubicada en: </b></td><td align='left' width='50 %' >" + address + ", <br>" + city + "</td>"
                        + "</tr>"
                        + "<tr  valign = 'center'>"
                        + "     <td  width='50 %'  align='rigth' > <b>El dia: </b></td><td align='left' width='50 %'>" + appointment.plannedDate + "</td>"
                        + "</tr>"
                        + "<tr  valign = 'center'>"
                        + "     <td  width='50 %'  align='rigth' > <b>A las: </b></td><td align='left' width='50 %'>" + plannedTime + "</td>"
                        + "</tr>"
                        + "<tr  valign = 'center'>"
                        + "     <td colspan='2' > <b>Datos del Vehículo: </b></td>"
                        + "</tr>"
                        + "<tr  valign = 'center'>"
                        + "     <td  width='50 %' align='rigth' > <b>Placa: </b></td><td align='left' width='50 %'>" + appointment.vehiclePlate + "</td>"
                        + "</tr>"
                        + "<tr  valign = 'center'>"
                        + "     <td  width='50 %' align='rigth' > <b>Marca: </b></td><td align='left' width='50 %'>" + brand + "</td>"
                        + "</tr>"
                        + "<tr  valign = 'center'>"
                        + "     <td  width='50 %' align='rigth' > <b>Modelo: </b></td><td align='left' width='50 %'>" + appointment.version + "</td>"
                        + "</tr>"
                        + "<tr  valign = 'center'>"
                        + "     <td  width='50 %' align='rigth' > <b>Año: </b></td><td align='left' width='50 %'>" + appointment.vehicleYear + "</td>"
                        + "</tr>"
                        + "</table>";

                    subject = "Agendamiento de cita. Id " + appointment.id;
                    imgRoute = "wwwroot/img/3B12-iphone.png";//imgRoute = @"C:\Users\marco.villegas\source\repos\CitaActiva\CitaActiva\wwwroot\img\3B12-iphone.png";
                }
                else if (appointment.deletedInd == 1)
                {
                    eMailBody = ""
                        + "<img src='cid:imagen' />"
                        + "<table style='width: 100 %' >"
                         + "<tr  align='center' valign = 'center' > "
                        + "     <td colspan='2' align='center' valign = 'center' > Estimado <b>" + appointment.contactName + "</b>.</td>"
                        + "</tr>"
                         + "<tr align='center' valign = 'center'>"
                        + "     <td colspan='2' align='center' valign = 'center'>Se ha CANCELADO su cita agendada con el ID <b>" + appointment.id + "</b>.</td>"
                        + "</tr>"
                        + "<tr valign = 'center'>"
                        + "     <td align='rigth' width='50 %' > <b>El dia: </b></td><td align='left' width='50 %'>" + appointment.plannedDate + "</td>"
                        + "</tr>"
                        + "<tr valign = 'center'>"
                        + "     <td align='rigth' width='50 %'><b> A las: </b></td><td align='left' width='50 %'>" + plannedTime + "</td>"
                        + "</tr>"
                        + "<tr valign = 'center'>"
                        + "     <td colspan='2'> <b>Datos del Vehículo: </b></td>"
                        + "</tr>"
                        + "<tr valign = 'center'>"
                        + "     <td align='rigth' width='50 %' > <b>Placa: </b></td><td align='left' width='50 %'>" + appointment.vehiclePlate + "</td>"
                        + "</tr>"
                        + "<tr valign = 'center'>"
                        + "     <td align='rigth' width='50 %'> <b>Marca: </b></td><td align='left' width='50 %' >" + brand + "</td>"
                        + "</tr>"
                        + "<tr valign = 'center'>"
                        + "     <td align='rigth' width='50 %'><b> Modelo: </b></td><td align='left' width='50 %'>" + appointment.version + "</td>"
                        + "</tr>"
                        + "<tr valign = 'center'>"
                        + "     <td align='rigth' width='50 %'><b> Año: </b></td><td align='left' width='50 %'>" + appointment.vehicleYear + "</td>"
                        + "</tr>"
                        + "</table>";

                    subject = "Cancelacion de cita. Id " + appointment.id;
                    imgRoute = "wwwroot/img/3B12-iphone.png";//@"C:\Users\marco.villegas\source\repos\CitaActiva\CitaActiva\wwwroot\img\3B12-iphone.png";
                }

                AlternateView htmlView = AlternateView.CreateAlternateViewFromString(eMailBody, Encoding.UTF8, MediaTypeNames.Text.Html);

                LinkedResource img;//= new LinkedResource(imgRoute, MediaTypeNames.Image.Jpeg);

                using (WebClient webClient = new WebClient())
                {
                    webClient.UseDefaultCredentials = true;
                    byte[] buf = webClient.DownloadData(imgRoute);
                    MemoryStream memoryStream = new MemoryStream(buf);
                    img = new LinkedResource(memoryStream);
                }
                              

                img.ContentId = "imagen";

                htmlView.LinkedResources.Add(img);
               
                using (var message = new MailMessage())
                {
                    message.To.Add(new MailAddress(appointment.contactMail, appointment.contactName));
                    message.From = new MailAddress("marco.villegas@autocom.mx", "Cita Activa");
                    //message.CC.Add(new MailAddress("cc@email.com", "CC Name"));
                    message.Bcc.Add(new MailAddress("marco.villegas@autocom.mx", "Cita Activa"));
                    message.Subject = subject;
                    message.Body = eMailBody;
                    message.IsBodyHtml = true;
                    message.AlternateViews.Add(htmlView);

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
