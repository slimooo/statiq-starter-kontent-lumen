using System.Collections.Generic;

namespace Kentico.Kontent.Statiq.Lumen.Models.ViewModels
{
    public class SidebarViewModel : ViewModelBase
    {
        public IEnumerable<Contact> Contacts { get; private set; }

        public Menu Menu { get; private set; }

        public string ActiveMenuItem { get; private set; }

        public bool IsIndex { get; private set; }

        public SidebarViewModel(IEnumerable<Contact> contacts, Menu menu, Author author, SiteMetadata metadata, bool isIndex, string activeMenuItem) : base(author, metadata)
        {
            Contacts = contacts;
            Menu = menu;
            IsIndex = isIndex;
            ActiveMenuItem = activeMenuItem;
        }
    }
}
