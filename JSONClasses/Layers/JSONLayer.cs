using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using TCC.JSONClasses.Layers;

namespace TCC.JSONClasses
{
    [DataContract]
    [JsonConverter(typeof(LayerConverter))]
    public abstract class JSONLayer
    {
        public JSONLayer() { }
        
        [DataMember(Name = "name")]
        public string name { get; set; }
        
        [DataMember(Name = "type")]
        public string type { get; set; }
        
        [DataMember(Name = "material")]
        public int material { get; set; }
        
        [DataMember(Name = "length")]
        public double length { get; set; }
        
        [DataMember(Name = "body_load")]
        public double[] body_load { get; set; }
    }
}
