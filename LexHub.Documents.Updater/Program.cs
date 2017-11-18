using Autofac.Extensions.DependencyInjection;
using LexHub.Documents.Updater.DI;
using LexHub.Documents.Updater.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.IO;

namespace LexHub.Documents.Updater
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = ConfigureServices();
            var loggerFactory = serviceProvider.GetService<ILoggerFactory>();
            var logger = loggerFactory.CreateLogger<Program>();

            var updater = serviceProvider.GetService<Updater>();

            updater.Update();


            logger.LogInformation("Processing finished.");
            Console.ReadKey();
        }

        private static AutofacServiceProvider ConfigureServices()
        {
            var serviceCollection = new ServiceCollection()
                           .AddLogging(builder =>
                           {
                               builder.AddConsole();
                               builder.AddDebug();
                           }).
                           AddOptions();
            var configuration = BuildConfiguration();

            serviceCollection.Configure<LexDocuments>(configuration.GetSection("LexDocuments"));
            var serviceProvider = DependencyInjectionBuilder.Build(serviceCollection);
            return serviceProvider;
        }

        private static IConfiguration BuildConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            return builder.Build();
        }
    }
}
