using System.Collections.Generic;

namespace Kentico.Kontent.Statiq.Lumen.Models.ViewModels
{
    public class SidebarViewModel
    {
        public IEnumerable<Contact> Contacts { get; set; }

        public IEnumerable<MenuItem> Menu { get; set; }

        public Author Author { get; set; }

        public SiteMetadata Metadata { get; set; }
    }
}
