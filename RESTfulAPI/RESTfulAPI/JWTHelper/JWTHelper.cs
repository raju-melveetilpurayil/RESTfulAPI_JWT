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
        private readonly IHttpContextAccessor httpContextAccessor;

        public JWTHelper(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            this.configuration = configuration;
            this.httpContextAccessor = httpContextAccessor;
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


            //var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            //var authenticationProperties = new AuthenticationProperties();
            //authenticationProperties.StoreTokens(new[] {
            //    new AuthenticationToken()
            //    {
            //        Name="JWT",
            //        Value=token
            //    }
            //});

            return token;
        }
    }
}
