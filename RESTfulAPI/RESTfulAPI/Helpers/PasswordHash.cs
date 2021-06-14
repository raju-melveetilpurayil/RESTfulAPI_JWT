using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTfulAPI.Helpers
{
    public class PasswordHash
    {
        private readonly IConfiguration configuration;
        public PasswordHash(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public string GetHash(string password)
        {
            ConfigReader configReader = new ConfigReader(configuration);

            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password,
            salt: Encoding.UTF8.GetBytes(configReader.GetSection("PasswordSalt")),
            prf: KeyDerivationPrf.HMACSHA1,
            iterationCount: 10000,
            numBytesRequested: 256 / 8));
        }
    }
}
