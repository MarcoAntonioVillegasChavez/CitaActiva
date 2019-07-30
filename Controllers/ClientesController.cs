using CitaActiva.Models;
using CitaActiva.ModelsViews;
using CitaActiva.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NToastNotify;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;

namespace CitaActiva.Controllers
{
    public class ClientesController : Controller
    {
        DataContext db = new DataContext();
        GoogleReCaptcha _googleReCaptcha;
        Servidor _servidor;
        private readonly IToastNotification _toastNotification;
        // private ILogger _logger;
        //public ClientesController(IToastNotification toastNotification, IOptions<GoogleReCaptcha> GoogleReCaptcha, ILogger logger)
        public ClientesController(IToastNotification toastNotification, IOptions<GoogleReCaptcha> GoogleReCaptcha, IOptions<Servidor> servidor)
        {
            _toastNotification = toastNotification;
            _googleReCaptcha = GoogleReCaptcha.Value;
            _servidor = servidor.Value;
        }
               
        [HttpGet]
        public IActionResult Index()
        {


            return View();
        }
        [HttpPost]
        [Route("/Clientes/Index/", Name = "LoginRoute")]
        public IActionResult Index([FromForm] Clientes cliente)
        {
            try
            {
                CryptographyService cryptographyService = new CryptographyService();
                cliente.password = cryptographyService.crypt(cliente.password);

                var usuarioAutenticado = db.Clientes.Where(c => c.email_cliente == cliente.email_cliente && c.password == cliente.password && c.cliente_activo == 1).ToList();

                if (usuarioAutenticado.Count > 0)
                {
                    //CookieOptions userCookie = new CookieOptions();
                    //userCookie.Expires = DateTime.Now.AddHours(5);
                    //Response.Cookies.Append("cliente", usuarioAutenticado[0].cuenta_personal, userCookie);

                    Response.Cookies.Append("cliente", usuarioAutenticado[0].cuenta_personal, new CookieOptions()  {
                        Expires = DateTime.Now.AddHours(5)
                    });
                    Response.Cookies.Append("nombreCliente", usuarioAutenticado[0].nombre_cliente, new CookieOptions()
                    {
                        Expires = DateTime.Now.AddHours(5)
                    });

                    return RedirectToAction("Index", new RouteValueDictionary(
                    new { controller = "Citas", action = "Index" }));
                }
                else
                {
                    _toastNotification.AddWarningToastMessage("El usuario o contraseña no existen o son incorrectos.");
                    return View();
                }
            } catch (Exception ex)
            {
                _toastNotification.AddWarningToastMessage("El usuario o contraseña son incorrectos. " + ex.Message.ToString());
                return View();
            }

        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewData["ReCaptchaKey"] = _googleReCaptcha.key;//_configuration.GetSection("GoogleReCaptcha:key").Value;

            Clientes cliente = new Clientes();

            if (Request.Cookies["cliente"] != null)
            {

                using (var context = new DataContext())
                {
                    var clientes = from c in context.Clientes
                                   where c.cuenta_personal == Request.Cookies["cliente"]
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
                                       c.homo_clave,
                                       c.fecha_registro,
                                       c.cliente_activo
                                   };
                    var clienteList = clientes.ToList();

                    if (clienteList.Count > 0)
                    {
                        using (DataContext ctx = new DataContext())
                        {
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
                            cliente.homo_clave = clienteList[0].homo_clave;
                            cliente.fecha_registro = clienteList[0].fecha_registro;
                            cliente.cliente_activo = clienteList[0].cliente_activo;

                            ViewBag.actualizacionInd = 1;
                        }
                    }
                }
            }
            else
            {
                cliente.cuenta_personal = "---";
                cliente.fecha_nacimiento = DateTime.Now.AddYears(-18);
                ViewBag.actualizacionInd = 0;
            }
            return View(cliente);
        }
        [HttpPost]
        public IActionResult Create([FromForm] Clientes clientes, string gRecaptchaResponse)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    CustomerService customerService = new CustomerService();

                    if (!customerService.ReCaptchaPassed(gRecaptchaResponse, _googleReCaptcha.secret))
                    {
                        _toastNotification.AddErrorToastMessage("El captcha no ha sido seleccionado correctamente, podria ser un robot.");
                        ModelState.AddModelError(string.Empty, "El captcha no ha sido seleccionado correctamente, podria ser un robot.");
                        ViewBag.actualizacionInd = 0;
                    }
                    else
                    {

                        clientes.cuenta_personal = Guid.NewGuid().ToString();
                        clientes.fecha_registro = DateTime.Now;
                        if (clientes.homo_clave == null)
                        {
                            clientes.homo_clave = "";
                        }

                        CryptographyService cryptographyService = new CryptographyService();
                        string pass = cryptographyService.crypt(clientes.password);
                        clientes.password = pass;

                        if (Request.Cookies["cliente"] != null)
                        {
                            clientes.cuenta_personal = Request.Cookies["cliente"];

                            using (DataContext ctx = new DataContext())
                            {
                                ctx.Entry(clientes).State = EntityState.Modified;
                                ctx.SaveChanges();
                            }

                            SendEmailService sendEmailServie = new SendEmailService();
                            sendEmailServie.SendEmailActualizacionUsuario(clientes.email_cliente, clientes.nombre_cliente);
                            _toastNotification.AddSuccessToastMessage("Tus datos han sido actualizados. ");

                            
                        }
                        else
                        {
                            clientes.id_cliente = 0;

                            db.Clientes.Add(clientes);
                            db.SaveChanges();

                            SendEmailService sendEmailServie = new SendEmailService();
                            sendEmailServie.SendEmailCreacionUsuario(clientes.cuenta_personal, clientes.email_cliente, clientes.nombre_cliente, _servidor.url);
                            _toastNotification.AddSuccessToastMessage("Te hemos enviado un correo, Por favor has clic en el enlace para activar tu usuario. ");

                            return RedirectToAction("Confirmacion", new RouteValueDictionary(
                                new { controller = "Clientes", action = "Confirmacion" }));
                        }
                       
                    }
                }
            }
            catch (Exception ex)
            {
                _toastNotification.AddErrorToastMessage("Ha ocurrido un error. Por favor comunicate con el administrador.");
                ViewBag.actualizacionInd = 0;
            }

            return View();
          
        }
        [HttpGet]
        [Route("/Clientes/ActivarCliente/{cuenta_personal}", Name = "ActivarClienteRoute")]
        public IActionResult ActivarCliente(string cuenta_personal)
        {
            try
            {
                Clientes cliente = new Clientes();

                using (var context = new DataContext())
                {
                    var clientes = from c in context.Clientes
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
                                       c.homo_clave,
                                       c.fecha_registro,
                                       c.cliente_activo
                                   };
                    var clienteList = clientes.ToList();

                    if (clienteList.Count > 0)
                    {
                        using (DataContext ctx = new DataContext())
                        {
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
                            cliente.homo_clave = clienteList[0].homo_clave;
                            cliente.fecha_registro = clienteList[0].fecha_registro;
                            cliente.cliente_activo = 1;

                            ctx.Entry(cliente).State = EntityState.Modified;
                            ctx.SaveChanges();
                        }
                        ViewBag.Mensaje = "Se ha activado tu usuario, ahora ya puedes acceder al agendamiento de citas, ingresando tu usuario y contraseña.";
                    }
                    else
                    {
                        ViewBag.Mensaje = "El usuario no existe.";
                    }
                }
                return View();
            }
            catch (Exception ex)
            {
                return View();
            }

        }
        [HttpGet]
        public IActionResult Confirmacion()
        {
            return View();
        }


        public Clientes LimpiarCliente(Clientes clientes)
        {
            clientes.cuenta_personal = "---";
            clientes.nombre_cliente = null;
            clientes.apellido_paterno = null;
            clientes.apellido_materno = null;
            clientes.rfc = null;
            clientes.homo_clave = null;
            clientes.email_cliente = null;

            return clientes;
        }

        [HttpGet]
        [Route("/Clientes/BuscarClienteById/{cuenta_personal}", Name="BuscarClienteRoute")]
        public string BuscarClienteById(string cuenta_personal)
        {
            Clientes clientes = new Clientes();
            using (DataContext db = new DataContext())
            {
                var result = from c in db.Clientes
                             where c.cuenta_personal == Request.Cookies["cliente"] 
                             && c.cliente_activo == 1
                             select new
                             {
                                 c.nombre_cliente,
                                 c.apellido_paterno,
                                 c.apellido_materno,
                                 c.email_cliente,
                                 c.rfc,
                                 c.telefono
                             };
                var clientesList = result.ToList();
                clientes.nombre_cliente = clientesList[0].nombre_cliente;
                clientes.apellido_paterno = clientesList[0].apellido_paterno;
                clientes.apellido_materno = clientesList[0].apellido_materno;
                clientes.email_cliente = clientesList[0].email_cliente;
                clientes.rfc = clientesList[0].rfc;
                clientes.telefono = clientesList[0].telefono;
            }
            return JsonConvert.SerializeObject(clientes);
        }

    }
}