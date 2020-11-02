using Kentico.Kontent.Delivery.Abstractions;
using Kentico.Kontent.Delivery.Urls.QueryParameters;
using Kentico.Kontent.Delivery.Urls.QueryParameters.Filters;
using Kentico.Kontent.Statiq.Lumen.Models;
using Kentico.Kontent.Statiq.Lumen.Models.ViewModels;
using Kontent.Statiq;
using Statiq.Common;
using Statiq.Core;
using Statiq.Razor;
using System.Linq;

namespace Kentico.Kontent.Statiq.Lumen.Pipelines
{
    public class Pages : Pipeline
    {
        public Pages(IDeliveryClient deliveryClient)
        {
            Dependencies.AddRange(nameof(MenuItems), nameof(SiteMetadatas));
            InputModules = new ModuleList{
                new Kontent<MenuItem>(deliveryClient)
                    .WithQuery(new IncludeTotalCountParameter(), new NotEmptyFilter("elements.content")),
                new SetDestination(Config.FromDocument((doc, ctx)  => new NormalizedPath($"pages/{doc.AsKontent<MenuItem>().Slug}/index.html" ))),
            };

            ProcessModules = new ModuleList {
                new MergeContent(new ReadFiles(patterns: "Index.cshtml") ),
                new RenderRazor()
                 .WithModel(Config.FromDocument((document, context) =>
                 {
                    var menuItem = document.AsKontent<MenuItem>();
                    var model = new HomeViewModel(menuItem.Page,
                                    new SidebarViewModel(
                                    context.Outputs.FromPipeline(nameof(MenuItems)).Select(x => x.AsKontent<Menu>()).FirstOrDefault(),
                                    context.Outputs.FromPipeline(nameof(SiteMetadatas)).Select(x => x.AsKontent<SiteMetadata>()).FirstOrDefault(),
                                    false, menuItem.Slug));
                    return model;
                 }
                 ))/*,
                new KontentImageProcessor()*/
            };

            OutputModules = new ModuleList {
                new WriteFiles(),
            };
        }
    }
}