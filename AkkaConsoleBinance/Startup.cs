using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AkkaConsoleBinance.Library;
using AkkaConsoleBinance.Repository;
using BinanceCryptoCurrency.Processor;
using BinanceCryptoCurrency.Utility;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace AkkaConsoleBinance
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var configurationFileApp = new ConfigurationFileApp();
            var httpUtilityTool = new HttpUtilityTool();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddSingleton<IBinanceProcessor>(
                new BinanceProcessor(new Uri(configurationFileApp.BinanceUrl), httpUtilityTool));
            services.AddSingleton<TickerRepository>(new TickerRepository(configurationFileApp.ConnectionString));
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
