using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Configurations
{
    public class AppSettingsHandler
    {
        private string _filename;
        private AppSettingsHandler _config;

        public AppSettingsHandler(string filename)
        {
            _filename = filename;
            _config = GetAppSettings();
        }

        public AppSettingsHandler GetAppSettings()
        {
            var config = new ConfigurationBuilder()
               .SetBasePath(AppContext.BaseDirectory)
               .AddJsonFile(_filename, false, true)
               .Build();

            return config.GetSection("App").Get<AppSettingsHandler>();
        }
    }

}
