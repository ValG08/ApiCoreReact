using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace TestWebApiCore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateHostBuilder(string[] args) =>
               WebHost.CreateDefaultBuilder(args)
                   .ConfigureAppConfiguration((context, config) =>
                   {
                       config.AddEnvironmentVariables();              
                   })
                   .UseKestrel(c =>
                   {
                       c.ConfigureHttpsDefaults(opt =>
                       {
                           opt.SslProtocols = System.Security.Authentication.SslProtocols.Tls12;
                       });
                   })
                   .UseStartup<Startup>();
    }
}
