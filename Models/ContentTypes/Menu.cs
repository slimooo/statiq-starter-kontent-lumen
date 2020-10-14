using System.Collections.Generic;
using System.Linq;

namespace Kentico.Kontent.Statiq.Lumen.Models
{
    public partial class Menu
    {
        public IEnumerable<MenuItem> MenuItemsTyped => MenuItems.OfType<MenuItem>();
    }
}