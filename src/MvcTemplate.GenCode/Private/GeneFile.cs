using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MvcTemplate.GenCode.Private
{
    internal class GeneFile : IGene
    {
        private string sourceFileName;
        private string targetFileName;
        private ModuleModel config;

        public GeneFile(ModuleModel obj, string source,string target)
        {
            sourceFileName = source;
            targetFileName = target;
            config = obj;
        }

        public void Run()
        {
            //string template = File.ReadAllText(sourceFileName);
            //var model = config;
            //string result = config.RazorLightEngine.CompileRenderAsync("template", template, model).Result;

            if (!File.Exists(targetFileName))
            {
                string result = config.RazorLightEngine.CompileRenderAsync(sourceFileName, config).Result;
                File.WriteAllText(targetFileName, result);
                Console.WriteLine(targetFileName);
            }
            else
            {
                Console.WriteLine($"FILE EXISTS - SKIPPED: {targetFileName}");
            }

            
        }
    }
}
