using mx.autocom.servicio.paquetes.service;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static mx.autocom.servicio.paquetes.service.PaquetesGrpcService;

namespace CitaActiva.gRPC
{
    public class GrpcCitas
    {
        public async Task<string> CrearCita(ModelsViews.AppointmentResult cita)
        {
            List<string> laboursList = new List<string>();

            foreach (var x in cita.labours)
            {
                laboursList.Add(x.description);
            }

            CitaRequest request = new CitaRequest();
            request.IdAgencia = cita.workshopId.ToString();
            request.NecesitaCarroReemplazo = false;
            request.PlacaVehiculo = cita.vehiclePlate;
            request.NombreContacto = cita.contactName;
            request.ReciveCliente = true;
            request.Email = cita.contactMail;
            request.Kilometraje = cita.mileage;
            request.VehiculoPickup = false;
            request.Telefono = cita.contactPhone;
            request.Fecha = cita.plannedData.plannedDate;
            request.Hora = cita.plannedData.plannedTime;
            request.IdRecepcionista = 1;
            request.LaboursList.AddRange(laboursList);
            request.Comments = cita.comments;
            //request.LaboursList.AddRange(new Google.Protobuf.Collections.RepeatedField<string> {"Servicio de 10000", "Falla Frenos", "Cambiar Focos" });

            PaquetesGrpc paquetesGrpc = new PaquetesGrpc();
            PaquetesGrpcServiceClient grpcService = paquetesGrpc.CargarPaquetesGrpc();
            var result =  await grpcService.CreateCitaQisAsync(request);
            if (result != null)
            {
          
                return result.Id;
            }
            else
            {
                return "Ha ucurrido un error.";
            }
        }

        public async Task<string> ListarAgenciasByIdZona(int id_zona)
        {
            PaquetesGrpc paquetesGrpc = new PaquetesGrpc();
            PaquetesGrpcServiceClient grpcService = paquetesGrpc.CargarPaquetesGrpc();
            Agencias agencias = await grpcService.GetAllAgenciasAsync(new mx.autocom.servicio.paquetes.service.AgenciaRequest { IdZona = id_zona });
            if (agencias != null)
            {
                List<Models.Agencias> agenciasList = new List<Models.Agencias>();
                foreach (Agencia x in agencias.Agencias_)
                {

                    agenciasList.Add(new Models.Agencias
                    {
                        id_agencia = x.IdAgencia,
                        codigo_quis = x.CodigoQis,
                        nombre_agencia = x.NombreAgencia,
                        activa = x.Activa,
                        google_place = x.GooglePlace,
                        place_id = x.PlaceId
                    });
                }
                return JsonConvert.SerializeObject(agenciasList);
            }
            else
            {
                return null;
            }
        }
    }
}
