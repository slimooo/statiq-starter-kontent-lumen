using Kentico.Kontent.Delivery.Abstractions;
using Kentico.Kontent.Delivery.Urls.QueryParameters;
using Kentico.Kontent.Delivery.Urls.QueryParameters.Filters;
using Kentico.Kontent.Statiq.Lumen.Extensions;
using Kentico.Kontent.Statiq.Lumen.Models;
using Kentico.Kontent.Statiq.Lumen.Models.ViewModels;
using Kontent.Statiq;
using Statiq.Common;
using Statiq.Core;
using Statiq.Razor;
using System;
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
                new SetMetadata(nameof(Category), Config.FromDocument((doc, ctx) =>
                {
                    // Add grouping key as metadata
                    return doc.AsKontent<Article>().SelectedCategory.System.Codename;
                })),
                new SetMetadata(nameof(Article.SelectedCategory), Config.FromDocument((doc, ctx) =>
                {
                    // Add some extra metadata to be used later for creating a filename
                    return doc.AsKontent<Article>().SelectedCategory;
                })),
                new GroupDocuments(nameof(Category)), // Group docs by the category name
                new ForEachDocument{ // For each category
                    new FlattenTree(true), // Flatten the tree structure
                    new FilterDocuments(TypedContentExtensions.KontentItemKey), // Keep only Kontent items (exclude the parent group doc)
                    new MergeMetadata {  // Copy metadata from child documents to parent
                        new PaginateDocuments(9) // Create paginatin (docs will reside under a parent doc - a page)
                    }.Reverse(), // Reverse not to copy the metadata in the other direction
                    new SetDestination(Config.FromDocument((doc, ctx) => Filename(doc))), // Set output
                    new MergeContent(new ReadFiles(patterns: "Index.cshtml") ),
                    new RenderRazor()
                     .WithModel(Config.FromDocument((document, context) =>
                     {
                        var category = document.Get<Category>(nameof(Article.SelectedCategory));
                        var model = new HomeViewModel(document.AsPagedKontent<Article>(),
                                        new SidebarViewModel(
                                        context.Outputs.FromPipeline(nameof(Contacts)).Select(x => x.AsKontent<Contact>()),
                                        context.Outputs.FromPipeline(nameof(MenuItems)).Select(x => x.AsKontent<Menu>()).FirstOrDefault(),
                                        context.Outputs.FromPipeline(nameof(Authors)).Select(x => x.AsKontent<Author>()).FirstOrDefault(),
                                        context.Outputs.FromPipeline(nameof(SiteMetadatas)).Select(x => x.AsKontent<SiteMetadata>()).FirstOrDefault(),
                                        false, null), category.Title);
                        return model;
                     }
                     ))
                }
                /*,
                new KontentImageProcessor()*/
            };

            OutputModules = new ModuleList {
                new WriteFiles(),
            };
        }

        private static NormalizedPath Filename(IDocument document)
        {
            var index = document.GetInt(Keys.Index);

            var category = document.Get<Category>(nameof(Article.SelectedCategory));

            return new NormalizedPath($"{category.Slug}{(index > 1 ? index.ToString() : "")}.html");
        }
    }
}