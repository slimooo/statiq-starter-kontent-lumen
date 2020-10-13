using Statiq.Common;
using Statiq.Core;

namespace Generator.Pipelines
{
    public class CopyAssets : Pipeline
    {
        public CopyAssets()
        {
            var basePath = "assets";
            InputModules = new ModuleList
            {
                new ReadFiles(
                    $"{basePath}/fonts/*",
                    $"{basePath}/js/libs/*",
                    $"{basePath}/js/libs/bootstrap/bootstrap.min.js",
                    $"{basePath}/img/*",
                    $"{basePath}/favicon.ico"),
                new SetDestination(Config.FromDocument((doc,context) => new NormalizedPath(doc.Destination.ToString().Substring(basePath.Length+1)) ) ),
                new WriteFiles()
            };
        }
    }
}
