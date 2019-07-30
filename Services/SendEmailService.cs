

using CitaActiva.Models;
using CitaActiva.ModelsViews;
using CitaActiva.Util;
using Microsoft.Extensions.Options;
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
        public bool SendEmailCreacionCita(string workShopName, string address, string city, Appointment appointment, int actionInd)
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

                string[] hr = appointment.plannedData.plannedTime.Split(':');
                string plannedTime = "A las: " + hr[0] + ":" + hr[1];
                string eMailBody = "";
                string subject = "";
                string imgRoute = "";



                if (appointment != null)
                {
                    if (actionInd == 0)
                    {
                        eMailBody = ""
                        + "<img src='cid:imagen' />"
                        + "<table style='width: 100 %' cellpadding='5'>"
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
                        + "     <td  width='50 %' align='rigth' > <b>Ubicación: </b></td><td align='left' width='50 %' >" + address + ", <br>" + city + "</td>"
                        + "</tr>"
                        + "<tr  valign = 'center'>"
                        + "     <td  width='50 %'  align='rigth' > <b>El dia: </b></td><td align='left' width='50 %'>" + appointment.plannedData.plannedDate + "</td>"
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
                        imgRoute = "wwwroot/img/3B12-iphone.png";
               
                    }
                    else
                    {

                        eMailBody = ""
                      + "<img src='cid:imagen' />"
                      + "<table style='width: 100 %' cellpadding='5'>"
                       + "<tr valign = 'center'>"
                      + "     <td  colspan='2' align='center' valign = 'center'> Estimado <b>" + appointment.contactName + ".</b></td>"
                      + "</tr>"
                      + "<tr valign = 'center'>"
                      + "     <td colspan='2' align='center' valign = 'center'> Se ha re agendado la cita con el ID <b>" + appointment.id + "</b>.</td>"
                      + "</tr>"
                      + "<tr valign = 'center'>"
                      + "     <td  width='50 %' align='rigth' ><b> En la Agencia: </b></td><td align='left' width='50 %' >" + workShopName + "</td>"
                      + "</tr>"
                      + "<tr valign = 'center'>"
                      + "     <td  width='50 %' align='rigth' > <b>Ubicación: </b></td><td align='left' width='50 %' >" + address + ", <br>" + city + "</td>"
                      + "</tr>"
                      + "<tr  valign = 'center'>"
                      + "     <td  width='50 %'  align='rigth' > <b>El dia: </b></td><td align='left' width='50 %'>" + appointment.plannedData.plannedDate + "</td>"
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

                        subject = "Re Agendamiento de cita. Id " + appointment.id;
                        imgRoute = "wwwroot/img/3B12-iphone.png";
                    }
                }
                else if (appointment != null)
                {
                    eMailBody = ""
                        + "<img src='cid:imagen' />"
                        + "<table style='width: 100 %'  cellpadding='5' >"
                         + "<tr  align='center' valign = 'center' > "
                        + "     <td colspan='2' align='center' valign = 'center' > Estimado <b>" + appointment.contactName + "</b>.</td>"
                        + "</tr>"
                         + "<tr align='center' valign = 'center'>"
                        + "     <td colspan='2' align='center' valign = 'center'>Se ha CANCELADO su cita agendada con el ID <b>" + appointment.id + "</b>.</td>"
                        + "</tr>"
                        + "<tr valign = 'center'>"
                        + "     <td align='rigth' width='50 %' > <b>El dia: </b></td><td align='left' width='50 %'>" + appointment.plannedData.plannedDate + "</td>"
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
                    imgRoute = "wwwroot/img/cancelada-iphone.png";
                }

                if (SendEmail(appointment.contactMail, appointment.contactName, subject, eMailBody, imgRoute))
                {
                    return true;

                }else
                {
                    return false;
                }
                
            }catch(Exception ex)
            {
                return false;
            }


        }

        public bool SendEmailCreacionUsuario(string cuenta_personal, string email_cliente, string nombre_cliente, string servidor)
        {
            
            string imgRoute = "wwwroot/img/cancelada-iphone.png";
            string subject = "Registro de usuario - Cita Activa";

            string eMailBody = ""
                 + "<img src='cid:imagen' />"
                        + "<table cellpadding='5' >"
                        + "<tr>"
                        +"<td colspan='2'>Estimado " + nombre_cliente + " te has registrado al sistema de agendamiento de Citas de Autocom. </td>"
                        +"</tr>"
                         + "<tr>"
                        + " <td> Tu usuario es: </td><td> " + email_cliente + " </td>"
                        + "</tr>"
                        + "<tr>"
                        + " <td colspan='2'> Por favor haz click en el siguiente enlace, para acceder al agendamiento de citas </td>"
                        + "</tr>"
                        + "<tr>" 
                        + "<td>" + servidor + "/Clientes/ActivarCliente/" + cuenta_personal + "</td>"
                        + "</tr>"
                        + "</table>";

            if (SendEmail(email_cliente, nombre_cliente, subject, eMailBody, imgRoute))
            {
                return true;
            }else
            {
                return false;
            }
        }

        public bool SendEmailActualizacionUsuario(string email_cliente, string nombre_cliente)
        {
            string imgRoute = "wwwroot/img/cancelada-iphone.png";
            string subject = "Actualizacion de Datos de usuario - Cita Activa";

            string eMailBody = ""
                 + "<img src='cid:imagen' />"
                        + "<table style='width: 100 %'  cellpadding='5' >"
                        + "<tr>"
                        + "<td colspan='2'>Estimado " + nombre_cliente + " tus datos han sido actualizados en el sistema de agendamiento de Citas de Autocom. </td>"
                        + "</tr>"
                        + "</table>";

            if (SendEmail(email_cliente, nombre_cliente, subject, eMailBody, imgRoute))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool SendEmail(string contactMail, string contactName, string subject, string eMailBody, string imgRoute)
        {
            try
            {

            AlternateView htmlView = AlternateView.CreateAlternateViewFromString(eMailBody, Encoding.UTF8, MediaTypeNames.Text.Html);

            LinkedResource img;

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
                message.To.Add(new MailAddress(contactMail, contactName));
                message.From = new MailAddress(Constants.correoElectronico, "Cita Activa");
                //message.CC.Add(new MailAddress("cc@email.com", "CC Name"));
                message.Bcc.Add(new MailAddress(Constants.correoElectronico, "Cita Activa"));
                message.Subject = subject;
                message.Body = eMailBody;
                message.IsBodyHtml = true;
                message.AlternateViews.Add(htmlView);

                using (var client = new SmtpClient("smtp.gmail.com"))
                {
                    client.Port = 587;
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential(Constants.correoElectronico, Constants.password);
                    client.EnableSsl = true;
                    client.Send(message);
                }
            }
            return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
