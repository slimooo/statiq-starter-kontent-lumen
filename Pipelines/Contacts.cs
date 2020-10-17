using Kentico.Kontent.Delivery.Abstractions;
using Kentico.Kontent.Statiq.Lumen.Models;

namespace Kentico.Kontent.Statiq.Lumen.Pipelines
{
    public class Contacts : LoadDataPipeLine<Contact>
    {
        public Contacts(IDeliveryClient deliveryClient) : base(deliveryClient)
        {
        }
    }
}