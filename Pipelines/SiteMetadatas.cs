using Kentico.Kontent.Delivery.Abstractions;
using Kentico.Kontent.Statiq.Lumen.Models;

namespace Kentico.Kontent.Statiq.Lumen.Pipelines
{
    public class SiteMetadatas : LoadDataPipeLine<SiteMetadata>
    {
        public SiteMetadatas(IDeliveryClient deliveryClient) : base(deliveryClient)
        {
        }
    }
}