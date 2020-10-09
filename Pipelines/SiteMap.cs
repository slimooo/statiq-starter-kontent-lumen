using Kentico.Kontent.Statiq.Lumen.Pipelines;
using Kontent.Statiq;
using Statiq.Common;
using Statiq.Core;
using System;
using System.Linq;

namespace Generator.Pipelines
{
    public class SiteMap : Pipeline
    {
        public SiteMap()
        {
            Dependencies.AddRange(nameof(Posts), nameof(Home));
            ProcessModules = new ModuleList(
                // pull documents from other pipelines
                new ReplaceDocuments(Dependencies.ToArray()),
                new SetMetadata(Keys.SitemapItem, Config.FromDocument<SitemapItem>((doc, ctx) =>
                {
                    var siteMapItem = new SitemapItem(doc.Destination.FullPath)
                    {
                        LastModUtc = doc.Get<DateTime?>(KontentKeys.System.LastModified, null)
                    };

                    if (!siteMapItem.LastModUtc.HasValue)
                    {
                        siteMapItem.LastModUtc = DateTime.UtcNow;
                        siteMapItem.ChangeFrequency = SitemapChangeFrequency.Weekly;
                    }
                    else
                    {
                        siteMapItem.ChangeFrequency = SitemapChangeFrequency.Monthly;
                    }

                    return siteMapItem;
                })),

                new GenerateSitemap()
            );

            OutputModules = new ModuleList {
                new WriteFiles(),
            };
        }
    }
}
