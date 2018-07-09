using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Swashbuckle.Orleans.SwaggerGen
{
    public class GrainKeyDescription
    {
        public GrainKeyDescription(string name, string des)
       : this(name, des,null)
        {

        }
        public GrainKeyDescription(string name, string des, params string[] noNeedKeyMethod)
        {
            this.Name = name;
            this.Description = des;
            this.NoNeedKeyMethod = noNeedKeyMethod?.ToList() ?? new List<string>();
        }
        public string Name { get; set; }

        public string Description { get; set; }

        public List<string> NoNeedKeyMethod { get; set; } = new List<string>();

    }
}
