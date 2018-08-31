using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace TicketPlatform.Web.Helper
{
    public class JsonConfigurationHelper
    {
        public static T GetAppSettings<T>(string key, string path = "appsettings.json") where T : class, new()
        {
            var currentClassDir = Directory.GetCurrentDirectory();
            IConfiguration config = new ConfigurationBuilder()
                .SetBasePath(currentClassDir)
                .Add(new JsonConfigurationSource { Path = path, Optional = false, ReloadOnChange = true })
                .Build();
            var appconfig = new ServiceCollection()
                .AddOptions()
                .Configure<T>(config.GetSection(key))
                .BuildServiceProvider()
                .GetService<IOptions<T>>()
                .Value;
            return appconfig;
        }
    }
    /// <summary>
    /// 实体类
    /// </summary>
    public class ConfigDTO
    {
        public dynamic value { get; set; }
    }

}
