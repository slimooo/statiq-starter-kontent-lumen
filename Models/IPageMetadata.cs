using Kentico.Kontent.Delivery.Abstractions;
using System.Collections.Generic;

namespace Kentico.Kontent.Statiq.Lumen.Models
{
    public interface IPageMetadata
    {
        #region Generic metadata
        public string Title { get; }
        public string Slug { get; }
        public IEnumerable<IAsset> SocialSharingMetadataTeaserImage { get; }
        public string SocialSharingMetadataKeywords { get; }
        public string SocialSharingMetadataDescription { get; set; }
        #endregion

        #region Open graph
        public string SocialSharingMetadataOgTitle { get; }
        public string SocialSharingMetadataOgDescription { get; }
        public IEnumerable<IAsset> SocialSharingMetadataOgImage { get; }
        #endregion

        #region Twitter card
        public string SocialSharingMetadataTwitterCreator { get; }
        public string SocialSharingMetadataTwitterSite { get; }
        public IEnumerable<IAsset> SocialSharingMetadataTwitterImage { get; }
        public IEnumerable<IMultipleChoiceOption> SocialSharingMetadataTwitterCard { get; set; }
        #endregion
    }
}
