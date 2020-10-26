using Kentico.Kontent.Delivery.Abstractions;
using System.Collections.Generic;
using System.Linq;

namespace Kentico.Kontent.Statiq.Lumen.Models
{
    public partial class Article : IPageMetadata
    {
        public IEnumerable<Tag> TagObjects => Tags.OfType<Tag>();

        public Category SelectedCategory => Category.OfType<Category>().First();
    }
}