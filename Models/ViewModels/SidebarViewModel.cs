using System.Collections.Generic;
using System.Linq;

namespace Kentico.Kontent.Statiq.Lumen.Models.ViewModels
{
    public class SidebarViewModel : ViewModelBase
    {
        public IEnumerable<Contact> Contacts { get; private set; }

        public Menu Menu { get; private set; }

        public string ActiveMenuItem { get; private set; }

        public bool IsIndex { get; private set; }

        public SidebarViewModel(Menu menu, SiteMetadata metadata, bool isIndex, string activeMenuItem) : base(metadata)
        {
            Contacts = metadata.SiteAuthor.Contacts.OfType<Contact>();
            Menu = menu;
            IsIndex = isIndex;
            ActiveMenuItem = activeMenuItem;
        }
    }
}
