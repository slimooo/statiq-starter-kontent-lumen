namespace Kentico.Kontent.Statiq.Lumen.Models.ViewModels
{
    public class PostViewModel : ViewModelBase
    {
        public Article Article { get; private set; }

        public PostViewModel(Article article, Author author, SiteMetadata metadata) : base(author, metadata)
        {
            Article = article;
        }
    }
}
