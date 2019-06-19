using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Lobster.Gateway
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        //public static IWebHostBuilder CreateWebHostBuilder(string[] args, IConfiguration config) =>
        //    WebHost.CreateDefaultBuilder(args)
        //    .UseKestrel()
        //    .ConfigureAppConfiguration(conbuilder =>
        //    {
        //        conbuilder.AddJsonFile("appsettings.json");
        //        conbuilder.AddJsonFile("configuration.json");//加载网关配置文件
        //    })
        //    .UseUrls(config["server.urls"])
        //    .UseStartup<Startup>();


        public static IWebHost BuildWebHost(string[] args) =>
          WebHost.CreateDefaultBuilder(args)
             .UseKestrel()
             .ConfigureAppConfiguration(conbuilder =>
             {
                 //conbuilder.AddJsonFile("appsettings.json");
                 conbuilder.AddJsonFile("configuration.json");//加载网关配置文件
             })
              .UseStartup<Startup>()
              .Build();
    }
}
