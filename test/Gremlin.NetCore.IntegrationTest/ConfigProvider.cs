using System.IO;
using Microsoft.Extensions.Configuration;

namespace Gremlin.NetCore.IntegrationTest
{
    public static class ConfigProvider
    {
        public static IConfiguration Configuration { get; private set; }

        static ConfigProvider()
        {
            Configuration = GetConfig();
        }

        static IConfiguration GetConfig()
        {
            var configFile = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            var builder = new ConfigurationBuilder()
                .AddJsonFile(configFile, optional: false, reloadOnChange: false);

            return builder.Build();
        }
    }
}