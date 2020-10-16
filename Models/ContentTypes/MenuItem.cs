using System.Linq;

namespace Kentico.Kontent.Statiq.Lumen.Models
{
    public partial class MenuItem
    {
        public Page Page => Content?.OfType<Page>().FirstOrDefault();
    }
}