using Humanizer;
using RazorLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace MvcTemplate.GenCode
{
    public class ModuleModel
    {
        public RazorLightEngine RazorLightEngine;

        public ModuleModel(string templateDir, string modelName,string controllerName,string areaName)
        {
            string model = modelName;
            string controller = controllerName;
            string area = areaName;
            string templateName = "MvcTemplate";

            Parse(model, controller, area, templateName);

            RazorLightEngine = new RazorLightEngineBuilder()
              .UseFilesystemProject(templateDir)
              .UseMemoryCachingProvider()
              .Build();
        }

        public PropertyInfo[] AllProperties { get; set; }
        public String Area { get; set; }
        public String Controller { get; set; }
        public String ControllerName { get; set; }
        public String ControllerNamespace { get; set; }
        public String ControllerTests { get; set; }
        public String ControllerTestsNamespace { get; set; }
        public String IService { get; set; }
        public String IValidator { get; set; }
        public String Model { get; set; }
        public String Models { get; set; }
        public String ModelShortName { get; set; }
        public String ModelVarName { get; set; }
        public PropertyInfo[] Properties { get; set; }
        public String Service { get; set; }
        public String ServiceTests { get; set; }
        public String TemplateName { get; set; }
        public String Validator { get; set; }

        public String ValidatorTests { get; set; }

        public String View { get; set; }

        private void Parse(String model, String controller, String area, string templateName)
        {
            TemplateName = templateName;
            ModelShortName = Regex.Split(model, "(?=[A-Z])").Last();
            ModelVarName = ModelShortName.ToLower();
            Models = model.Pluralize(false);
            Model = model;

            View = $"{Model}View";

            ServiceTests = $"{Model}ServiceTests";
            IService = $"I{Model}Service";
            Service = $"{Model}Service";

            ValidatorTests = $"{Model}ValidatorTests";
            IValidator = $"I{Model}Validator";
            Validator = $"{Model}Validator";

            ControllerTestsNamespace = "MvcTemplate.Tests.Unit.Controllers" + (!String.IsNullOrWhiteSpace(area) ? $".{area}" : "");
            ControllerNamespace = "MvcTemplate.Controllers" + (!String.IsNullOrWhiteSpace(area) ? $".{area}" : "");
            ControllerTests = $"{controller}ControllerTests";
            Controller = $"{controller}Controller";
            ControllerName = $"{controller}";

            Area = area;

            string objName = string.Concat("MvcTemplate.Objects.", Model);


            AllProperties = typeof(MvcTemplate.Objects.BaseModel)
                .Assembly
                .GetTypes()
                .SingleOrDefault(type => type.Name.Equals(Model, StringComparison.OrdinalIgnoreCase))?
                .GetProperties(BindingFlags.Public | BindingFlags.Instance) ?? typeof(MvcTemplate.Objects.BaseModel).GetProperties();

            //AllProperties = typeof(TemplateModel).GetProperties(BindingFlags.Public | BindingFlags.Instance) ?? typeof(TemplateModel).GetProperties();


            Properties = typeof(MvcTemplate.Objects.BaseModel)
                .Assembly
                .GetTypes()
                .SingleOrDefault(type => type.Name.Equals(Model, StringComparison.OrdinalIgnoreCase))?
                .GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly) ?? new PropertyInfo[0];

            //Properties = typeof(TemplateModel).GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly) ?? new PropertyInfo[0];
        }
    }
}
