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
}