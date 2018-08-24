using MvcTemplate.GenCode.Private;
using System;
using System.Collections.Generic;
using System.IO;

namespace MvcTemplate.GenCode
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            string modelName = args[0];
            string controllerName = args[1];
            String areaName;

            if (args.Length == 3)
            {
                areaName = args[2];
            }
            else
            {
                areaName = "";
            }
            
            string templateDir = Directory.GetCurrentDirectory().Substring(0, Directory.GetCurrentDirectory().ToLower().LastIndexOf(".gencode")) + @".gencode\module";
            string newDir = templateDir.Substring(0, templateDir.LastIndexOf(@"\src\")) + @"\src\";
            var config = new ModuleModel(templateDir, modelName, controllerName, areaName);
            String path = (config.Area != null ? config.Area + "/" : "") + config.ControllerName;
            var executor = new List<IGene>();

            MakeGeneDir(newDir, config, path, executor);
            MakeGeneFiles(newDir, config, path, executor);
            MakeGenFilesByCopy(newDir, config, path, executor, templateDir);

            
            foreach (var item in executor)
            {
                item.Run();
            }

            Console.WriteLine("It is OK");
        }

        private static void MakeGenFilesByCopy(string newDir, ModuleModel config, string path, List<IGene> executor, string templateDir)
        {
            string modelSourceFile = templateDir.Replace(@"gencode\module", $@"Objects\Models\{path}\{config.Model}.cs");
            modelSourceFile = modelSourceFile.Replace(@"/", @"\");

            executor.Add(new GeneViewByModel(config, modelSourceFile, $"{newDir}/{config.TemplateName}.Objects/Views/{path}/{config.View}.cs"));
        }

        private static void MakeGeneFiles(string newDir, ModuleModel config, string path, List<IGene> executor)
        {
            executor.Add(new GeneFile(config, "Controllers/Controller.cshtml", $"{newDir}/{config.TemplateName}.Controllers/{path}/{config.Controller}.cs"));
            //executor.Add(new GeneFile(config, "Tests/ControllerTests.cshtml", $"{newDir.Replace(@"src","test")}/{config.TemplateName}.Tests/Unit/Controllers/{path}/{config.ControllerTests}.cs"));


            executor.Add(new GeneFile(config, "Services/Service.cshtml", $"{newDir}/{config.TemplateName}.Services/{path}/{config.Model}Service.cs"));
            executor.Add(new GeneFile(config, "Services/IService.cshtml", $"{newDir}/{config.TemplateName}.Services/{path}/I{config.Model}Service.cs"));
            //executor.Add(new GeneFile(config, "Tests/ServiceTests.cshtml", $"{newDir.Replace(@"src", "test")}/{config.TemplateName}.Tests/Unit/Services/{path}/{config.Model}ServiceTests.cs"));

            executor.Add(new GeneFile(config, "Validators/Validator.cshtml", $"{newDir}/{config.TemplateName}.Validators/{path}/{config.Model}Validator.cs"));
            executor.Add(new GeneFile(config, "Validators/IValidator.cshtml", $"{newDir}/{config.TemplateName}.Validators/{path}/I{config.Model}Validator.cs"));
            //executor.Add(new GeneFile(config, "Tests/ValidatorTests.cshtml", $"{newDir.Replace(@"src", "test")}/{config.TemplateName}.Tests/Unit/Validators/{path}/{config.Model}ValidatorTests.cs"));

            executor.Add(new GeneFile(config, "Web/Index.cshtml", $"{newDir}/{config.TemplateName}.Web/Views/{path}/Index.cshtml"));
            executor.Add(new GeneFile(config, "Web/Create.cshtml", $"{newDir}/{config.TemplateName}.Web/Views/{path}/Create.cshtml"));
            executor.Add(new GeneFile(config, "Web/Details.cshtml", $"{newDir}/{config.TemplateName}.Web/Views/{path}/Details.cshtml"));
            executor.Add(new GeneFile(config, "Web/Edit.cshtml", $"{newDir}/{config.TemplateName}.Web/Views/{path}/Edit.cshtml"));
            executor.Add(new GeneFile(config, "Web/Delete.cshtml", $"{newDir}/{config.TemplateName}.Web/Views/{path}/Delete.cshtml"));
        }

        private static void MakeGeneDir(string newDir, ModuleModel config, string path, List<IGene> executor)
        {
            string testdir = newDir.Replace(@"src", "test");


            executor.Add(new GeneDir($"{newDir}/{config.TemplateName}.Controllers/{path}/"));
            //executor.Add(new GeneDir($"{newDir}/{config.TemplateName}.Objects/Models/{path}/"));
            executor.Add(new GeneDir($"{newDir}/{config.TemplateName}.Objects/Views/{path}/"));
            executor.Add(new GeneDir($"{newDir}/{config.TemplateName}.Services/{path}/"));
            
            executor.Add(new GeneDir($"{testdir}/{config.TemplateName}.Tests/Unit/Controllers/{path}/"));
            executor.Add(new GeneDir($"{testdir}/{config.TemplateName}.Tests/Unit/Services/{path}/"));
            executor.Add(new GeneDir($"{testdir}/{config.TemplateName}.Tests/Unit/Validators/{path}/"));

            executor.Add(new GeneDir($"{newDir}/{config.TemplateName}.Validators/{path}/"));
            executor.Add(new GeneDir($"{newDir}/{config.TemplateName}.Web/Views/{path}/"));
        }
    }
}