using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CitaActiva.Models;
using CitaActiva.ModelsViews;
using CitaActiva.Services;
using Microsoft.AspNetCore.Mvc;

namespace CitaActiva.Controllers
{
    public class TokenController : Controller
    {
        private DataContext db = new DataContext();
        public Token ObtenerToken()
        {
            Token token = new Token();
            TokenService tokenService = new TokenService();
            try
            {
                var tokens = from t in db.Token
                        orderby t.createdAt descending
                        select new
                        {
                            t.id,
                            t.token_type,
                            t.access_token,
                            t.expires_in,
                            t.createdAt
                        };
                var tokenList = tokens.ToList();

                token.id = tokenList[0].id;
                token.token_type = tokenList[0].token_type;
                token.access_token = tokenList[0].access_token;
                token.expires_in = tokenList[0].expires_in;
                token.createdAt = tokenList[0].createdAt;

                DateTime today = Convert.ToDateTime(DateTime.Now.ToString("G"));
                DateTime fechaToken = token.createdAt;

                TimeSpan result = today.Subtract(fechaToken);
                TimeSpan tokenTime = TimeSpan.FromMinutes(Convert.ToInt32(token.expires_in) / 60);

                if (result >= tokenTime)
                {
                    token = tokenService.ObtenerToken();
                    token.createdAt = Convert.ToDateTime(DateTime.Now.ToString("G"));

                    db.Token.Add(token);
                    db.SaveChanges();
                }
                
                return token;

            }catch(Exception ex)
            {
                token = tokenService.ObtenerToken();
                token.createdAt = Convert.ToDateTime(DateTime.Now.ToString("G"));

                db.Token.Add(token);
                db.SaveChanges();

                return token;
            }

        }
    }
}