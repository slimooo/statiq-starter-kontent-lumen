namespace Kentico.Kontent.Statiq.Lumen.Models.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        public Page Page { get; private set; }

        public PagedContent<Article> Articles { get; private set; }

        public SidebarViewModel Sidebar { get; private set; }

        public HomeViewModel(PagedContent<Article> articles, SidebarViewModel sidebar) : base(sidebar.Author, sidebar.Metadata)
        {
            Articles = articles;
            Sidebar = sidebar;
        }

        public HomeViewModel(Page page, SidebarViewModel sidebar) : base(sidebar.Author, sidebar.Metadata)
        {
            Page = page;
            Sidebar = sidebar;
        }
    }
}
