using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TCC.JSONClasses
{
    [DataContract]
    public class JSONCable
    {
        public JSONCable() { }
        
        [DataMember(Name = "name")]
        public string name { get; set; }
        
        [DataMember(Name = "sections")]
        public List<JSONSection> sections { get; set; }
        
        [DataMember(Name = "layers")]
        public List<JSONLayer> layers { get; set; }
        
        [DataMember(Name = "layer_connections")]
        public List<JSONLayerConnection> layer_connections { get; set; }
        
        [DataMember(Name = "materials")]
        public List<JSONMaterial> materials { get; set; }
    }
}
