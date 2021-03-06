using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RESTfulAPI.Helpers;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RESTfulAPI.JWTHelper
{
    public class JWTHelper : IJWTHelper
    {
        private readonly IConfiguration configuration;

        public JWTHelper(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        /// <summary>
        /// Class to generate token
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public string GetToken(string email)
        {
            ConfigReader configReader = new ConfigReader(configuration);
            var tokenHandler = new JwtSecurityTokenHandler();
            var signingKey = Encoding.UTF8.GetBytes(configReader.GetSection("JWTSigningKey"));

            var claims = new List<Claim> {
                new Claim(JwtRegisteredClaimNames.Email,email)
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddMinutes(20),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(signingKey), SecurityAlgorithms.HmacSha256),
                Audience = configReader.GetSection("ApplicationAudience"),
                Issuer = configReader.GetSection("ApplicationIssuer")
            };
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            string token = tokenHandler.WriteToken(securityToken);
            return token;
        }

        public TokenValidationParameters GetTokenValidationParameters()
        {
            ConfigReader configReader = new ConfigReader(configuration);
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configReader.GetSection("JWTSigningKey")));
            var tokenValidationParameters = new TokenValidationParameters()
            {
                IssuerSigningKey = signingKey,
                ValidateIssuerSigningKey = true,
                ValidateLifetime = true,
                ValidateIssuer = true,
                ValidateAudience = false,
                ValidIssuer = configReader.GetSection("ApplicationIssuer"),
                ValidAudience = configReader.GetSection("ApplicationAudience"),
                ClockSkew = TimeSpan.Zero
            };

            return tokenValidationParameters;
        }

    }
}
