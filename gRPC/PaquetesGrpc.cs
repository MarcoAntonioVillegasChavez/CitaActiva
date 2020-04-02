using CitaActiva.Models;
using Grpc.Core;
using Microsoft.Extensions.Configuration;
using mx.autocom.servicio.paquetes.service;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using static mx.autocom.servicio.paquetes.service.PaquetesGrpcService;

namespace CitaActiva.gRPC
{
    public class PaquetesGrpc
    {       
        public PaquetesGrpcServiceClient CargarPaquetesGrpc()
        {
            var builder = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json");

            var configuration = builder.Build();
            string GrpcServerHost = configuration["grpcPaquetesServer:host"];
            string GrpcServerPort = configuration["grpcPaquetesServer:port"];

            var channelTarget = $"{GrpcServerHost}:{GrpcServerPort}";
            var channel = new Channel(channelTarget, ChannelCredentials.Insecure);
            PaquetesGrpcServiceClient grpcService = new PaquetesGrpcServiceClient(channel);

            return grpcService;           
        }
    }
}
