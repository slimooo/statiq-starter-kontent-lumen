using Kentico.Kontent.Delivery.Abstractions;
using Kentico.Kontent.Statiq.Lumen.Extensions;
using Kentico.Kontent.Statiq.Lumen.Models;
using Kentico.Kontent.Statiq.Lumen.Models.ViewModels;
using Kontent.Statiq;
using Statiq.Common;
using Statiq.Core;
using Statiq.Razor;
using System.Collections.Generic;
using System.Linq;

namespace Kentico.Kontent.Statiq.Lumen.Pipelines
{
    public class Categories : Pipeline
    {
        public Categories(IDeliveryClient deliveryClient)
        {
            Dependencies.AddRange(nameof(Posts), nameof(MenuItems), nameof(Contacts), nameof(Authors), nameof(SiteMetadatas));

            ProcessModules = new ModuleList {
                new ReplaceDocuments(nameof(Posts)), // Get docs from a different pipeline
                new GroupDocuments(nameof(Category)), // Group docs by the category name
                new SetMetadata(nameof(Article.SelectedCategory), Config.FromDocument(doc => doc.GetChildren().FirstOrDefault().Get<Category>(nameof(Article.SelectedCategory)))), // Copy group metadata to the group parent page
                new ForEachDocument{ // For each category
                    new ExecuteConfig(Config.FromDocument(groupDoc => new ModuleList
                    {
                        new ReplaceDocuments(Config.FromDocument<IEnumerable<IDocument>>(doc => doc.GetChildren())), // Remove the group parent page (to be able to apply Pagination)
                        new PaginateDocuments(4), // Create pagination (docs will reside under a parent doc - a page)
                        new MergeContent(new ReadFiles(patterns: "Index.cshtml")),
                        new RenderRazor()
                            .WithModel(Config.FromDocument((document, context) =>
                            {
                                var category = groupDoc.Get<Category>(nameof(Article.SelectedCategory));
                                var model = new HomeViewModel(document.AsPagedKontent<Article>(),
                                                new SidebarViewModel(
                                                context.Outputs.FromPipeline(nameof(Contacts)).Select(x => x.AsKontent<Contact>()),
                                                context.Outputs.FromPipeline(nameof(MenuItems)).Select(x => x.AsKontent<Menu>()).FirstOrDefault(),
                                                context.Outputs.FromPipeline(nameof(Authors)).Select(x => x.AsKontent<Author>()).FirstOrDefault(),
                                                context.Outputs.FromPipeline(nameof(SiteMetadatas)).Select(x => x.AsKontent<SiteMetadata>()).FirstOrDefault(),
                                                false, null), category.Title);
                                return model;
                            }
                            )),
                        new SetDestination(Config.FromDocument((doc, ctx) => Filename(doc, groupDoc))), // Set output
                    }))
                }

                /*,
                new KontentImageProcessor()*/
            };

            OutputModules = new ModuleList {
                new WriteFiles(),
            };
        }

        private static NormalizedPath Filename(IDocument page, IDocument group)
        {
            var index = page.GetInt(Keys.Index);
            var category = group.Get<Category>(nameof(Article.SelectedCategory)).Slug;
            return new NormalizedPath($"category/{category}/{(index > 1 ? $"{index}/" : "")}index.html");
        }
    }
}