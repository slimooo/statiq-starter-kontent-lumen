using Generator.Pipelines;
using Kentico.Kontent.Statiq.Lumen.Extensions;
using Kentico.Kontent.Statiq.Lumen.Models;
using Statiq.Common;
using Statiq.Core;
using Statiq.Razor;
using System.Linq;

namespace Kentico.Kontent.Statiq.Lumen.Pipelines
{
    public class Home : Pipeline
    {
        public Home()
        {
            Dependencies.Add(nameof(Posts));
            ProcessModules = new ModuleList(
                // pull documents from other pipelines
                new ReplaceDocuments(Dependencies.ToArray()),
                new PaginateDocuments(9),
                new SetDestination(Config.FromDocument((doc, ctx) => Filename(doc))),
                new MergeContent(new ReadFiles(patterns: "Index.cshtml")),
                new RenderRazor()
                    .WithModel(Config.FromDocument((document, context) => document.AsPagedKontent<Article>()))/*,
                new KontentImageProcessor()*/ //TODO: remove

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
