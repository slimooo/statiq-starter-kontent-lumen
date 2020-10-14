using System;
using System.Collections.Generic;
using System.Text;

namespace Kentico.Kontent.Statiq.Lumen.Models.ViewModels
{
    public class HomeViewModel
    {
        public PagedContent<Article> Articles { get; set; }
        public SidebarViewModel Sidebar { get; set; }
    }
}
