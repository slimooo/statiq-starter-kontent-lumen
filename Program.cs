using Kentico.Kontent.Delivery.Abstractions;
using Kentico.Kontent.Delivery.Extensions;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Statiq.App;
using Statiq.Common;
using Kentico.Kontent.Statiq.Lumen.Models;
using Microsoft.Extensions.Configuration;

namespace Generator
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
                })
                .RunAsync();
    }
}
