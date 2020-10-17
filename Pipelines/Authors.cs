using Kentico.Kontent.Delivery.Abstractions;
using Kentico.Kontent.Statiq.Lumen.Models;

namespace Kentico.Kontent.Statiq.Lumen.Pipelines
{
    public class Authors : LoadDataPipeLine<Author>
    {
        public Authors(IDeliveryClient deliveryClient) : base(deliveryClient)
        {
        }
    }
}