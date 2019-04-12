using System.IO;
using Microsoft.Extensions.Configuration;

namespace AkkaConsoleBinance.Library {

    public sealed class ConfigurationFileApp : IConfigurationFileApp {

        public ConfigurationFileApp() {
            var builder = new ConfigurationBuilder().SetBasePath(
                Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");

            var configuration = builder.Build();

            BinanceUrl = configuration[$"BinanceUrl"];
            ConnectionString = configuration[$"ConnectionString"];
        }

        public string BinanceUrl { get; }
        public string ConnectionString { get; }
    }
}