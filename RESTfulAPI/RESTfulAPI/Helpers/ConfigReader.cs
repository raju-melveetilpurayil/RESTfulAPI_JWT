using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RESTfulAPI.Helpers
{
    public class ConfigReader
    {
        private readonly IConfiguration configuration;
        public ConfigReader(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        /// <summary>
        /// Read section depend on environment
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetSection(string key)
        {
            string value = string.Empty;
            if (!string.IsNullOrEmpty(key))
            {
                try
                {
                    var environment = configuration.GetSection("ENVIRONMENT").Value;
                    var environmentAppsettings = environment + ":AppSettings";
                    value = configuration.GetSection(environmentAppsettings + ":" + key).Value;
                }
                catch (Exception ex)
                {
                    //Logging can impliment here
                    throw ex;
                }
            }
            return value;

        }

        /// <summary>
        /// Connection string depend on environment
        /// </summary>
        /// <returns></returns>
        public string GetConnectionStringByEnvironment()
        {
            string connectionstring = string.Empty;

            try
            {
                string environment = configuration["ENVIRONMENT"];
                if (!string.IsNullOrEmpty(environment))
                {
                    connectionstring = configuration.GetConnectionString(environment + ":DefaultConnection");
                }
            }
            catch (Exception ex)
            {
                //Logging can impliment here
                throw ex;
            }
            return connectionstring;
        }
    }
}
