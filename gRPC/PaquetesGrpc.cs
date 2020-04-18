using Grpc.Core;
using Microsoft.Extensions.Configuration;
using System.IO;
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

        public PaquetesGrpcServiceClient TokenGrpc()
        {
            var builder = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json");

            var configuration = builder.Build();
            string GrpcServerHost = configuration["TokenQis:host"];
            string GrpcServerPort = configuration["TokenQis:port"];

            var channelTarget = $"{GrpcServerHost}:{GrpcServerPort}";
            var channel = new Channel(channelTarget, ChannelCredentials.Insecure);
            PaquetesGrpcServiceClient grpcService = new PaquetesGrpcServiceClient(channel);

            return grpcService;
        }
    }
}
