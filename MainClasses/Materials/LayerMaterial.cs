using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using TCC.MainClasses.Materials;

namespace TCC.MainClasses
{
    [DataContract]
    [JsonConverter(typeof(MaterialConverter))]
    public abstract class LayerMaterial
    {
        public LayerMaterial() { }

        [JsonIgnore]
        public string Name { get; set; }

        [DataMember(Name = "density")]
        public double Density { get; set; }
        
        [DataMember(Name = "id")]
        public int ID { get; set; }
        
        [DataMember(Name = "type")]
        public string Type { get; set; }
    }
}
