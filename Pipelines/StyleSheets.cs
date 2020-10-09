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
                new ReadFiles(pattern: "assets/scss/init.scss"),
                new CompileSass()
                    .WithCompactOutputStyle(),
                new SetDestination( new NormalizedPath("site.css") ),
                new WriteFiles()
            };
        }
    }
}

