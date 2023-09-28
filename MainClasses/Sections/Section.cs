using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using TCC.MainClasses.Sections;

namespace TCC.MainClasses
{
    [DataContract]
    [JsonConverter(typeof(SectionConverter))]
    public abstract class Section
    {
        public Section() { }

        [JsonIgnore]
        public string Name { get; set; }
        
        [DataMember(Name = "id")]
        public int ID { get; set; }

        [DataMember(Name = "type")]
        public string Type { get; set; }
    }
}
