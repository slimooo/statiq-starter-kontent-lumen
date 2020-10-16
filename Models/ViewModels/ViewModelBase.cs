namespace Kentico.Kontent.Statiq.Lumen.Models.ViewModels
{
    public abstract class ViewModelBase
    {
        protected ViewModelBase(Author author, SiteMetadata metadata)
        {
            Author = author;
            Metadata = metadata;
        }

        public Author Author { get; set; }

        public SiteMetadata Metadata { get; set; }
    }
}
