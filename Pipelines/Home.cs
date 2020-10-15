using Kentico.Kontent.Delivery.Abstractions;
using Kentico.Kontent.Delivery.Urls.QueryParameters;
using Kentico.Kontent.Statiq.Lumen.Extensions;
using Kentico.Kontent.Statiq.Lumen.Models;
using Kentico.Kontent.Statiq.Lumen.Models.ViewModels;
using Kontent.Statiq;
using Statiq.Common;
using Statiq.Core;
using Statiq.Razor;
using System.Linq;

namespace Kentico.Kontent.Statiq.Lumen.Pipelines
{
    public class Home : Pipeline
    {
        public Home(IDeliveryClient deliveryClient)
        {
            Dependencies.AddRange(nameof(Posts), nameof(MenuItems), nameof(Contacts), nameof(Authors), nameof(SiteMetadatas));
            ProcessModules = new ModuleList(
                // pull documents from other pipelines
                new ReplaceDocuments(nameof(Posts)),//Dependencies.ToArray()),
                new PaginateDocuments(9),
                new SetDestination(Config.FromDocument((doc, ctx) => Filename(doc))),
                new MergeContent(new ReadFiles("Index.cshtml")),
                new RenderRazor()
                    .WithModel(Config.FromDocument((document, context) => 
                    new HomeViewModel() { 
                        Articles = document.AsPagedKontent<Article>() ,
                        Sidebar = new SidebarViewModel
                        {
                            Menu = context.Outputs.FromPipeline(nameof(MenuItems)).Select(x => x.AsKontent<Menu>()).FirstOrDefault(),
                            Contacts = context.Outputs.FromPipeline(nameof(Contacts)).Select(x => x.AsKontent<Contact>()),
                            Author = context.Outputs.FromPipeline(nameof(Authors)).Select(x => x.AsKontent<Author>()).FirstOrDefault(),
                            Metadata = context.Outputs.FromPipeline(nameof(SiteMetadatas)).Select(x => x.AsKontent<SiteMetadata>()).FirstOrDefault(),
                            IsIndex = true
                        }
                    })
                    ),
                new KontentImageProcessor()

            );

            OutputModules = new ModuleList {
                new WriteFiles(),
            };
        }

        private static NormalizedPath Filename(IDocument document)
        {
            var index = document.GetInt(Keys.Index);

            return new NormalizedPath($"index{(index > 1 ? index.ToString() : "")}.html");
        }
    }
}
