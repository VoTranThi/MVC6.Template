using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace MvcTemplate.GenCode.Private
{
    internal class GeneViewByModel : IGene
    {
        private readonly string sourceFile;
        private readonly string targetFile;
        private ModuleModel config;

        public GeneViewByModel(ModuleModel obj,string source, string target)
        {
            this.sourceFile = source;
            this.targetFile = target;
            config = obj;
        }
        public void Run()
        {
            if (!File.Exists(sourceFile))
            {
                Console.WriteLine($"FILE DOES NOT EXISTS: {sourceFile}");
            }

            string content = File.ReadAllText(sourceFile);

            content = content.Replace("BaseModel", "BaseView");
            content = content.Replace($"class {config.Model}", $"class {config.Model}View");

            if (!File.Exists(targetFile))
            {
                File.WriteAllText(targetFile, content);
            }
            else
            {
                Console.WriteLine($"FILE EXISTS - SKIPPED: {targetFile}");
            }
        }
    }
}
