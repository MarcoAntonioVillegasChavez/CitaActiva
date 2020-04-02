using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CitaActiva.Services
{
    public class CryptographyService
    {
        public string crypt(string pass)
        {
            using (var md5 = MD5.Create())
            {
                var result = md5.ComputeHash(Encoding.ASCII.GetBytes(pass));
                return Encoding.ASCII.GetString(result);
            }
        }
    }
}
