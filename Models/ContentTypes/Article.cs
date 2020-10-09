using Kentico.Kontent.Delivery.Abstractions;
using System.Collections.Generic;
using System.Linq;

namespace Kentico.Kontent.Statiq.Lumen.Models
{
    public partial class Article : IPageMetadata
    {
        public IEnumerable<Tag> TagObjects => Tags.OfType<Tag>();

        public string MetadataTwitterCreator => string.Empty;

        public string UrlPattern => string.Empty;

        public string MetadataMetaKeywords => string.Empty;

        public IEnumerable<IAsset> MetadataTwitterImage => new List<IAsset>();

        public string MetadataOgTitle => string.Empty;

        public IEnumerable<IAsset> MetadataOgImage => new List<IAsset>();

        public string MetadataTwitterSite => string.Empty;

        public string MetadataMetaDescription => string.Empty;

        public string MetadataMetaTitle => string.Empty;

        public string MetadataOgDescription => string.Empty;

        public IEnumerable<IAsset> TeaserImage => new List<IAsset>();

        public IEnumerable<IMultipleChoiceOption> MetadataTwitterCard { get => new List<IMultipleChoiceOption>(); set => new List<IMultipleChoiceOption>(); }
    }
}