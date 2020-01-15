using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ShareMe.DAL.Context;
using ShareMe.DAL.UnitOfWork;

namespace ShareMe
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args)
                .Build()
                .MigrateDatabase()
                .Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }

    public static class MigrationManager
    {
        public static IUnitOfWork UnitOfWorkInstance { get; private set; }

        public static IHost MigrateDatabase(this IHost webHost)
        {
            using (var scope = webHost.Services.CreateScope())
            {
                UnitOfWorkInstance = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
                var appContext = scope.ServiceProvider.GetRequiredService<ShareMeContext>();
                {
                    try
                    {
                        var migrator = appContext.GetInfrastructure().GetService<IMigrator>();
                        migrator.Migrate();
                    }
                    catch (Exception exception)
                    {
                        Console.WriteLine(exception.Message);
                        throw;
                    }
                }
            }
            return webHost;
        }
    }
}

