using Kentico.Kontent.Delivery.Abstractions;
using Kentico.Kontent.Delivery.Urls.QueryParameters;
using Kentico.Kontent.Statiq.Lumen.Models;
using Kontent.Statiq;
using Statiq.Common;
using Statiq.Core;
using Statiq.Razor;
using SortOrder = Kontent.Statiq.SortOrder;

namespace Kentico.Kontent.Statiq.Lumen.Pipelines
{
    public class Posts : Pipeline
    {
        public Posts(IDeliveryClient deliveryClient)
        {
            InputModules = new ModuleList{
                new Kontent<Article>(deliveryClient)
                    .OrderBy(Article.DateCodename, SortOrder.Descending)
                    .WithQuery(new DepthParameter(1), new IncludeTotalCountParameter()),
                new SetDestination(Config.FromDocument((doc, ctx)  => new NormalizedPath($"{doc.AsKontent<Article>().Slug}.html" ))),
            };

            ProcessModules = new ModuleList {
                new MergeContent(new ReadFiles(patterns: "Post.cshtml") ),
                new RenderRazor()
                 .WithModel(Config.FromDocument((document, context) => document.AsKontent<Article>()))/*,
                new KontentImageProcessor()*/ // TODO: uncomment
            };

            OutputModules = new ModuleList {
                new WriteFiles(),
            };
        }
    }
}