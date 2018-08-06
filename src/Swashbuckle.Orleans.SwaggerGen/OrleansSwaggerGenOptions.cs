using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Swashbuckle.Orleans.SwaggerGen
{
    public class OrleansSwaggerGenOptions
    {
        public OrleansSwaggerGenOptions()
        {
            this.SetApiRouteTemplateFunc = (m) => new WebApiRoute(m.DeclaringType.Name, $"/{m.DeclaringType.Name}/{m.Name}");
        }
        public string DocumentName { get; set; }
        public string Host { get; set; }
        public string BasePath { get; set; }
        public List<string> Schemes { get; set; } = new List<string>();
        public Assembly GrainAssembly { get; set; }
        public Dictionary<Type, GrainKeyDescription> GrainInterfaceGrainKeyAsName { get; set; } = new Dictionary<Type, GrainKeyDescription>();
        public Func<MethodInfo, WebApiRoute> SetApiRouteTemplateFunc { get; set; }
        public List<string> IgnoreGrainInterfaces { get; set; } = new List<string>();
        public List<string> IgnoreGrainMethods { get; set; } = new List<string>();

    }
}