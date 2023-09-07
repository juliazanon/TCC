using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TCC.JSONClasses
{
    [DataContract]
    public class JSONLayerConnection
    {
        public JSONLayerConnection() { }

        [DataMember(Name = "type")]
        public string type { get; set; }
        
        [DataMember(Name = "first_layer")]
        public string first_layer { get; set; }

        [DataMember(Name = "second_layer")]
        public string second_layer { get; set; }

        [DataMember(Name = "friction_coefficient")]
        public double friction_coefficient { get; set; }

        [DataMember(Name = "normal_direction")]
        public double[] normal_direction { get; set; }
        
        [DataMember(Name = "first_tangent_direction")]
        public double[] first_tangent_direction { get; set; }
        
        [DataMember(Name = "second_tangent_direction")]
        public double[] second_tangent_direction { get; set; }
        
        [DataMember(Name = "normal_penalty")]
        public double normal_penalty { get; set; }
        
        [DataMember(Name = "tangential_penalty")]
        public double tangential_penalty { get; set; }
        
        [DataMember(Name = "pinball_search_radius")]
        public double pinball_search_radius { get; set; }
    }
}
