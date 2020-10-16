using Kentico.Kontent.Delivery.Abstractions;
using Kentico.Kontent.Delivery.Urls.QueryParameters;
using Kentico.Kontent.Statiq.Lumen.Models;
using Kentico.Kontent.Statiq.Lumen.Models.ViewModels;
using Kontent.Statiq;
using Statiq.Common;
using Statiq.Core;
using Statiq.Razor;
using System.Linq;

namespace Kentico.Kontent.Statiq.Lumen.Pipelines
{
    public class Posts : Pipeline
    {
        public Posts(IDeliveryClient deliveryClient)
        {
            Dependencies.AddRange(nameof(Authors), nameof(SiteMetadatas));
            InputModules = new ModuleList{
                new Kontent<Article>(deliveryClient)
                    .OrderBy(Article.DateCodename, SortOrder.Descending)
                    .WithQuery(new DepthParameter(1), new IncludeTotalCountParameter()),
                new SetDestination(Config.FromDocument((doc, ctx)  => new NormalizedPath($"posts/{doc.AsKontent<Article>().Slug}.html" ))),
            };

            ProcessModules = new ModuleList {
                new MergeContent(new ReadFiles(patterns: "Post.cshtml") ),
                new RenderRazor()
                 .WithModel(Config.FromDocument((document, context) =>
                 new PostViewModel(document.AsKontent<Article>(),
                               context.Outputs.FromPipeline(nameof(Authors)).Select(x => x.AsKontent<Author>()).FirstOrDefault(),
                               context.Outputs.FromPipeline(nameof(SiteMetadatas)).Select(x => x.AsKontent<SiteMetadata>()).FirstOrDefault()))),
                new KontentImageProcessor()
            };

            OutputModules = new ModuleList {
                new WriteFiles(),
            };
        }
    }
}