using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TechnicalRadiation.Services
{
    public class Authentication : IAuthentication
    {
        public IConfiguration Config { get; }
        public Authentication(IConfiguration config)
        {
            Config = config;
        }
        public bool Authenticate(string secret)
        {
            var secretKey = Config.GetValue<string>("secretKey");
            if (secretKey == secret)
            {
                return true;
            }

            return false;
        }
    }
}
