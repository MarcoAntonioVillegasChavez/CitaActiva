

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
        public bool SendEmailCreacionCita(Appointment cita, int actionInd, string address, string city)
        {
            

            try
            {
                string brand = "Nissan";
                if (cita.brandId == "NI")
                {
                    brand = "NISSAN";
                }
                else if (cita.brandId == "IF")
                {
                    brand = "INFINITI";
                }

                string[] hr = cita.plannedData.plannedTime.Split(':');
                string plannedTime = "A las: " + hr[0] + ":" + hr[1];
                string eMailBody = "";
                string subject = "";
                string imgRoute = "";
               


                if (cita != null)
                {
                    if (actionInd == 0)
                    {
                        eMailBody = ""
                        + "<img src='cid:imagen' />"
                        + "<table style='width: 100 %' cellpadding='5'>"
                         + "<tr valign = 'center'>"
                        + "     <td  colspan='2' align='center' valign = 'center'> Estimado <b>" + cita.contactName + ".</b></td>"
                        + "</tr>"
                        + "<tr valign = 'center'>"
                        + "     <td colspan='2' align='center' valign = 'center'> Se ha generado una cita con el ID <b>" + cita.id + "</b>.</td>"
                        + "</tr>"
                        + "<tr valign = 'center'>"
                        + "     <td  width='50 %' align='rigth' ><b> En la Agencia: </b></td><td align='left' width='50 %' >" + cita.workShopName + "</td>"
                        + "</tr>"
                        //+ "<tr valign = 'center'>"
                        //+ "     <td  width='50 %' align='rigth' > <b>Ubicación: </b></td><td align='left' width='50 %' >" + address + ", <br>" + city + "</td>"
                        //+ "</tr>"
                        + "<tr  valign = 'center'>"
                        + "     <td  width='50 %'  align='rigth' > <b>El dia: </b></td><td align='left' width='50 %'>" + cita.plannedData.plannedDate + "</td>"
                        + "</tr>"
                        + "<tr  valign = 'center'>"
                        + "     <td  width='50 %'  align='rigth' > <b>A las: </b></td><td align='left' width='50 %'>" + plannedTime + "</td>"
                        + "</tr>"
                        + "<tr  valign = 'center'>"
                        + "     <td colspan='2' > <b>Datos del Vehículo: </b></td>"
                        + "</tr>"
                        + "<tr  valign = 'center'>"
                        + "     <td  width='50 %' align='rigth' > <b>Placa: </b></td><td align='left' width='50 %'>" + cita.vehiclePlate + "</td>"
                        + "</tr>"
                        + "<tr  valign = 'center'>"
                        + "     <td  width='50 %' align='rigth' > <b>Marca: </b></td><td align='left' width='50 %'>" + brand + "</td>"
                        + "</tr>"
                        + "<tr  valign = 'center'>"
                        + "     <td  width='50 %' align='rigth' > <b>Modelo: </b></td><td align='left' width='50 %'>" + cita.version + "</td>"
                        + "</tr>"
                        + "<tr  valign = 'center'>"
                        + "     <td  width='50 %' align='rigth' > <b>Año: </b></td><td align='left' width='50 %'>" + cita.vehicleYear + "</td>"
                        + "</tr>";
                      

                        if(cita.comments == null || cita.comments == "")
                        {
                            eMailBody += "<tr  valign = 'center'>"
                                + "     <td cospan='2' width='50 %' align='rigth' > <b>Observaciones: </b></td>"
                                + "</tr>"
                                + "<tr  valign = 'center'>"
                                + "     <td align='left' colspan='2' width='50 %'>" + cita.comments + "</td>"
                                + "</tr>";
                        }

                        eMailBody += "<tr  valign = 'center'>"
                        + "     <td align='left' colspan='2' width='50 %'><b>Servicios: </b></td>"
                        + "</tr>";
                       
                        for (int i = 0; i< cita.labours.Count; i++)
                        {
                            eMailBody += "<tr  valign = 'center'>"
                             + "     <td align='left' colspan='2' width='50 %'>" + cita.labours[i].description + " </td>"
                             + "</tr>";
                        }
                        eMailBody += "<tr  valign = 'center'>"
                        + "     <td cospan='2' ><b>Para cualquier cambio o cancelación en su cita, favor de contactar al tel: 800 711 2886.</b></td>"
                        + "</tr>";

                        // '';
                        eMailBody += "</table>";

                        //eMailBody
                        subject = "Agendamiento de cita. Id " + cita.id;
                        imgRoute = "wwwroot/img/CitaAgendada.jpg";
               
                    }
                    else
                    {

                        eMailBody = ""
                      + "<img src='cid:imagen' />"
                      + "<table style='width: 100 %' cellpadding='5'>"
                       + "<tr valign = 'center'>"
                      + "     <td  colspan='2' align='center' valign = 'center'> Estimado <b>" + cita.contactName + ".</b></td>"
                      + "</tr>"
                      + "<tr valign = 'center'>"
                      + "     <td colspan='2' align='center' valign = 'center'> Se ha re agendado la cita con el ID <b>" + cita.id + "</b>.</td>"
                      + "</tr>"
                      + "<tr valign = 'center'>"
                      + "     <td  width='50 %' align='rigth' ><b> En la Agencia: </b></td><td align='left' width='50 %' >" + cita.workShopName + "</td>"
                      + "</tr>"
                      + "<tr valign = 'center'>"
                      + "     <td  width='50 %' align='rigth' > <b>Ubicación: </b></td><td align='left' width='50 %' >" + address + ", <br>" + city + "</td>"
                      + "</tr>"
                      + "<tr  valign = 'center'>"
                      + "     <td  width='50 %'  align='rigth' > <b>El dia: </b></td><td align='left' width='50 %'>" + cita.plannedData.plannedDate + "</td>"
                      + "</tr>"
                      + "<tr  valign = 'center'>"
                      + "     <td  width='50 %'  align='rigth' > <b>A las: </b></td><td align='left' width='50 %'>" + plannedTime + "</td>"
                      + "</tr>"
                      + "<tr  valign = 'center'>"
                      + "     <td colspan='2' > <b>Datos del Vehículo: </b></td>"
                      + "</tr>"
                      + "<tr  valign = 'center'>"
                      + "     <td  width='50 %' align='rigth' > <b>Placa: </b></td><td align='left' width='50 %'>" + cita.vehiclePlate + "</td>"
                      + "</tr>"
                      + "<tr  valign = 'center'>"
                      + "     <td  width='50 %' align='rigth' > <b>Marca: </b></td><td align='left' width='50 %'>" + brand + "</td>"
                      + "</tr>"
                      + "<tr  valign = 'center'>"
                      + "     <td  width='50 %' align='rigth' > <b>Modelo: </b></td><td align='left' width='50 %'>" + cita.version + "</td>"
                      + "</tr>"
                      + "<tr  valign = 'center'>"
                      + "     <td  width='50 %' align='rigth' > <b>Año: </b></td><td align='left' width='50 %'>" + cita.vehicleYear + "</td>"
                      + "</tr>"
                       + "<tr  valign = 'center'>"
                        + "     <td  width='50 %' align='rigth' > <b>Observaciones: </b></td><td align='left' width='50 %'>" + cita.comments + "</td>"
                        + "</tr>"
                      + "</table>";

                        subject = "Re Agendamiento de cita. Id " + cita.id;
                        imgRoute = "wwwroot/img/LogoNuevo.jpg";
                    }
                }
                else if (cita != null)
                {
                    eMailBody = ""
                        + "<img src='cid:imagen' />"
                        + "<table style='width: 100 %'  cellpadding='5' >"
                         + "<tr  align='center' valign = 'center' > "
                        + "     <td colspan='2' align='center' valign = 'center' > Estimado <b>" + cita.contactName + "</b>.</td>"
                        + "</tr>"
                         + "<tr align='center' valign = 'center'>"
                        + "     <td colspan='2' align='center' valign = 'center'>Se ha CANCELADO su cita agendada con el ID <b>" + cita.id + "</b>.</td>"
                        + "</tr>"
                        + "<tr valign = 'center'>"
                        + "     <td align='rigth' width='50 %' > <b>El dia: </b></td><td align='left' width='50 %'>" + cita.plannedData.plannedDate + "</td>"
                        + "</tr>"
                        + "<tr valign = 'center'>"
                        + "     <td align='rigth' width='50 %'><b> A las: </b></td><td align='left' width='50 %'>" + plannedTime + "</td>"
                        + "</tr>"
                        + "<tr valign = 'center'>"
                        + "     <td colspan='2'> <b>Datos del Vehículo: </b></td>"
                        + "</tr>"
                        + "<tr valign = 'center'>"
                        + "     <td align='rigth' width='50 %' > <b>Placa: </b></td><td align='left' width='50 %'>" + cita.vehiclePlate + "</td>"
                        + "</tr>"
                        + "<tr valign = 'center'>"
                        + "     <td align='rigth' width='50 %'> <b>Marca: </b></td><td align='left' width='50 %' >" + brand + "</td>"
                        + "</tr>"
                        + "<tr valign = 'center'>"
                        + "     <td align='rigth' width='50 %'><b> Modelo: </b></td><td align='left' width='50 %'>" + cita.version + "</td>"
                        + "</tr>"
                        + "<tr valign = 'center'>"
                        + "     <td align='rigth' width='50 %'><b> Año: </b></td><td align='left' width='50 %'>" + cita.vehicleYear + "</td>"
                        + "</tr>"
                         + "<tr  valign = 'center'>"
                        + "     <td  width='50 %' align='rigth' > <b>Observaciones: </b></td><td align='left' width='50 %'>" + cita.comments + "</td>"
                        + "</tr>"
                        + "</table>";

                    eMailBody += "Para cualquier duda, por favor, comuniquese al  <a href='tel: 800 711 2886' ><strong> 800 711 2886</strong></a> ";

                    subject = "Cancelacion de cita. Id " + cita.id;
                    imgRoute = "wwwroot/img/CitaCancelada.jpg";
                }

                if (SendEmail(cita.contactMail, cita.contactName, subject, eMailBody, imgRoute))
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
                    //using (var client = new SmtpClient("smtp-relay.gmail.com"))
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
