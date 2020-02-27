using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet_g36.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace dotnet_g36
{
    public class Program
    {
        public static void Main(string[] args)
        {
           /*Dit staat in de slides maar niet in vb examen 
            *
            * using (ApplicationDbContext context = new ApplicationDbContext())
            {
                new ItLabDataInitializer(context).initializeData();
                Console.WriteLine("Database created");
            }*/

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
