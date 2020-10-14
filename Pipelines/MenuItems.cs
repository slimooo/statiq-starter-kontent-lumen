using Kentico.Kontent.Delivery.Abstractions;
using Kentico.Kontent.Statiq.Lumen.Models;

namespace Kentico.Kontent.Statiq.Lumen.Pipelines
{
    public class MenuItems : LoadDataPipeLine<Menu>
    {
        public MenuItems(IDeliveryClient deliveryClient) : base(deliveryClient)
        {
        }
    }

    public class Contacts : LoadDataPipeLine<Contact>
    {
        public Contacts(IDeliveryClient deliveryClient) : base(deliveryClient)
        {
        }
    }

    public class Authors : LoadDataPipeLine<Author>
    {
        public Authors(IDeliveryClient deliveryClient) : base(deliveryClient)
        {
        }
    }

    public class SiteMetadatas : LoadDataPipeLine<SiteMetadata>
    {
        public SiteMetadatas(IDeliveryClient deliveryClient) : base(deliveryClient)
        {
        }
    }
}