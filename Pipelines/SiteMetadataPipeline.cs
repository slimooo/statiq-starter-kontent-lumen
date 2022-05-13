using Kentico.Kontent.Delivery.Abstractions;
using Kentico.Kontent.Urls.Delivery.QueryParameters;
using Kentico.Kontent.Statiq.Lumen.Models;

namespace Kentico.Kontent.Statiq.Lumen.Pipelines
{
    public class SiteMetadataPipeline : LoadDataPipeLine<SiteMetadata>
    {
        public SiteMetadataPipeline(IDeliveryClient deliveryClient) : base(deliveryClient, new DepthParameter(2))
        {
        }
    }
}