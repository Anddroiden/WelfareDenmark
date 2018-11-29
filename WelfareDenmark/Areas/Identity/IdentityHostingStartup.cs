using Microsoft.AspNetCore.Hosting;
using WelfareDenmark.Areas.Identity;

[assembly: HostingStartup(typeof(IdentityHostingStartup))]

namespace WelfareDenmark.Areas.Identity {
    public class IdentityHostingStartup : IHostingStartup {
        public void Configure(IWebHostBuilder builder) {
            builder.ConfigureServices((context, services) => { });
        }
    }
}