using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CitaActiva.gRPC;
using CitaActiva.Models;
using CitaActiva.ModelsViews;
using CitaActiva.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using NToastNotify;


namespace CitaActiva.Controllers
{
    public class CitasController : Controller
    {
        private readonly IToastNotification _toastNotification;
        GoogleReCaptcha _googleReCaptcha;
        Servidor _servidor;
        Ambiente _ambiente;
        public CitasController(IToastNotification toastNotification, IOptions<GoogleReCaptcha> GoogleReCaptcha, IOptions<Servidor> servidor, IOptions<Ambiente> evironment)
        {
            _toastNotification = toastNotification;
            _googleReCaptcha = GoogleReCaptcha.Value;
            _servidor = servidor.Value;
            _ambiente = evironment.Value;
        }
        public async Task <IActionResult> Index()
        {
            DateTime thisDay = DateTime.Today;
            ViewBag.Year = thisDay.Year.ToString();
            ViewBag.Month = thisDay.Month.ToString();
            ViewBag.Day = thisDay.Day.ToString();
            ViewBag.Hora = DateTime.Now.AddMinutes(60).ToString("HH:mm:ss");

            //Llena el dropdownlist de marcas
            GrpcMarcas gRcpMarcas = new GrpcMarcas();
            var marcasResult = JsonConvert.DeserializeObject<List<MarcasVehiculo>>(await gRcpMarcas.ListarMarcas());
            ViewBag.marcasVehiculo = marcasResult;

            GrpcZonas grpcZonas = new GrpcZonas();
            var zonaResult = JsonConvert.DeserializeObject<List<Zonas>>(await grpcZonas.ListarZonas());
            ViewBag.zonas = zonaResult;

            //Llena el dropdownlist tipos de combustibles
            GrpcCombustibles gRpcCombustibles = new GrpcCombustibles();
            var tipoCombustibleResult = JsonConvert.DeserializeObject<List<TipoCombustible>>(await gRpcCombustibles.ListarCombustibles()); //JsonConvert.DeserializeObject<List<TipoCombustible>>(tipoCombustibleController.GetTipoCombustible());
            ViewBag.tipoCombustibleList = tipoCombustibleResult;

            return View();
        }
        [HttpPost]
        [Route("/Citas/Agendamiento/{cita}", Name = "AgendamientoRoute")]
        public async Task<string> Index([FromForm] Appointment cita)
        {
            try
            {
                MarcasVehiculoController marcasVehiculoController = new MarcasVehiculoController();
                //var marcasVehiculo = JsonConvert.DeserializeObject<MarcasVehiculo>(marcasVehiculoController.GetMarcaByID(Convert.ToInt32(cita.brandId)));
                //cita.brandCode = marcasVehiculo.codigo_qis;

                DeliveryData deliveryData = new DeliveryData();
                deliveryData.receptionistId = "";
                deliveryData.deliveryDate = "";
                deliveryData.deliveryTime = "";

             
                AppointmentResult appointmentModel = new AppointmentResult();

                appointmentModel.id = "";
                appointmentModel.needReplacementCar = false;
                appointmentModel.vehiclePlate = "Cita Activa - " + cita.vehiclePlate;
                appointmentModel.contactName = cita.contactName;
                appointmentModel.contactMail = cita.contactMail;
                appointmentModel.contactPhone = cita.contactPhone;
                appointmentModel.clientReceived = false;
                appointmentModel.workshopId = cita.workshopId;
                appointmentModel.pickUpVehicle = false;
                appointmentModel.vehicleYear = cita.vehicleYear.ToString();
                appointmentModel.deliveryData = deliveryData;
                appointmentModel.versionId = cita.version;
                appointmentModel.plannedData = cita.plannedData;
                appointmentModel.mileage = cita.mileage;
                appointmentModel.labours = cita.labours;
                appointmentModel.comments =  cita.comments;

                 GrpcCitas grpc = new GrpcCitas();

                string result = "";
                if (_ambiente.environment == "Produccion")
                {
                    result = await grpc.CrearCita(appointmentModel);
                }
                else if (_ambiente.environment == "Pruebas" ||  _ambiente.environment == "Desarrollo")
                {
                    result = "No se ha generado la cita en quiter, porque es un sistema de pruebas";//await grpc.CrearCita(appointmentModel);

                }               
                cita.id = result;

                if (result != "Ha ucurrido un error.")
                {
                    try
                    {
                        SendEmailService sendEmail = new SendEmailService();
                        sendEmail.SendEmailCreacionCita(cita, 0,"", "");
                    } catch(Exception ex)
                    {
                    }

                    _toastNotification.AddSuccessToastMessage("La cita " + result + " ha sido agendada con exito");
                }

                return result;
              
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        //public IActionResult Historial()
        //{
        //    return View();
        //}
        public IActionResult Gracias()
        {
            return View();
        }
    }
}