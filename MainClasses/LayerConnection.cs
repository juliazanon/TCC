using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TCC.MainClasses
{
    [DataContract]
    public class LayerConnection
    {
        public LayerConnection() { }

        [JsonIgnore]
        public string Name { get; set; }
        
        [DataMember(Name = "type")]
        public string Type { get; set; }
        
        [DataMember(Name = "first_layer")]
        public string FirstLayer { get; set; }
        
        [DataMember(Name = "second_layer")]
        public string SecondLayer { get; set; }
        
        [DataMember(Name = "friction_coefficient")]
        public double FrictionCoefficient { get; set; }
        
        [DataMember(Name = "normal_direction")]
        public double[] NormalDirection { get; set; }
        
        [DataMember(Name = "first_tangent_direction")]
        public double[] FirstTangentDirection { get; set; }
        
        [DataMember(Name = "second_tangent_direction")]
        public double[] SecondTangentDirection { get; set; }
        
        [DataMember(Name = "normal_penalty")]
        public double NormalPenalty { get; set; }
        
        [DataMember(Name = "tangential_penalty")]
        public double TangentialPenalty { get; set; }
        
        [DataMember(Name = "pinball_search_radius")]
        public double PinballSearchRadius { get; set; }
    }
}
