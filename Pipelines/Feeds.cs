using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using Kentico.Kontent.Statiq.Lumen.Models;
using Kentico.Kontent.Statiq.Lumen.Modules;
using Kontent.Statiq;
using Microsoft.Extensions.Logging;
using Statiq.Common;
using Statiq.Core;
using Statiq.Feeds;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Kentico.Kontent.Statiq.Lumen.Pipelines
{
    public class Feeds : Pipeline
    {
        public Feeds()
        {
            Dependencies.Add(nameof(Posts));
            ProcessModules = new ModuleList(
                // pull documents from other pipelines
                new ReplaceDocuments(Dependencies.ToArray()),

                // Set metadata for the feeds module
                new SetMetaDataItems(
                    async (input, context) =>
                    {
                        var post = input.AsKontent<Article>();
                        var html = await ParseHtml(input, context);
                        var article = html?.GetElementsByTagName("article").FirstOrDefault()?.InnerHtml ?? "";

                        return new MetadataItems
                        {
                            {FeedKeys.Title, post.Title},
                            {FeedKeys.Content, post.MetadataMetaDescription},
                            {FeedKeys.Description, post.MetadataMetaDescription},
                            {FeedKeys.Image, post.MetadataOgImage.FirstOrDefault()?.Url},// TODO: make that a local image!
                            {FeedKeys.Published, post.Date},
                            {FeedKeys.Content, article}
                        };
                    }),
                new GenerateFeeds()
                    .WithFeedTitle("Lumen Blog")
                    .WithFeedCopyright($"{DateTime.Today.Year}")
            );
            OutputModules = new ModuleList(
                new WriteFiles()
            );
        }

        private static async Task<IHtmlDocument?> ParseHtml(IDocument document, IExecutionContext context)
        {
            var parser = new HtmlParser();
            try
            {
                await using var stream = document.GetContentStream();
                return await parser.ParseDocumentAsync(stream);
            }
            catch (Exception ex)
            {
                context.LogWarning("Exception while parsing HTML for {0}: {1}", document.ToSafeDisplayString(), ex.Message);
            }

            return null;
        }
    }
}