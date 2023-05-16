using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace DataAccess.Infrastructure
{
    public class AppSettings
    {
        public string Secret { get; set; }
  


            public static string  getTokenKey(string key)
            {
                IConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
                string path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
                configurationBuilder.AddJsonFile(path, false);
                string tokenKey = configurationBuilder.Build().GetSection(key).Value;
               return tokenKey;
            }


        public static double getTokenTime(string key)
        {
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
            string path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            configurationBuilder.AddJsonFile(path, false);
            string tokentime = configurationBuilder.Build().GetSection(key).Value;
            return Convert.ToDouble(tokentime);
        }

        public static string  getkeyValue(string key)
        {
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
            string path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            configurationBuilder.AddJsonFile(path, false);
            string val = configurationBuilder.Build().GetSection(key).Value;
            return val;
        }

    }
}