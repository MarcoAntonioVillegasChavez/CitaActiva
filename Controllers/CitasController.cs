using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public CitasController(IToastNotification toastNotification, IOptions<GoogleReCaptcha> GoogleReCaptcha, IOptions<Servidor> servidor)
        {
            _toastNotification = toastNotification;
            _googleReCaptcha = GoogleReCaptcha.Value;
            _servidor = servidor.Value;
        }
        public IActionResult Index()
        {
            //Token token = new Token();

            //TokenController tokenController = new TokenController();
            //token = tokenController.ObtenerToken();

            DateTime thisDay = DateTime.Today;
            ViewBag.Year = thisDay.Year.ToString();
            ViewBag.Month = thisDay.Month.ToString();
            ViewBag.Day = thisDay.Day.ToString();
            ViewBag.Hora = DateTime.Now.AddHours(2).ToString("HH:mm:ss");

            //Llena el dropdownlist de Agencias
            //AgenciasController agenciasController = new AgenciasController();
            //var agenciasResult = agenciasController.GetAgencias(token, "", 0);
            //ViewBag.agenciasList = JsonConvert.DeserializeObject<List<Agencias>>(agenciasResult);

            //Llena el dropdownlist de marcas
            MarcasVehiculoController marcasVehiculoController = new MarcasVehiculoController();
            var marcasVehiculoResult = marcasVehiculoController.GetMarcasVehiculo();
            ViewBag.marcasVehiculo = JsonConvert.DeserializeObject<List<MarcasVehiculo>>(marcasVehiculoResult);

            //Llena el dropdownlist de Vehiculos, solo si se es una consulta de agendamiento.
            /*
            FamiliasVehiculoController familiasVehiculoController = new FamiliasVehiculoController();
            var familiasVehiculoResult = familiasVehiculoController.GetFemiliasVehiculo(1);
            ViewBag.familiasVehiculo = "";
            */

            TipoCombustibleController tipoCombustibleController = new TipoCombustibleController();
            var tipoCombustibleResult = JsonConvert.DeserializeObject<List<TipoCombustible>>(tipoCombustibleController.GetTipoCombustible());
            ViewBag.tipoCombustibleList = tipoCombustibleResult;

            //Llena el dropdownlist de Servicios.
            //ServiciosController serviciosController = new ServiciosController();
            //var serviciosResult = serviciosController.GetServicios(1, 1, 1);
            //ViewBag.serviciosList = JsonConvert.DeserializeObject<List<Servicios>>(serviciosResult);

           
            

            return View();
        }
        [HttpPost]
        [Route("/Citas/Agendamiento/{cita}/{plannedData}/{labours}", Name = "AgendamientoRoute")]
        public async Task<string> Index([FromForm] Appointment cita)
        {
            cita.labours.operatorId = "";
            cita.labours.teamId = "";

            MarcasVehiculoController marcasVehiculoController = new MarcasVehiculoController();
            var marcasVehiculo = JsonConvert.DeserializeObject<MarcasVehiculo>(marcasVehiculoController.GetMarcaByID(Convert.ToInt32(cita.brandId)));
            cita.brandCode = marcasVehiculo.codigo;

            Token token = new Token();
            TokenService tokenService = new TokenService();
            token = tokenService.ObtenerToken();
            AppointmentModel appointmentModel = new AppointmentModel();

            appointmentModel.id = "";
            appointmentModel.needReplacementCar = false;
            appointmentModel.vehiclePlate = cita.vehiclePlate;
            appointmentModel.contactName = cita.contactName;
            appointmentModel.contactMail = cita.contactMail;
            appointmentModel.contactPhone = cita.contactPhone;
            appointmentModel.clientReceived = false;
            appointmentModel.workshopId = cita.workshopId;
            appointmentModel.pickUpVehicle = false;
            appointmentModel.vehicleYear = cita.vehicleYear.ToString();
            appointmentModel.brandId = cita.brandId;
            appointmentModel.versionId = cita.version;
            appointmentModel.plannedData = cita.plannedData;
            appointmentModel.labours = cita.labours;

            AppointmentService appointmentService = new AppointmentService();
            //var result = JsonConvert.DeserializeObject<AppointmentResult>(await appointmentService.CreateAppointment(token, appointmentModel));
             var result = JsonConvert.DeserializeObject < AppointmentResult >( "{'deliveryData':{'receptionistId':0},'needReplacementCar':false,'labours':[{'description':60000,'id':6,'plannedHours':2}],'vehiclePlate':'Prueba cita activa QUITER','contactName':'Marco Antonio','clientReceived':false,'workshopId':1,'id':8935060,'contactMail':'tonystark001988 @gmail.com','pickUpVehicle':false,'contactPhone':5567708678,'plannedData':{'plannedDate':'0031 - 01 - 10','plannedTime':'09:00:00','receptionistId':0}}"); //appointmentService.CreateAppointment(token, cita);

            if (result.id != null)
            {
                try
                {
                    KitsClientes kitsClientes = new KitsClientes();
                    using (DataContext db = new DataContext())
                    {
                        kitsClientes.id_kit = 1;
                        kitsClientes.id_cliente = 1;
                        kitsClientes.idvehiculo_cliente = 0;
                        db.Add(kitsClientes);
                        db.SaveChanges();
                    }

                    Citas citas = new Citas();
                    using (DataContext db = new DataContext())
                    {
                        citas.id_appointment = result.id;
                        citas.idkit_cliente = 0;
                        citas.status_cita = 1;
                        citas.fecha_registro = new DateTime();
                        citas.fecha_actualizacion = new DateTime();

                        db.Add(citas);
                        db.SaveChanges();
                        int id = citas.id_cita;
                    }
                    //AgendamientoCitas agendamientoCitas = new AgendamientoCitas();
                    //using (DataContext db = new DataContext())
                    //{
                    //    agendamientoCitas.id_cita= 0;
                    //    agendamientoCitas.id_agencia= cita.workshopId;
                    //    agendamientoCitas.planned_date= cita.plannedData.plannedDate;
                    //    agendamientoCitas.planned_time= cita.plannedData.plannedTime;

                    //    db.Add(agendamientoCitas);
                    //    db.SaveChanges();
                    //}

                    return result.id;
                }catch(Exception ex)
                {
                    return null;
                }
                
            }
            else
            {
                return null;
            }
        }

        public IActionResult Historial()
        {
            return View();
        }
    }
}