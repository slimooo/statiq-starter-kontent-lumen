using Kentico.Kontent.Delivery.Abstractions;
using Statiq.Common;
using System.Linq;

namespace Kentico.Kontent.Statiq.Lumen.Models
{
    public class SocialSharingMetadata
    {
        public SocialSharingMetadata(IPageMetadata site, IPageMetadata page)
        {
            Title = page.SocialSharingMetadataOgTitle.Cascade(page.SocialSharingMetadataOgTitle).Cascade(page.Title);
            Url = page.Slug; //TODO: IExecutionContext.Current.GetLink()
            TwitterCard = page.SocialSharingMetadataTwitterCard.FirstOrDefault()?.Codename ?? "summary_large_image";
            Description = page.SocialSharingMetadataOgDescription
                .Cascade(page.SocialSharingMetadataDescription);
            Image = page.SocialSharingMetadataOgImage.FirstOrDefault() ?? site?.SocialSharingMetadataOgImage.FirstOrDefault();
            TwitterSite = page.SocialSharingMetadataTwitterSite
                .Cascade(site?.SocialSharingMetadataTwitterSite);
            TwitterCreator = page.SocialSharingMetadataTwitterCreator
                .Cascade(site?.SocialSharingMetadataTwitterCreator);
            TwitterImage = page.SocialSharingMetadataTwitterImage.FirstOrDefault() ?? Image;
        }

        public string Url { get; }
        public string Title { get; }
        public string Description { get; }
        public IAsset Image { get; }
        public IAsset TwitterImage { get; }
        public string TwitterCreator { get; }
        public string TwitterSite { get; }
        public string TwitterCard { get; set; }
    }
}