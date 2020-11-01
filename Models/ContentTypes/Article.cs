using Kentico.Kontent.Delivery.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Kentico.Kontent.Statiq.Lumen.Models
{
    public partial class Article : ITwitterCard, IOpenGraphArticle
    {
        public IEnumerable<Tag> TagObjects => Tags.OfType<Tag>();

        public Category SelectedCategory => Category.OfType<Category>().First();

        public IEnumerable<IAsset> TeaserImage => SocialSharingMetadataTeaserImage;

        public string Keywords => SocialSharingMetadataKeywords;

        public string OgTitle => SocialSharingMetadataOgTitle.Cascade(Title);

        public string OgDescription => SocialSharingMetadataDescription.Cascade(Description);

        public IEnumerable<IAsset> OgImage => SocialSharingMetadataOgImage;

        public string TwitterCreator => SocialSharingMetadataTwitterCreator;

        public string TwitterSite => SocialSharingMetadataTwitterSite;

        public IAsset TwitterImage => SocialSharingMetadataTwitterImage?.FirstOrDefault().Cascade(OgImage?.FirstOrDefault());

        public string TwitterCard => SocialSharingMetadataTwitterCard?.FirstOrDefault()?.Codename;

        public string TwitterTitle => OgTitle;

        public string TwitterDescription => OgDescription;

        public DateTime OgPublishedTime => System.LastModified; // TODO: new field

        public DateTime OgModifiedTime => System.LastModified;

        public IEnumerable<string> OgAuthor => new List<string> { "John Doe" }; // TODO: new field

        public string OgSection => SelectedCategory.Title;

        public IEnumerable<string> OgTag => TagObjects.Select(t => t.Title);
    }
}