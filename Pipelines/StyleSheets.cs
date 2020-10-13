using Statiq.Common;
using Statiq.Core;
using Statiq.Sass;

namespace Generator.Pipelines
{
    public class StyleSheets : Pipeline
    {
        public StyleSheets()
        {
            InputModules = new ModuleList {
                new ReadFiles(pattern: "assets/scss/**/{!_,}*.scss"),
                new CompileSass()
                    .WithCompactOutputStyle(),
                new SetDestination(".css"),
                new WriteFiles()
            };
        }
    }
}

