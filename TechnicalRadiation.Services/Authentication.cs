using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechnicalRadiation.Models;
using TechnicalRadiation.Models.Dtos;
using TechnicalRadiation.Models.Entities;
using TechnicalRadiation.Repositories.Data;

namespace TechnicalRadiation.Services
{
    public class Authentication : IAuthentication

    {
        public IConfiguration Config { get;  }
        public Authentication(IConfiguration config)
        {
          this.Config = config;

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
