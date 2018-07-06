using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Swashbuckle.Orleans.SwaggerGen
{
    public class OrleansSwaggerGenOptions
    {
        public string DocumentName { get; set; }
        public string Host { get; set; }
        public string BasePath { get; set; }
        public List<string> Schemes { get; set; } = new List<string>();
        public Assembly GrainAssembly { get; set; }
        public string GrainInterfaceNameExtractRegexString { get; set; } = "(?<=(I))[.\\s\\S]*?(?=(Service))";

        public Regex GrainInterfaceNameExtractRegex { get
            {
                return new Regex(this.GrainInterfaceNameExtractRegexString, RegexOptions.Multiline | RegexOptions.Singleline);
            } }
        public Dictionary<Type, string> GrainInterfaceGrainKeyAsName { get; set; } = new Dictionary<Type, string>();

        public Action<SwaggerGenOptions> SwaggerGenAction { get; set; }
    }
}