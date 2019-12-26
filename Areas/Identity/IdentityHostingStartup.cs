using Microsoft.AspNetCore.Hosting;
[assembly: HostingStartup(typeof(SmartSale.Areas.Identity.IdentityHostingStartup))]
namespace SmartSale.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => { });
        }
    }
}