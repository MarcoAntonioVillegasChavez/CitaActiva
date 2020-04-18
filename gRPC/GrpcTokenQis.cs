using CitaActiva.Models;
using mx.autocom.servicio.paquetes.service;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using Serilog;
using System.Threading.Tasks;
using static mx.autocom.servicio.paquetes.service.PaquetesGrpcService;
using Microsoft.Extensions.Options;

namespace CitaActiva.gRPC
{
    public class GrpcTokenQis
    {       
        public async Task<Token> GetTokenQis(string environment)
        {
            PaquetesGrpc paquetesGrpc = new PaquetesGrpc();
            PaquetesGrpcServiceClient grpcService = paquetesGrpc.TokenGrpc();
            TokenQis tokenQis = await grpcService.GetTokenQisAsync(new mx.autocom.servicio.paquetes.service.TokenQisRequest { Environment = environment });
            if (tokenQis != null)
            {
                Token token = new Token {
                    id = Convert.ToInt32( tokenQis.Id),
                    access_token = tokenQis.AccessToken,
                    created_at = Convert.ToDateTime(tokenQis.CreatedAt),
                    expires_in = tokenQis.ExpiresIn,
                    token_type = tokenQis.TokenType
                };

                Log.Error("++ Token Qis ++ " + token.access_token + " " + token.created_at);
                return token;
            }
            else
            {
                return null;
            }
        }
    }
}
