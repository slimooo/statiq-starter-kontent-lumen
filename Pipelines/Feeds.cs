//using Statiq.Common;
//using Statiq.Core;
//using Statiq.Feeds;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace Generator.Pipelines
//{
//    public class Feeds : Pipeline
//    {
//        public Feeds()
//        {
//            Dependencies.Add(nameof(Posts));
//            ProcessModules = new ModuleList(
//                // pull documents from other pipelines
//                new ReplaceDocuments(Dependencies.ToArray()),
                
//                // Set metadata for the feeds module
//                //new SetMetaDataItems(
//                //    async (input, context) =>
//                //    {
//                //        var post = input.AsKontent<Article>();
                        
//                //        var html = await input.ParseHtmlAsync(context);
//                //        var article = html.GetElementsByTagName("article").FirstOrDefault()?.InnerHtml;

//                //        return new MetadataItems
//                //        {
//                //            {FeedKeys.Title, post.Title},
//                //            {FeedKeys.Content, post.MetadataMetaDescription},
//                //            {FeedKeys.Description, post.MetadataMetaDescription},
//                //            {FeedKeys.Image, post.MetadataOgImage.FirstOrDefault()?.Url},// TODO: make that a local image!
//                //            {FeedKeys.Published, post.PostDate},
//                //            {FeedKeys.Content, article}
//                //        };
//                //   }),
//                new GenerateFeeds()
//                    .WithFeedTitle("Lumen Blog")
//                    .WithFeedCopyright($"{DateTime.Today.Year}")
//            );
//            OutputModules = new ModuleList(
//                new WriteFiles()
//            );
//        }
//    }

//    //public class SetMetaDataItems : Module
//    //{
//    //    private readonly Func<IDocument, IExecutionContext, Task<MetadataItems>> _getMetadata;

//    //    public SetMetaDataItems(Func<IDocument, IExecutionContext, Task<MetadataItems>> getMetadata)
//    //    {
//    //        _getMetadata = getMetadata;
//    //    }

//    //    protected override async Task<IEnumerable<IDocument>> ExecuteInputAsync(IDocument input, IExecutionContext context)
//    //    {
//    //        var metadata = await _getMetadata(input, context);

//    //        return metadata == null
//    //            ? input.Yield()
//    //            : input.Clone(metadata).Yield();
//    //    }
//    //}
//}