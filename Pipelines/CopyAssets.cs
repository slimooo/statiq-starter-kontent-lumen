using Statiq.Common;
using Statiq.Core;

namespace Kentico.Kontent.Statiq.Lumen.Pipelines
{
    public class CopyAssets : Pipeline
    {
        public CopyAssets()
        {
            var basePath = "assets";
            InputModules = new ModuleList
            {
                //new CopyFiles("./assets/{css,fonts,js,images}/**/*", "*.{png,ico,webmanifest}"), //TODO
                new ReadFiles(
                    $"{basePath}/fonts/*",
                    $"{basePath}/js/*",
                    $"{basePath}/img/*",
                    $"{basePath}/favicon.ico"),
                new SetDestination(Config.FromDocument((doc,context) => new NormalizedPath(doc.Destination.ToString().Substring(basePath.Length+1)) ) ),
                new WriteFiles()
            };
        }
    }
}
