using Statiq.Common;
using Statiq.Core;

namespace Kentico.Kontent.Statiq.Lumen.Pipelines
{
    public class CopyAssets : Pipeline
    {
        public CopyAssets()
        {
            InputModules = new ModuleList
            {
                new CopyFiles("./assets/{css,fonts,js,img}/**/*", "./assets/*.{png,ico,webmanifest}")
            };
        }
    }
}
