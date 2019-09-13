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
    public class Authorization : IAuthorization

    {
        public IConfiguration Config { get;  }
        public Authorization(IConfiguration config)
        {
          this.Config = config;
        }

        public bool Authorization(string secret)
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
