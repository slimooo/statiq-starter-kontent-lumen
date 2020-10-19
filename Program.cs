using Kentico.Kontent.Delivery.Abstractions;
using Kentico.Kontent.Delivery.Extensions;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Statiq.App;
using Statiq.Common;
using Kentico.Kontent.Statiq.Lumen.Models;
using Microsoft.Extensions.Configuration;
using Statiq.Web;

namespace Kentico.Kontent.Statiq.Lumen
{
    public static class Program
    {
        public static async Task<int> Main(string[] args) =>
            await Bootstrapper
                .Factory
                .CreateDefault(args)
                .ConfigureServices((services, settings) =>
                {
                    services.AddSingleton<ITypeProvider, CustomTypeProvider>();
                    services.AddDeliveryClient((IConfiguration)settings);
                    //services.Configure<RazorViewEngineOptions>(o =>
                    //{
                    //    o.ViewLocationFormats.Add("/input/{1}/{0}" + RazorViewEngine.ViewExtension);
                    //    o.ViewLocationFormats.Add("/input/Shared/{0}" + RazorViewEngine.ViewExtension);
                    //});
                })
                .AddHostingCommands()
                .RunAsync();
    }
}
