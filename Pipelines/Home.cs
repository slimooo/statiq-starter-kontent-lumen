using Generator.Pipelines;
using Kentico.Kontent.Statiq.Lumen.Models;
using Kontent.Statiq;
using Statiq.Common;
using Statiq.Core;
using Statiq.Razor;
using System;
using System.Collections.Generic;
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

    public static class Extensions
    {
        public static PagedContent<TModel> AsPagedKontent<TModel>(this IDocument document)
        {
            return new PagedContent<TModel>(document);
        }
    }

    public class PagedContent<TContentModel>
    {
        private readonly IDocument _document;
        private readonly Lazy<IReadOnlyList<TContentModel>> _children;
        private readonly Lazy<PagedContent<TContentModel>> _previous;
        private readonly Lazy<PagedContent<TContentModel>> _next;

        public PagedContent(IDocument document)
        {
            _document = document;
            _children = new Lazy<IReadOnlyList<TContentModel>>(() => document.GetChildren().Select(c => c.AsKontent<TContentModel>()).ToArray());

            _previous = new Lazy<PagedContent<TContentModel>>(() =>
            {
                var otherDocument = document.GetDocument(Keys.Previous);
                return otherDocument != null ? new PagedContent<TContentModel>(otherDocument) : null;
            });
            _next = new Lazy<PagedContent<TContentModel>>(() =>
            {
                var otherDocument = document.GetDocument(Keys.Next);
                return otherDocument != null ? new PagedContent<TContentModel>(otherDocument) : null;
            });
        }

        public int Index => _document.GetInt(Keys.Index);
        public int TotalPages => _document.GetInt(Keys.TotalPages);
        public int TotalItems => _document.GetInt(Keys.TotalItems);
        public IReadOnlyList<TContentModel> Items => _children.Value;
        public PagedContent<TContentModel> Previous => _previous.Value;
        public PagedContent<TContentModel> Next => _next.Value;
        public string Url => _document.GetLink();
    }
}
