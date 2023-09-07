using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using TCC.JSONClasses.Sections;

namespace TCC.JSONClasses
{
    [DataContract]
    [JsonConverter(typeof(SectionConverter))]
    public abstract class JSONSection
    {
        public JSONSection() { }

        [DataMember(Name = "id")]
        public int id { get; set; }

        [DataMember(Name = "type")]
        public string type { get; set; }
    }
}
