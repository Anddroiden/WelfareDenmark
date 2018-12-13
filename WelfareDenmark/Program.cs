using System.Net;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace WelfareDenmark {
    public class Program {
        public static void Main(string[] args) {
            CreateWebHostBuilder(args).Build().Run();
        }

        private static IWebHostBuilder CreateWebHostBuilder(string[] args) {
            return WebHost.CreateDefaultBuilder(args)
                .UseIISIntegration()
                .UseKestrel(options => {
                    options.Listen(IPAddress.Any, 443, listenOptions => {
                        var configuration =
                            (IConfiguration) options.ApplicationServices.GetService(typeof(IConfiguration));
                        if (configuration["ASPNETCORE_ENVIRONMENT"] != "Development") {
                            listenOptions.UseHttps("cert.pfx", configuration["certPassword"]);
                        }
                    });
                })
                .UseStartup<Startup>();
        }
    }
}